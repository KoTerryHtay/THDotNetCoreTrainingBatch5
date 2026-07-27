using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using THDotNetTrainingBatch5.Database.Models;

namespace THDotNetTrainingBatch5.Domain.Features.Blog;

// Business Logic + Data Access Layer
public class BlogService
{
    private readonly AppDbContext _db = new AppDbContext();

    public List<TblBlog> GetBlogs()
    {
        var model = _db.TblBlogs.AsNoTracking().ToList();

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

    public TblBlog UpdateBlog(int id, TblBlog blog)
    {
        var item = _db.TblBlogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id);
        if (item is null)
        {
            return null;
        }

        item.BlogTitle = blog.BlogTitle;
        item.BlogAuthor = blog.BlogAuthor;
        item.BlogContent = blog.BlogContent;

        _db.Entry(item).State = EntityState.Modified;
        _db.SaveChanges();

        return item;
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
