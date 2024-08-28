using MongoDB.Driver;
using TodoAppApi.Abstractions;
using TodoAppApi.Contracts;
using TodoAppApi.Documents;

namespace TodoAppApi.Service;

public class TodoListService : ITodoListService
{
    private readonly IMongoCollection<TodoListDocument> _todoListDocument;
    public TodoListService(MongoDbContext context)
    {
        _todoListDocument = context.GetCollection<TodoListDocument>(nameof(TodoListDocument));
    }
    public async Task<string> CreateTodoListAsync(CreateTodoListRequest request, CancellationToken cancellationToken )
    {
        // todo get todo from before and after
        // if they are empty assum no item in list
        // if before is empty assume its 1st rank item
        // if after is empty assume it is last rank item
        // if both are present get their rank average and store in new item
        var todo = new TodoListDocument()
        {
            Id = Guid.NewGuid().ToString(),
            Title = request.Title,
            Rank = 0
        };
        await _todoListDocument.InsertOneAsync(todo, cancellationToken: cancellationToken);
        return todo.Id;
    }
}
