using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using THDotNetTrainingBatch5.RestApi.DataModels;
using THDotNetTrainingBatch5.RestApi.ViewModels;

namespace THDotNetTrainingBatch5.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsDapperController : ControllerBase
    {
        private readonly string _connectionString = "Data Source=LAPTOP-0NOHR6LI;Initial Catalog=DotNetTrainingBatch5;User ID=sa;Password=sasa@123;TrustServerCertificate=True";

        [HttpGet]
        public IActionResult GetBlogs()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = "select * from tbl_blog where DeleteFlag = 0";
                var lst = db.Query<BlogDataModel>(query).ToList();
                //foreach (var item in lst)
                //{
                //    Console.WriteLine(item.BlogId);
                //    Console.WriteLine(item.BlogTitle);
                //    Console.WriteLine(item.BlogAuthor);
                //    Console.WriteLine(item.BlogContent);
                //}

                return Ok(lst);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = "select * from tbl_blog where DeleteFlag = 0 and BlogId = @BlogId";
                var item = db.Query<BlogDataModel>(query, new BlogDataModel
                {
                    BlogId = id
                }).FirstOrDefault();

                if (item is null)
                {
                    return NotFound("No data found");
                }

                //Console.WriteLine(item.BlogId);
                //Console.WriteLine(item.BlogTitle);
                //Console.WriteLine(item.BlogAuthor);
                //Console.WriteLine(item.BlogContent);

                return Ok(item);
            }
        }

        [HttpPost]
        public IActionResult CreateBlogs(BlogViewModel blog)
        {
            string query = @"INSERT INTO [dbo].[Tbl_Blog]
                    ([BlogTitle]
                    ,[BlogAuthor]
                    ,[BlogContent]
                    ,[DeleteFlag])
                VALUES
                    (@BlogTitle
                    ,@BlogAuthor
                    ,@BlogContent
                    ,0)";

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                int result = db.Execute(query, new BlogDataModel
                {
                    BlogTitle = blog.Title,
                    BlogAuthor = blog.Author,
                    BlogContent = blog.Content
                });

                return Ok(result == 1 ? "Saving Successfully." : "Saving Failed.");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlogs(int id, BlogViewModel blog)
        {
            string query = @"UPDATE [dbo].[Tbl_Blog]
                   SET [BlogTitle] = @BlogTitle
                      ,[BlogAuthor] = @BlogAuthor
                      ,[BlogContent] = @BlogContent
                      ,[DeleteFlag] = 0
                 WHERE BlogId = @BlogId";

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                int result = db.Execute(query, new BlogDataModel
                {
                    BlogId = id,
                    BlogTitle = blog.Title,
                    BlogAuthor = blog.Author,
                    BlogContent = blog.Content
                });

                return Ok(result == 1 ? "Saving Successfully." : "Saving Failed.");

            }
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlogs(int id, BlogViewModel blog)
        {
            string conditions = "";
            if (!string.IsNullOrEmpty(blog.Title))
            {
                conditions += " [BlogTitle] = @BlogTitle, ";
            }
            if (!string.IsNullOrEmpty(blog.Author))
            {
                conditions += " [BlogAuthor] = @BlogAuthor, ";
            }
            if (!string.IsNullOrEmpty(blog.Content))
            {
                conditions += " [BlogContent] = @BlogContent, ";
            }

            if (conditions.Length == 0)
            {
                return BadRequest("Invalid Parameter");
            }

            conditions = conditions.Substring(0, conditions.Length - 2);

            string query = $@"UPDATE [dbo].[Tbl_Blog]
            SET {conditions}
            WHERE BlogId = @BlogId";

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                int result = db.Execute(query, new BlogDataModel
                {
                    BlogId = id,
                    BlogTitle = blog.Title,
                    BlogAuthor = blog.Author,
                    BlogContent = blog.Content
                });

                return Ok(result == 1 ? "Updating Successfully." : "Updating Failed.");
            }

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlogs(int id)
        {
            string query = @"UPDATE [dbo].[Tbl_Blog]
                   SET [DeleteFlag] = 1
                 WHERE BlogId = @BlogId";

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                int result = db.Execute(query, new BlogDataModel
                {
                    BlogId = id,
                });

                return Ok(result == 1 ? "Delete Successfully." : "Delete Failed.");

            }
        }
    }
}
