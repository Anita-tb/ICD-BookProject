using Microsoft.AspNetCore.Mvc;
using ICD.Framework.Abstraction.Session;
using ICD.FrameWork.Controllers;
using ICD.BookProject.BusinessServiceContract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using k8s.KubeConfigModels;
using Microsoft.AspNetCore.Http;

namespace ICD.BookProject.Api.Controllers;

[Route("api/Books")]
[ApiController]
public class BookController : BaseController
{
    private readonly IBookService _bookService;
    public BookController(IAppSession appSession , IBookService bookService) : base(appSession)
    {
        _bookService = bookService;
        
    }

    [HttpPost("insert")]
    //[AllowAnonymous]
    public async Task<IActionResult> InsertBookAsync([FromBody] InsertBookRequest request)
    {
       
        var result = await _bookService.InsertBookAsync(request);
        return Ok(result);
    }

    [HttpPost("update")]
    //[AllowAnonymous]
    public async Task<IActionResult> UpdateBookAsync([FromBody] UpdateBookRequest request)
    {
        var result = await _bookService.UpdateBookAsync(request);
        return Ok(result);
    }

    [HttpPost("delete")]
    //[AllowAnonymous]
    public async Task<IActionResult> DeleteBookAsync([FromBody] DeleteTypeIntRequest request)
    {
        var result = await _bookService.DeleteBookAsync(request);

        return Ok(result);
    }
    
    
    [HttpPost("get")]
    [AllowAnonymous]
    public async Task<IActionResult> GetBooksAsync([FromBody] GetBooksQuery query)
    {
        var result = await _bookService.GetBooksAsync(query);

        return Ok(result);
    }
    
    [HttpPost("get-by-key")]
    [AllowAnonymous]
    public async Task<IActionResult> GetByKeyBooksAsync([FromBody] GetByKeyBooksQuery query)
    {
        var result = await _bookService.GetByKeyBooksAsync(query);

        return Ok(result);
    }
    
    [HttpPost("append-author")]
    //[AllowAnonymous]
    public async Task<IActionResult> AppendAuthorAsync([FromBody] AppendAuthorRequest request)
    {
        var result = await _bookService.AppendAuthorAsync(request);

        return Ok(result);
    }
    
    [HttpPost("append-category")]
    //[AllowAnonymous]
    public async Task<IActionResult> AppendCategoryAsync([FromBody] AppendCategoryRequest request)
    {
        var result = await _bookService.AppendCategoryAsync(request);

        return Ok(result);
    }
}