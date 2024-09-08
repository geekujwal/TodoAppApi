using Microsoft.Extensions.Options;
using TodoAppApi.Abstractions;
using TodoAppApi.Contracts;
using TodoAppApi.Middlewares;
using TodoAppApi.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

ConfigureServices(builder.Services);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseAuthorization();

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
}
