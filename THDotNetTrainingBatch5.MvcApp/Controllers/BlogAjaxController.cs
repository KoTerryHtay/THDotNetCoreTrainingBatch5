using Microsoft.AspNetCore.Mvc;
using THDotNetTrainingBatch5.Database.Models;
using THDotNetTrainingBatch5.Domain.Features.Blog;
using THDotNetTrainingBatch5.MvcApp.Models;
namespace THDotNetTrainingBatch5.MvcApp.Controllers;

public class BlogAjaxController : Controller
{
    private readonly IBlogService _blogService;

    public BlogAjaxController(IBlogService blogService)
    {
        _blogService = blogService;
    }

    [ActionName("Index")]
    public IActionResult BlogList()
    {
        var lst = _blogService.GetBlogs();

        return View("BlogList", lst.Data.TblBlog);
    }

    [ActionName("List")]
    public IActionResult BlogListAjax()
    {
        var lst = _blogService.GetBlogs();
        return Json(lst.Data.TblBlog);
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
        MessageModel model;
        try
        {
            _blogService.CreateBlog(new TblBlog
            {
                BlogTitle = requestModel.Title,
                BlogAuthor = requestModel.Author,
                BlogContent = requestModel.Content,
            });
            model = new MessageModel(true, "Blog Created Successfully");
        }
        catch (Exception ex)
        {
            model = new MessageModel(false, ex.ToString());
        }

        return Json(model);
    }

    public class MessageModel
    {
        public MessageModel()
        {
        }

        public MessageModel(bool isIsuccess, string message)
        {
            IsSuccess = isIsuccess;
            Message = message;
        }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }

    [HttpPost]
    [ActionName("Delete")]
    public IActionResult BlogDelete(BlogRequestModel blogRequestModel)
    {
        //return RedirectToAction("Index");
        MessageModel model;
        try
        {
            _blogService.DeleteBlog(blogRequestModel.Id);
            model = new MessageModel(true, "Blog Deleted Successfully");
        }
        catch (Exception ex)
        {
            model = new MessageModel(false, ex.ToString());
        }

        return Json(model);
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
        MessageModel model;
        try
        {
            _blogService.UpdateBlog(id, new TblBlog
            {
                BlogTitle = requestModel.Title,
                BlogAuthor = requestModel.Author,
                BlogContent = requestModel.Content,
            });

            model = new MessageModel(true, "Blog Updated Successfully");
        }
        catch (Exception ex)
        {
            model = new MessageModel(false, ex.ToString());
        }

        return Json(model);
    }
}
