using System.Threading.Tasks;
using ICD.BookProject.BusinessServiceContract;
using ICD.Framework.Abstraction.Session;
using ICD.FrameWork.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace ICD.BookProject.Api.Controllers;


[Route("api/Libraries")]
[ApiController]
public class LibraryController : BaseController
{
    private readonly ILibraryService _libraryService;
    public LibraryController(IAppSession appSession , ILibraryService libraryService) : base(appSession)
    {
        _libraryService = libraryService;
    }
    
    [HttpPost("add-book")]
    //[AllowAnonymous]
    public async Task<IActionResult> AddBookAsync([FromBody] AddBookRequest request)
    {
        var result = await _libraryService.AddBookAsync(request);
        return Ok(result);
    }
    
    [HttpPost("update-currentpage")]
    //[AllowAnonymous]
    public async Task<IActionResult> UpdateCurrentPageAsync([FromBody] UpdateCurrentPageRequest request)
    {
        var result = await _libraryService.UpdateCurrentPageAsync(request);
        return Ok(result);
    }
    
    [HttpPost("get-my-books")]
    //[AllowAnonymous]
    public async Task<IActionResult> GetMyBooksAsync([FromBody] GetMyBooksQuery query)
    {
        var result = await _libraryService.GetMyBooksAsync(query);
        return Ok(result);
    }
    
    [HttpPost("delete")]
    //[AllowAnonymous]
    public async Task<IActionResult> RemoveBookAsync([FromBody] DeleteTypeIntRequest request)
    {
        var result = await _libraryService.RemoveBookAsync(request);

        return Ok(result);
    }
    
    
    
}