namespace TodoAppApi.Contracts;

public class CreateTodoListRequest
{
    public string Title { get; set; }
    // public float Rank { get; set; }
    public string Before { get; set; }

    public string After { get; set; }
}