// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using THDotNetTrainingBatch5.Database.Models;

Console.WriteLine("Hello, World! >>>");

//var options = new DbContextOptionsBuilder<AppDbContext>()
//    .UseSqlServer("Data Source=LAPTOP-0NOHR6LI;Initial Catalog=DotNetTrainingBatch5;User ID=sa;Password=sasa@123;TrustServerCertificate=True")
//    .Options;

//AppDbContext db = new AppDbContext(options);
//var lst = db.TblBlogs.ToList();

//foreach (var item in lst)
//{
//    Console.WriteLine(item.BlogId);
//    Console.WriteLine(item.BlogTitle);
//    Console.WriteLine(item.BlogAuthor);
//    Console.WriteLine(item.BlogContent);
//}
//Console.ReadKey();


var blog = new BlogModel
{
    Id = 1,
    Title = "Test Title",
    Author = "Test Author",
    Content = "Test Content"
};

string jsonStr2 = """{"Id": 1,"Title": "Test Title","Author": "Test Author","Content": "Test Content"}""";

var obj = JsonConvert.DeserializeObject<BlogModel>(jsonStr2);

//System.Text.Json.JsonSerializer.Serialize(obj);
//System.Text.Json.JsonSerializer.Deserialize<BlogModel>(jsonStr2);

string jsonStr = blog.ToJson();
Console.WriteLine(jsonStr);
Console.WriteLine(obj.Title);

public class BlogModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string Content { get; set; }
}

public static class Extensions // DevCode
{
    public static string ToJson(this object obj)
    {
        string jsonStr = JsonConvert.SerializeObject(obj, Formatting.Indented);
        return jsonStr;
    }

    public static string ToObj(this object obj)
    {
        string jsonStr = JsonConvert.SerializeObject(obj, Formatting.Indented);
        return jsonStr;
    }
}