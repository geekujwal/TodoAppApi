using MongoDB.Driver;
using TodoAppApi.Abstractions;
using TodoAppApi.Contracts;
using TodoAppApi.Documents;
using TodoAppApi.Mapper;

namespace TodoAppApi.Service;

public class TodoListService : ITodoListService
{
    private readonly IMongoCollection<TodoListDocument> _todoListDocument;
    public TodoListService(MongoDbContext context)
    {
        _todoListDocument = context.GetCollection<TodoListDocument>(nameof(TodoListDocument));
    }
    public async Task<string> CreateTodoListAsync(CreateOrUpdateTodoListRequest request, CancellationToken cancellationToken)
    {
        var ids = new HashSet<string>
        {
            request.After,
            request.Before
        };
        FilterDefinition<TodoListDocument> filter = Builders<TodoListDocument>.Filter.In(doc => doc.Id, ids);
        float rank = 0;
        var todos = await (await _todoListDocument.FindAsync<TodoListDocument>(filter, cancellationToken: cancellationToken)).ToListAsync(cancellationToken: cancellationToken);
        if (todos.Count > 1)
        {
            rank = todos.Select(doc => doc.Rank).Sum() / 2;
        }
        if (todos.Count == 1)
        {
            rank = todos.FirstOrDefault().Rank / 2;
        }
        if (todos.Count == 0)
        {
        }
        var todo = new TodoListDocument()
        {
            Id = Guid.NewGuid().ToString(),
            Title = request.Title,
            Rank = rank
        };
        await _todoListDocument.InsertOneAsync(todo, cancellationToken: cancellationToken);
        return todo.Id;
    }

    public async Task DeleteTodoItemAsync(string id, CancellationToken cancellationToken)
    {
        var filter = Builders<TodoListDocument>.Filter.Eq(doc => doc.Id, id);
        var result = await _todoListDocument.DeleteOneAsync(filter, cancellationToken);
        if (result.DeletedCount > 0)
        {
            return;
        }
        else
        {
            throw new Exception("Item Not found");
        }
    }

    public async Task<List<GetTodoListResponse>> GetTodoItemAsync(PageFilter pageFilter, CancellationToken cancellationToken)
    {
        // todo with pagination
        // todo map the response
        // todo add text search feature
        var filter = Builders<TodoListDocument>.Filter.Empty;
        var result = await _todoListDocument.FindAsync<TodoListDocument>(filter, cancellationToken: cancellationToken);
        var response =  await result.ToListAsync(cancellationToken: cancellationToken);
        var mapperResponse = response.Select(res => new TodoDocumentToGetTodoResponseMapper().TodoDocumentToGetTodoResponse(res)).ToList();
        return mapperResponse;

    }

    public async Task<bool> UpdateTodoListAsync(string id, CreateOrUpdateTodoListRequest request, CancellationToken cancellationToken)
    {
        var filter = Builders<TodoListDocument>.Filter.Eq(doc => doc.Id, id);
        var update = Builders<TodoListDocument>.Update
            .Set(doc => doc.Title, request.Title);

        if (request.After is not null || request.Before is not null)
        {
            var ids = new HashSet<string>
        {
            request.After,
            request.Before
        };
            var rankFilter = Builders<TodoListDocument>.Filter.In(doc => doc.Id, ids);
            var todos = await (await _todoListDocument.FindAsync(rankFilter, cancellationToken: cancellationToken)).ToListAsync(cancellationToken: cancellationToken);

            float newRank = 0;
            if (todos.Count > 1)
            {
                newRank = todos.Select(doc => doc.Rank).Sum() / 2;
            }
            else if (todos.Count == 1)
            {
                newRank = todos.First().Rank / 2;
            }

            update = update.Set(doc => doc.Rank, newRank);
        }

        var result = await _todoListDocument.UpdateOneAsync(filter, update, cancellationToken: cancellationToken);
        return result.ModifiedCount > 0;
    }
}
