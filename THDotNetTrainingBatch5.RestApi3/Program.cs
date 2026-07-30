using Refit;
using RestSharp;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// HttpClient Dependency Injection
builder.Services.AddSingleton(n => new HttpClient()
{
    BaseAddress = new Uri(builder.Configuration.GetSection("ApiDomainUrl").Value!)
});

// RestSharp Dependency Injection
builder
    .Services
    .AddSingleton(n => new RestClient(builder.Configuration.GetSection("ApiDomainUrl").Value!) { });

// Refit Dependency Injection
builder.Services
    .AddRefitClient<IArtGalleryApi>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri(builder.Configuration.GetSection("ApiDomainUrl").Value!));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public interface IArtGalleryApi
{
    [Get("/art-gallery")]
    Task<List<ArtGalleryModel>> GetArtGallery();
}

public class ArtGalleryModel
{
    public int ArtId { get; set; }
    public string ArtName { get; set; }
    public string ArtDescription { get; set; }
    public string ArtImageUrl { get; set; }
}