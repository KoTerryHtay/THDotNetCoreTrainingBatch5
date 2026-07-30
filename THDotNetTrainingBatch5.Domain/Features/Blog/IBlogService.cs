using THDotNetTrainingBatch5.Database.Models;
using THDotNetTrainingBatch5.Domain.Models;

namespace THDotNetTrainingBatch5.Domain.Features.Blog;

public interface IBlogService
{
    TblBlog CreateBlog(TblBlog blog);
    bool? DeleteBlog(int id);
    TblBlog GetBlog(int id);
    Result<ResultBlogResponseModel<List<TblBlog>>> GetBlogs();
    TblBlog PatchBlog(int id, TblBlog blog);
    BlogResponseModel UpdateBlog(int id, TblBlog blog);
}