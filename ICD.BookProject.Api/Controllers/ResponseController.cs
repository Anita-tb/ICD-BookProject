using System.Threading.Tasks;
using ICD.BookProject.BusinessServiceContract;
using ICD.Framework.Abstraction.Session;
using ICD.FrameWork.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace ICD.BookProject.Api.Controllers;

[Route("api/Responses")]
[ApiController]
public class ResponseController : BaseController
{
    private readonly IResponseService _responseService;
    public ResponseController(IAppSession appSession , IResponseService responseService) : base(appSession)
    {
        _responseService = responseService;
    }
    
    [HttpPost("insert")]
    //[AllowAnonymous]
    public async Task<IActionResult> InsertResponseAsync([FromBody] InsertResponseRequest request)
    {
        var result = await _responseService.InsertResponseAsync(request);
        return Ok(result);
    }
    
    [HttpPost("get")]
    //[AllowAnonymous]
    public async Task<IActionResult> GetResponsesAsync([FromBody] GetResponsesQuery query)
    {
        var result = await _responseService.GetResponsesAsync(query);
        return Ok(result);
    }
    
    [HttpPost("delete")]
    //[AllowAnonymous]
    public async Task<IActionResult> DeleteResponseAsync([FromBody] DeleteTypeIntRequest request)
    {
        var result = await _responseService.DeleteResponseAsync(request);

        return Ok(result);
    }
    
    
    
}