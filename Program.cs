using Microsoft.Extensions.Options;
using TodoAppApi.Abstractions;
using TodoAppApi.Contracts;
using TodoAppApi.Hubs;
using TodoAppApi.Middlewares;
using TodoAppApi.Service;

var builder = WebApplication.CreateBuilder(args);


ConfigureServices(builder.Services);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseAuthorization();

app.MapHub<TodoListHub>("/todolisthub");

app.MapControllers();


app.Run();

void ConfigureServices(IServiceCollection services)
{
    services.AddSingleton<ITodoListService, TodoListService>();

    services.Configure<MongoDbSettings>(
       builder.Configuration.GetSection(nameof(MongoDbSettings)));

    services.AddSingleton<MongoDbSettings>(sp =>
        sp.GetRequiredService<IOptions<MongoDbSettings>>().Value);

    services.AddSingleton<MongoDbContext>();

    services.AddControllers();

    services.AddSignalR();
}
