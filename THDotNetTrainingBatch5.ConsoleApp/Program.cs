// See https://aka.ms/new-console-template for more information
using System.Data;
using System.Data.SqlClient;
using THDotNetTrainingBatch5.ConsoleApp;

Console.WriteLine("Hello, World!");
//Console.ReadLine();

// md => markdown
// C# => Database

// ADO.NET
// Dapper
// EFCore / Entity Framework

// nuget

AdoDotNetExample adoDotNetExample = new AdoDotNetExample();
//adoDotNetExample.Read();
//adoDotNetExample.Create();
adoDotNetExample.Edit();


Console.ReadKey();
