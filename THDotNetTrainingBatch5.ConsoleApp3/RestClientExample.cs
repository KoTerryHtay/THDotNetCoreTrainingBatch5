using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace THDotNetTrainingBatch5.ConsoleApp3
{
    public class RestClientExample
    {

        private readonly RestClient _client;
        private readonly string _postsEndpoint = "https://jsonplaceholder.typicode.com/posts";

        public RestClientExample()
        {
            _client = new RestClient();
        }

        public async Task Read()
        {
            RestRequest request = new RestRequest(_postsEndpoint, Method.Get);
            var response = await _client.GetAsync(request);
            if (response.IsSuccessStatusCode && response.Content is not null)
            {
                string jsonStr = response.Content;
                Console.WriteLine(jsonStr);
            }
        }

        public async Task Edit(int id)
        {
            RestRequest request = new RestRequest($"{_postsEndpoint}/{id}", Method.Get);
            var response = await _client.GetAsync(request);

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                Console.WriteLine("No data found");
                return;
            }

            if (response.IsSuccessStatusCode && response.Content is not null)
            {
                string jsonStr = response.Content;
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

            RestRequest request = new RestRequest(_postsEndpoint, Method.Post);
            request.AddJsonBody(requestModel);

            var response = await _client.PostAsync(request);
            if (response.IsSuccessStatusCode && response.Content is not null)
            {
                Console.WriteLine(response.Content);
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

            RestRequest request = new RestRequest($"{_postsEndpoint}/{id}", Method.Patch);
            request.AddJsonBody(requestModel);

            var response = await _client.PatchAsync(request);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.Content);
            }
        }

        public async Task Delete(int id)
        {
            RestRequest request = new RestRequest($"{_postsEndpoint}/{id}", Method.Delete);
            var response = await _client.ExecuteAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                Console.WriteLine("No data found");
                return;
            }
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.Content);
            }
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
