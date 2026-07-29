// See https://aka.ms/new-console-template for more information
using THDotNetTrainingBatch5.ConsoleApp3;

Console.WriteLine("Hello, World!");

//HttpClientExample httpClient = new HttpClientExample();
//await httpClient.ReadAsync();
//await httpClient.Edit(1);
//await httpClient.Edit(101);
//await httpClient.Create("title", "body", 1);
//await httpClient.Update(10, "title test", "body test", 1);
//await httpClient.Delete(10);

//RestClientExample restClient = new RestClientExample();
//await restClient.Read();

RefitExample refit = new RefitExample();
await refit.Run();

Console.ReadKey();
