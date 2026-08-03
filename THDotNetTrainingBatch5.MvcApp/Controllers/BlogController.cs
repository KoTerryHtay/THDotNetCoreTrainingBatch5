using Microsoft.AspNetCore.Mvc;
using THDotNetTrainingBatch5.Database.Models;
using THDotNetTrainingBatch5.Domain.Features.Blog;
using THDotNetTrainingBatch5.MvcApp.Models;
namespace THDotNetTrainingBatch5.MvcApp.Controllers;

public class BlogController : Controller
{
    private readonly IBlogService _blogService;

    public BlogController(IBlogService blogService)
    {
        _blogService = blogService;
    }

    public IActionResult Index()
    {
        var lst = _blogService.GetBlogs();

        return View(lst.Data.TblBlog);
    }

    [ActionName("Create")]
    public IActionResult BlogCreate()
    {
        return View("BlogCreate");
    }

    [HttpPost]
    [ActionName("Save")]
    public IActionResult BlogSave(BlogRequestModel requestModel)
    {
        try
        {
            _blogService.CreateBlog(new TblBlog
            {
                BlogTitle = requestModel.Title,
                BlogAuthor = requestModel.Author,
                BlogContent = requestModel.Content,
            });

            //ViewBag.IsSuccess = true;
            //ViewBag.Message = "Blog Created Successfully";

            TempData["IsSuccess"] = true;
            TempData["Message"] = "Blog Created Successfully";
        }
        catch (Exception ex)
        {
            TempData["IsSuccess"] = false;
            TempData["Message"] = ex.ToString();
        }

        //var lst = _blogService.GetBlogs();
        //return View("Index", lst);

        return RedirectToAction("Index");
    }

    [ActionName("Delete")]
    public IActionResult BlogDelete(int id)
    {
        _blogService.DeleteBlog(id);
        return RedirectToAction("Index");
    }

    [ActionName("Edit")]
    public IActionResult BlogEdit(int id)
    {
        var blog = _blogService.GetBlog(id);
        BlogRequestModel blogRequestModel = new BlogRequestModel
        {
            Id = blog.BlogId,
            Author = blog.BlogAuthor,
            Content = blog.BlogContent,
            Title = blog.BlogTitle
        };

        return View("BlogEdit", blogRequestModel);
    }

    [HttpPost]
    [ActionName("Update")]
    public IActionResult BlogUpdate(int id, BlogRequestModel requestModel)
    {
        try
        {
            _blogService.UpdateBlog(id, new TblBlog
            {
                BlogTitle = requestModel.Title,
                BlogAuthor = requestModel.Author,
                BlogContent = requestModel.Content,
            });

            TempData["IsSuccess"] = true;
            TempData["Message"] = "Blog Updated Successfully";
        }
        catch (Exception ex)
        {
            TempData["IsSuccess"] = false;
            TempData["Message"] = ex.ToString();
        }

        return RedirectToAction("Index");
    }
}
