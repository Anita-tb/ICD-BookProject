using System.Threading.Tasks;
using ICD.BookProject.BusinessServiceContract;
using ICD.Framework.Abstraction.Session;
using ICD.FrameWork.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace ICD.BookProject.Api.Controllers;

[Route("api/Options")]
[ApiController]
public class OptionController : BaseController
{
    private readonly IOptionService _optionService;

    public OptionController(IAppSession appSession, IOptionService optionService) : base(appSession)
    {
        _optionService = optionService;
    }
    
    [HttpPost("insert")]
    //[AllowAnonymous]
    public async Task<IActionResult> InsertOptionAsync([FromBody] InsertOptionRequest request)
    {
       
        var result = await _optionService.InsertOptionAsync(request);
        return Ok(result);
    }
    
    [HttpPost("get")]
    //[AllowAnonymous]
    public async Task<IActionResult> GetOptionsAsync([FromBody] GetOptionsQuery query)
    {
        var result = await _optionService.GetOptionsAsync(query);
        return Ok(result);
    }
    
    [HttpPost("update")]
    //[AllowAnonymous]
    public async Task<IActionResult> UpdateOptionAsync([FromBody] UpdateOptionRequest request)
    {
        var result = await _optionService.UpdateOptionAsync(request);
        return Ok(result);
    }
    
    [HttpPost("delete")]
    //[AllowAnonymous]
    public async Task<IActionResult> DeleteOptionAsync([FromBody] DeleteTypeIntRequest request)
    {
        var result = await _optionService.DeleteOptionAsync(request);

        return Ok(result);
    }
    
    
    
    
}