using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using THDotNetTrainingBatch5.Domain.Models;
using THDotNetTrainingBatch5.RestApi.Controllers;

namespace THDotNetTrainingBatch5.RestApi.Endpoints;

[Route("api/[controller]")]
[ApiController]
public class BaseController : ControllerBase
{
    [NonAction]
    public IActionResult Execute(object model)
    {
        JObject jObj = JObject.Parse(JsonConvert.SerializeObject(model));

        if (jObj.ContainsKey("Response"))
        {
            BaseResponseModel baseResponseModel = JsonConvert.DeserializeObject<BaseResponseModel>(jObj["Response"]!.ToString())!;

            if (baseResponseModel.RespType.Equals(EnumRespType.SystemError)) return NotFound(model);

            return Ok(model);
        }
        return StatusCode(500, "Invalid Response Model. Please add BaseResponseModel to your ResponseModel.");


    }

    public IActionResult Execute<T>(Result<T> model)
    {

        if (model.IsValidationError) return NotFound(model);

        return Ok(model);

    }
}


public enum EnumRespType
{
    None,
    Success,
    ValidationError,
    SystemError
}