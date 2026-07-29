using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace THDotNetTrainingBatch5.ConsoleApp3;

public class HttpClientExample
{
    private readonly HttpClient _client;
    private readonly string _postsEndpoint = "https://jsonplaceholder.typicode.com/posts";

    public HttpClientExample()
    {
        _client = new HttpClient();
    }

    public async Task ReadAsync()
    {
        var response = await _client.GetAsync(_postsEndpoint);
        if (response.IsSuccessStatusCode)
        {
            string jsonStr = await response.Content.ReadAsStringAsync();
            Console.WriteLine(jsonStr);
        }
    }

    public async Task Edit(int id)
    {
        var response = await _client.GetAsync($"{_postsEndpoint}/{id}");

        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            Console.WriteLine("No data found");
            return;
        }

        if (response.IsSuccessStatusCode)
        {
            string jsonStr = await response.Content.ReadAsStringAsync();
            Console.WriteLine(jsonStr);
            return;
        }
    }

    public async Task Create(string title, string body, int userId)
    {
        PostModel requestModel = new PostModel()
        {
            body = body,
            title = title,
            userId = userId
        };

        var jsonRequest = JsonConvert.SerializeObject(requestModel);
        var content = new StringContent(jsonRequest, Encoding.UTF8, Application.Json);
        var response = await _client.PostAsync(_postsEndpoint, content);
        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine(await response.Content.ReadAsStringAsync());
        }
    }

    public async Task Update(int id, string title, string body, int userId)
    {
        PostModel requestModel = new PostModel()
        {
            body = body,
            title = title,
            userId = userId
        };

        var jsonRequest = JsonConvert.SerializeObject(requestModel);
        var content = new StringContent(jsonRequest, Encoding.UTF8, Application.Json);
        var response = await _client.PutAsync($"{_postsEndpoint}/{id}", content);
        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine(await response.Content.ReadAsStringAsync());
        }
    }

    public async Task Delete(int id)
    {
        var response = await _client.DeleteAsync($"{_postsEndpoint}/{id}");
        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            Console.WriteLine("No data found");
            return;
        }
        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine(await response.Content.ReadAsStringAsync());
        }
    }
}

public class PostModel
{
    public int userId { get; set; }
    public int id { get; set; }
    public string title { get; set; }
    public string body { get; set; }
}
