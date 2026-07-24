using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using THDotNetTrainingBatch5.ConsoleApp.Models;

namespace THDotNetTrainingBatch5.ConsoleApp
{
    public class EFCoreExample
    {
        public void Read()
        {
            AppDbContext db = new AppDbContext();
            var lst = db.Blog.Where(x => x.DeleteFlag == false).ToList();

            foreach (var item in lst)
            {
                Console.WriteLine(item.BlogId);
                Console.WriteLine(item.BlogTitle);
                Console.WriteLine(item.BlogAuthor);
                Console.WriteLine(item.BlogContent);
            }
        }

        public void Create(string title, string author, string content)
        {
            BlogDataModel blog = new BlogDataModel
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            };

            AppDbContext db = new AppDbContext();
            db.Blog.Add(blog);
            var result = db.SaveChanges();


            Console.WriteLine(result == 1 ? "Saving Successfully." : "Saving Failed.");
        }

        public void Edit(int id)
        {
            AppDbContext db = new AppDbContext();
            //db.Blog.Where(x => x.BlogId == id).FirstOrDefault();
            var item = db.Blog.FirstOrDefault(x => x.BlogId == id);

            if (item is null)
            {
                Console.WriteLine("No data found");
                return;
            }

            Console.WriteLine(item.BlogId);
            Console.WriteLine(item.BlogTitle);
            Console.WriteLine(item.BlogAuthor);
            Console.WriteLine(item.BlogContent);

        }

        public void Update(int id, string title, string author, string content)
        {
            AppDbContext db = new AppDbContext();
            // select * from Tbl_Blog with (nolock)
            var item = db.Blog
                .AsNoTracking()
                .FirstOrDefault(x => x.BlogId == id);

            if (item is null)
            {
                Console.WriteLine("No data found");
                return;
            }

            if (!string.IsNullOrEmpty(title))
            {
                item.BlogTitle = title;
            }

            if (!string.IsNullOrEmpty(author))
            {
                item.BlogAuthor = author;
            }
            if (!string.IsNullOrEmpty(content))
            {
                item.BlogContent = content;
            }

            db.Entry(item).State = EntityState.Modified;
            var result = db.SaveChanges();

            Console.WriteLine(result == 1 ? "Updating Successfully." : "Updating Failed.");
        }

        public void Delete(int id)
        {
            AppDbContext db = new AppDbContext();            
            var item = db.Blog
                .AsNoTracking()
                .FirstOrDefault(x => x.BlogId == id);

            if (item is null)
            {
                Console.WriteLine("No data found");
                return;
            }

            db.Entry(item).State = EntityState.Deleted;
            var result = db.SaveChanges();

            Console.WriteLine(result == 1 ? "Deleting Successfully." : "Deleting Failed.");
        }
    }
}
