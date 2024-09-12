using TodoAppApi.Contracts;

namespace TodoAppApi.Abstractions;

public interface ITodoListService
{
    Task<string> CreateTodoListAsync(CreateOrUpdateTodoListRequest request, CancellationToken cancellationToken );

    Task DeleteTodoItemAsync(string id, CancellationToken cancellationToken);

    //todo need to use actual DTO instead of dynamic
    Task<dynamic> GetTodoItemAsync(PageFilter filter, CancellationToken cancellationToken);

    Task<bool> UpdateTodoListAsync(string id, CreateOrUpdateTodoListRequest request, CancellationToken cancellationToken);
}
