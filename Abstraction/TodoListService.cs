using TodoAppApi.Contracts;

namespace TodoAppApi.Abstractions;

public interface ITodoListService
{
    Task<string> CreateTodoListAsync(CreateTodoListRequest request, CancellationToken cancellationToken );
}
