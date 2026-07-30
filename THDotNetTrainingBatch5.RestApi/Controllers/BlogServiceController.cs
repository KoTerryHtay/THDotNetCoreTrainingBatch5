using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using THDotNetTrainingBatch5.Database.Models;
using THDotNetTrainingBatch5.Domain.Features.Blog;
using THDotNetTrainingBatch5.RestApi.Endpoints;

namespace THDotNetTrainingBatch5.RestApi.Controllers;

[Route("api/[controller]")]
[ApiController]
//public class BlogServiceController : ControllerBase
public class BlogServiceController : BaseController
{
    private readonly IBlogService _service;

    public BlogServiceController(IBlogService service)
    {
        _service = service;
    }

    //public BlogServiceController()
    //{
    //    _service = new BlogService();
    //}

    [HttpGet]
    public IActionResult GetBlogs()
    {
        var lst = _service.GetBlogs();
        return Ok(lst);
    }

    [HttpGet("{id}")]
    public IActionResult GetBlog(int id)
    {
        var item = _service.GetBlog(id);

        if (item is null)
        {
            return NotFound();
        }

        return Ok(item);
    }

    [HttpPost]
    public IActionResult CreateBlogs(TblBlog blog)
    {
        var model = _service.CreateBlog(blog);
        return Ok(model);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateBlogs(int id, TblBlog blog)
    {
        var model = _service.UpdateBlog(id, blog);

        //if (model.Response.RespType.Equals(EnumRespType.SystemError)) return NotFound(model);

        //return Ok(model);

        //if (item is null)
        //{
        //    return NotFound();
        //}

        //return Ok(model);

        return Execute(model);
    }

    [HttpPatch("{id}")]
    public IActionResult PatchBlogs(int id, TblBlog blog)
    {
        var item = _service.PatchBlog(id, blog);
        if (item is null)
        {
            return NotFound();
        }

        return Ok(item);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteBlogs(int id)
    {
        var item = _service.DeleteBlog(id);
        if (item is null)
        {
            return NotFound();
        }

        return Ok((bool)item ? "Delete Successfully" : "Delete not Successfully");
    }
}
