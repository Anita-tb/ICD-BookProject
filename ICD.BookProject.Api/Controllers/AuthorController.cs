using System.Threading.Tasks;
using ICD.BookProject.BusinessServiceContract;
using ICD.Framework.Abstraction.Session;
using ICD.FrameWork.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ICD.BookProject.Api.Controllers;

[Route("api/Authors")]
[ApiController]
public class AuthorController : BaseController
{
    private readonly IAuthorService _authorService;
    public AuthorController(IAppSession appSession , IAuthorService authorService) : base(appSession)
    {
        _authorService = authorService;
    }

    [HttpPost("insert")]
    //[AllowAnonymous]
    public async Task<IActionResult> InsertAuthorAsync([FromBody] InsertAuthorRequest request)
    {
        var result = await _authorService.InsertAuthorAsync(request);
        return Ok(result);
    }
    
    [HttpPost("update")]
    //[AllowAnonymous]
    public async Task<IActionResult> UpdateAuthorAsync([FromBody] UpdateAuthorRequest request)
    {
        var result = await _authorService.UpdateAuthorAsync(request);
        return Ok(result);
    }
    
    [HttpPost("get")]
    //[AllowAnonymous]
    public async Task<IActionResult> GetAuthorsAsync([FromBody] GetAuthorsQuery query)
    {
        var result = await _authorService.GetAuthorsAsync(query);
        return Ok(result);
    }
    
    [HttpPost("delete")]
    //[AllowAnonymous]
    public async Task<IActionResult> DeleteAuthorAsync([FromBody] DeleteTypeIntRequest request)
    {
        var result = await _authorService.DeleteAuthorAsync(request);

        return Ok(result);
    }
}