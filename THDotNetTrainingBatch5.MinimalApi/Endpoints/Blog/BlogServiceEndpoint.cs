using Microsoft.AspNetCore.Mvc;
using THDotNetTrainingBatch5.Domain.Features.Blog;

namespace THDotNetTrainingBatch5.MinimalApi.Endpoints.Blog;

// Presentation Layer
public static class BlogServiceEndpoint
{

    //public static string Test(this string i)
    //{
    //    return i;
    //}

    public static void UseBlogServiceEndpoint(this IEndpointRouteBuilder app)
    {

        app.MapGet("/blogs", ([FromServices] IBlogService service) =>
        {
            //BlogService service = new BlogService();
            var lst = service.GetBlogs();
            return Results.Ok(lst);
        })
        .WithName("GetBlogs")
        .WithOpenApi();

        app.MapGet("/blogs/{id}", ([FromServices] IBlogService service, int id) =>
        {
            var item = service.GetBlog(id);
            if (item is null)
            {
                return Results.BadRequest("No data found");
            }
            return Results.Ok(item);
        })
        .WithName("GetBlog")
        .WithOpenApi();

        app.MapPost("/blogs", ([FromServices] IBlogService service, TblBlog blog) =>
        {
            var newBlog = service.CreateBlog(blog);
            return Results.Ok(newBlog);
        })
        .WithName("CreateBlog")
        .WithOpenApi();

        app.MapPut("/blogs/{id}", ([FromServices] IBlogService service, int id, TblBlog blog) =>
        {

            var item = service.UpdateBlog(id, blog);
            if (item is null)
            {
                return Results.BadRequest("No data found");
            }
            return Results.Ok(blog);
        })
        .WithName("UpdateBlog")
        .WithOpenApi();

        app.MapDelete("/blogs/{id}", ([FromServices] IBlogService service, int id) =>
        {
            var item = service.DeleteBlog(id);
            if (item is null)
            {
                return Results.BadRequest("No data found");
            }

            return Results.Ok((bool)item ? "Delete Successfully" : "Delete not Successfully");
        })
        .WithName("DeleteBlog")
        .WithOpenApi();
    }
}
