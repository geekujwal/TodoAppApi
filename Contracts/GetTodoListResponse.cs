namespace TodoAppApi.Contracts;

public class GetTodoListResponse
{
    public string Id { get; set; }

    public string Title { get; set; }

    public float Rank { get; set; }

    public DateTimeOffset Created { get; set; } = DateTimeOffset.UtcNow;

    public DateTimeOffset Modified { get; set; } = DateTimeOffset.UtcNow;
}
