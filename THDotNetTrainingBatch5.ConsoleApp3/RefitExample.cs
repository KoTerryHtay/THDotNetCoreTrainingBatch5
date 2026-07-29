using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace THDotNetTrainingBatch5.ConsoleApp3
{
    public class RefitExample
    {
        public async Task Run()
        {
            var blogApi = RestService.For<IBlogApi>("https://localhost:7172");
            var lst = await blogApi.GetBlogs();
            foreach (var item in lst)
            {
                Console.WriteLine(item.BlogTitle);
            }

            var item2 = await blogApi.GetBlog(2);
            try
            {
                var item3 = await blogApi.GetBlog(100);

            }
            catch (ApiException ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    Console.WriteLine("No data found");
                }
            }

            var item4 = await blogApi.CreateBlog(new BlogModel
            {
                BlogTitle = "Title 1",
                BlogAuthor = "Author 1",
                BlogContent = "Content 1",
            });
            Console.WriteLine(item4);
        }

    }
}
