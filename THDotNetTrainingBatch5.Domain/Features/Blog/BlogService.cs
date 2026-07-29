using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using THDotNetTrainingBatch5.Database.Models;
using THDotNetTrainingBatch5.Domain.Models;

namespace THDotNetTrainingBatch5.Domain.Features.Blog;

// Business Logic + Data Access Layer
public class BlogService
{
    private readonly AppDbContext _db = new AppDbContext();

    //public List<TblBlog> GetBlogs()
    public Result<ResultBlogResponseModel<List<TblBlog>>> GetBlogs()
    {
        Result<ResultBlogResponseModel<List<TblBlog>>> model = new Result<ResultBlogResponseModel<List<TblBlog>>>();

        var tbl = _db.TblBlogs.AsNoTracking().ToList();

        if (tbl.Count == 0)
        {
            model = Result<ResultBlogResponseModel<List<TblBlog>>>.SystemError("Data not found");
            goto Result;
        }

        model = Result<ResultBlogResponseModel<List<TblBlog>>>.Success(new ResultBlogResponseModel<List<TblBlog>> { TblBlog = tbl }, "Get all blogs");

    Result:
        return model;
    }

    public TblBlog GetBlog(int id)
    {
        var item = _db.TblBlogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id);
        return item;
    }

    public TblBlog CreateBlog(TblBlog blog)
    {
        _db.TblBlogs.Add(blog);
        _db.SaveChanges();

        return blog;
    }

    //public TblBlog UpdateBlog(int id, TblBlog blog)
    public BlogResponseModel UpdateBlog(int id, TblBlog blog)
    {
        BlogResponseModel model = new BlogResponseModel();

        var item = _db.TblBlogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id);
        if (item is null)
        {
            model.Response = BaseResponseModel.SystemError("002", "No data found!");
            //return model;
            goto Result;
        }

        item.BlogTitle = blog.BlogTitle;
        item.BlogAuthor = blog.BlogAuthor;
        item.BlogContent = blog.BlogContent;

        _db.Entry(item).State = EntityState.Modified;
        _db.SaveChanges();

        model.TblBlog = item;
        model.Response = BaseResponseModel.SystemError("001", "Update Successfully");

    Result:
        return model;
        //return item;
    }

    public TblBlog PatchBlog(int id, TblBlog blog)
    {
        var item = _db.TblBlogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id);
        if (item is null)
        {
            return null;
        }

        if (!string.IsNullOrEmpty(blog.BlogTitle))
        {
            item.BlogTitle = blog.BlogTitle;
        }
        if (!string.IsNullOrEmpty(blog.BlogAuthor))
        {
            item.BlogAuthor = blog.BlogAuthor;
        }
        if (!string.IsNullOrEmpty(blog.BlogContent))
        {
            item.BlogContent = blog.BlogContent;
        }

        _db.Entry(item).State = EntityState.Modified;
        _db.SaveChanges();

        return item;
    }

    public bool? DeleteBlog(int id)
    {
        var item = _db.TblBlogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id);
        if (item is null)
        {
            return null;
        }

        _db.Entry(item).State = EntityState.Deleted;
        int reslt = _db.SaveChanges();

        return reslt > 0;
    }




}
