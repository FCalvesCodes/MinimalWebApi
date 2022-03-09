using Microsoft.EntityFrameworkCore;
using MinimalWebApi.Data;
using MinimalWebApi.Models;
using MinimalWebApi.Services;
using MinimalWebApi.Services.Contracts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApiContext>(options => options.UseInMemoryDatabase("api"));
builder.Services.AddScoped<IArticleService, ArticleService>();

var app = builder.Build();

app.MapGet("/articles", async (IArticleService service) =>
{
    return Results.Ok(await service.GetAllAsync());
});

app.MapGet("/articles/{id}", async (int id, IArticleService service) =>
{
    var article = await service.GetAsync(id);
    return article != null ? Results.Ok(article) : Results.NotFound();
});

app.MapPost("/articles", async (ArticleRequest request, IArticleService service) =>
{
    var article = await service.CreateAsync(new Article
    {
        Title = request.Title ?? string.Empty,
        Content = request.Content ?? string.Empty,
        PublishedAt = request.PublishedAt,
    });

    return Results.Created($"/articles/{article.Id}", article);
});

app.MapPut("/articles/{id}", async (int id, ArticleRequest request, IArticleService service) =>
{
    var article = await service.UpdateAsync(new Article()
    {
        Id = id,
        Title = request.Title,
        Content = request.Content,
        PublishedAt = request.PublishedAt,
    });

    return Results.Ok(article);
});

app.MapDelete("/articles/{id}", async (int id, ApiContext context) =>
{
    var article = await context.Articles.FindAsync(id);
    if (article == null)
    {
        return Results.NotFound();
    }
    context.Articles.Remove(article);
    await context.SaveChangesAsync();
    return Results.NoContent();
});

app.Run();