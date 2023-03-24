using Microsoft.OpenApi.Models;
using rubenheeren_aspnet_core_react.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("CORSPolicy",
     builder =>
     {
         builder.AllowAnyMethod().AllowAnyHeader().WithOrigins("http://localhost:3000", "https://salmon-coast-0046a3d03.2.azurestaticapps.net");
     }
     );
});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(swaggerGenOptions =>
{
    swaggerGenOptions.SwaggerDoc("v1",new OpenApiInfo {Title = "ASP.NET React Tutorial",Version="v1"});
});


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI(swaggerUIOptions =>
{
    swaggerUIOptions.DocumentTitle = "ASP.NET React Tutorial";
    swaggerUIOptions.SwaggerEndpoint("/swagger/v1/swagger.json","Web API serving simple Post model");
    swaggerUIOptions.RoutePrefix = string.Empty;
});


app.UseHttpsRedirection();

app.UseCors("CORSPolicy");

app.MapGet("/get-all-posts", async () => await PostsRepository.GetPostsAsync()).WithTags("Posts Endpoints");

app.MapGet("/get-post-by-id/{id}", async (int id) =>
{
    Post postToReturn = await PostsRepository.GetPostByIdAsync(id);
    if (postToReturn!=null)
    {
        return Results.Ok(postToReturn);
    }
    else
    {
        return Results.BadRequest();
    }
}).WithTags("Posts Endpoints");

app.MapPost("/create-post", async (Post postToCreate) =>
{
    bool res = await PostsRepository.CreatePostAsync(postToCreate);

    if (res)
    {
        return Results.Ok("Created successfully");
    }
    else
    {
        return Results.BadRequest();
    }
}).WithTags("Posts Endpoints");

app.MapPut("/update-post", async (Post postToUpdate) =>
{
    bool res = await PostsRepository.UpdatePostAsync(postToUpdate);

    if (res)
    {
        return Results.Ok("Updated successfully");
    }
    else
    {
        return Results.BadRequest();
    }
}).WithTags("Posts Endpoints");

app.MapDelete("/delete-post/{postId}", async (int postId) =>
{
    bool res = await PostsRepository.DeletePostAsync(postId);

    if (res)
    {
        return Results.Ok("Removed post successfully");
    }
    else
    {
        return Results.BadRequest();
    }
}).WithTags("Posts Endpoints");



app.Run();
