using THDotNetTrainingBatch5.Database.Models;

namespace THDotNetTrainingBatch5.Domain.Models;

public class BlogResponseModel
{
    public BaseResponseModel Response { get; set; }
    public TblBlog TblBlog { get; set; }
}

public class ResultBlogResponseModel<T>
{
    public T TblBlog { get; set; }
}

public class ResultBlogResponseModel1
{
    public List<TblBlog> TblBlog { get; set; }
}