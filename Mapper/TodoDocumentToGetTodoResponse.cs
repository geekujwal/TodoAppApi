
using Riok.Mapperly.Abstractions;
using TodoAppApi.Documents;
using TodoAppApi.Contracts;

namespace TodoAppApi.Mapper;

[Mapper]
public partial class TodoDocumentToGetTodoResponseMapper
{
    public partial GetTodoListResponse TodoDocumentToGetTodoResponse(TodoListDocument car);
}