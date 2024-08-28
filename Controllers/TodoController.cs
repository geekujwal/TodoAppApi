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

    [HttpGet(Name = "TodoList")]
    public void GetTodoListAsync()
    {
        var x = 1 + 1;
    }

    [HttpPost(Name = "TodoList")]
    public async Task<string> CreateTodoListAsync(CreateTodoListRequest request, CancellationToken cancellationToken )
    {
        return await _todolistService.CreateTodoListAsync(request, cancellationToken); 
    }
}
