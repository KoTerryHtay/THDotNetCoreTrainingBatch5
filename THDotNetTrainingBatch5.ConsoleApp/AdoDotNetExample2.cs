using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using THDotNetTrainingBatch5.Shared;

namespace THDotNetTrainingBatch5.ConsoleApp
{
    public class AdoDotNetExample2
    {
        private readonly string _connectionString = "Data Source=LAPTOP-0NOHR6LI;Initial Catalog=DotNetTrainingBatch5;User ID=sa;Password=sasa@123;";

        private readonly AdoDotNetService _adoDotNetService;

        private record BlogConfig(string id, string title, string author, string content);
        private readonly BlogConfig blog = new("BlogId", "BlogTitle", "BlogAuthor", "BlogContent");

        public AdoDotNetExample2()
        {
            _adoDotNetService = new AdoDotNetService(_connectionString);
        }

        public void Read()
        {
            string query = @"SELECT [BlogId]
                            ,[BlogTitle]
                            ,[BlogAuthor]
                            ,[BlogContent]
                            ,[DeleteFlag]
                        FROM [dbo].[Tbl_Blog] where DeleteFlag = 0";

            var dt = _adoDotNetService.Query(query);

            foreach (DataRow dr in dt.Rows)
            {
                Console.WriteLine(dr["BlogId"]);
                Console.WriteLine(dr["BlogTitle"]);
                Console.WriteLine(dr["BlogAuthor"]);
                Console.WriteLine(dr["BlogContent"]);
            }
        }

        public void Edit()
        {
            Console.Write("Blog Id: ");
            string id = Console.ReadLine();

            string query = @"SELECT [BlogId]
                            ,[BlogTitle]
                            ,[BlogAuthor]
                            ,[BlogContent]
                            ,[DeleteFlag]
                        FROM [dbo].[Tbl_Blog] where BlogID = @BlogId";

            var dt = _adoDotNetService.Query(query, new SqlParameterModel(blog.id, $"{blog.id}"));

            DataRow dr = dt.Rows[0];
            //Console.WriteLine(dr["BlogId"]);
            //Console.WriteLine(dr["BlogTitle"]);
            //Console.WriteLine(dr["BlogAuthor"]);
            //Console.WriteLine(dr["BlogContent"]);
            Console.WriteLine(dr[blog.id]);
            Console.WriteLine(dr[blog.title]);
            Console.WriteLine(dr[blog.author]);
            Console.WriteLine(dr[blog.content]);

        }

        public void Create()
        {
            Console.WriteLine("Blog Title: ");
            string title = Console.ReadLine();

            Console.WriteLine("Blog Author: ");
            string author = Console.ReadLine();

            Console.WriteLine("Blog Content: ");
            string content = Console.ReadLine();

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
            // new SqlParameterModel{ Name = blog.title,Value = $"@{blog.title}"}
            int result = _adoDotNetService.Execxute(query,
                new SqlParameterModel(blog.title, $"@{blog.title}"),
                new SqlParameterModel(blog.author, $"@{blog.author}"),
                new SqlParameterModel(blog.content, $"@{blog.content}")
            );

            Console.WriteLine(result == 1 ? "Saving Successfully." : "Saving Failed.");
        }
    }
}
