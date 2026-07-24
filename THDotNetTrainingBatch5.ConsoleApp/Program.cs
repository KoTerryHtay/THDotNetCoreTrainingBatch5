// See https://aka.ms/new-console-template for more information
using System.Data;
using System.Data.SqlClient;
using THDotNetTrainingBatch5.ConsoleApp;

Console.WriteLine("Hello, World!");
//Console.ReadLine();

// C# => Database

// EFCore / Entity Framework

// nuget


// ADO.NET
//AdoDotNetExample adoDotNetExample = new AdoDotNetExample();
//adoDotNetExample.Read();
//adoDotNetExample.Create();
//adoDotNetExample.Edit();
//adoDotNetExample.Update();
//adoDotNetExample.Delete();

// Dapper
DapperExample dapperExample = new DapperExample();
//dapperExample.Read();
//dapperExample.Create("title 1", "author", "content");
//dapperExample.Edit(1);
//dapperExample.Edit(2);
dapperExample.Update(2,"title 1", "author", "content");
dapperExample.Delete(14);


Console.ReadKey();
