using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Refit;
using RestSharp;
using System.Threading.Tasks;

namespace THDotNetTrainingBatch5.RestApi3.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BurmaProjectIdeaController : ControllerBase
{
    private readonly HttpClient _httpClient;
    private readonly RestClient _restClient;
    private readonly IArtGalleryApi _artGalleryApi;

    public BurmaProjectIdeaController(HttpClient httpClient, RestClient restClient, IArtGalleryApi artGalleryApi)
    {
        _httpClient = httpClient;
        _restClient = restClient;
        _artGalleryApi = artGalleryApi;
    }

    [HttpGet("birds")]
    public async Task<IActionResult> BirdsAsync([FromServices] HttpClient httpClient)
    {
        var response = await httpClient.GetAsync("birds");
        var str = await response.Content.ReadAsStringAsync();

        return Ok(str);
    }

    [HttpGet("pick-a-pile")]
    public async Task<IActionResult> PickAPileAsync()
    {
        RestRequest request = new RestRequest("pick-a-pile", Method.Get);
        var response = await _restClient.GetAsync(request);

        return Ok(response.Content);
    }

    [HttpGet("art-gallery")]
    public async Task<IActionResult> ArtGalleryAsync()
    {
        var response = await _artGalleryApi.GetArtGallery();

        return Ok(response);
    }
}

