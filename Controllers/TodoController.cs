using Microsoft.AspNetCore.Mvc;
using TodoAppApi.Abstractions;
using TodoAppApi.Contracts;

namespace TodoAppApi.Controllers;

[ApiController]
[Route("[controller]")]
public class TodoListController : ControllerBase
{
    private readonly ITodoListService _todolistService;
   
    public TodoListController(ITodoListService todolistService)
    {
        _todolistService = todolistService;
    }

    [HttpPost]
    public async Task<string> CreateTodoListAsync([FromBody]CreateOrUpdateTodoListRequest request, CancellationToken cancellationToken )
    {
        return await _todolistService.CreateTodoListAsync(request, cancellationToken); 
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTodoListAsync(string id, CancellationToken cancellationToken )
    {
        await _todolistService.DeleteTodoItemAsync(id, cancellationToken); 
        return NoContent();
    }
}
