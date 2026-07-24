// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
using THDotNetTrainingBatch5.Database.Models;

Console.WriteLine("Hello, World! >>>");

var options = new DbContextOptionsBuilder<AppDbContext>()
    .UseSqlServer("Data Source=LAPTOP-0NOHR6LI;Initial Catalog=DotNetTrainingBatch5;User ID=sa;Password=sasa@123;TrustServerCertificate=True")
    .Options;

AppDbContext db = new AppDbContext(options);
var lst = db.TblBlogs.ToList();

foreach (var item in lst)
{
    Console.WriteLine(item.BlogId);
    Console.WriteLine(item.BlogTitle);
    Console.WriteLine(item.BlogAuthor);
    Console.WriteLine(item.BlogContent);
}
Console.ReadKey();