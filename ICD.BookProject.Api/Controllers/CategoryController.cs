using System.Threading.Tasks;
using ICD.BookProject.BusinessServiceContract;
using ICD.Framework.Abstraction.Session;
using ICD.FrameWork.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace ICD.BookProject.Api.Controllers;


[Route("api/Categories")]
[ApiController]
public class CategoryController : BaseController
{
    private readonly ICategoryService _categoryService;
    public CategoryController(IAppSession appSession , ICategoryService categoryService) : base(appSession)
    {
        _categoryService = categoryService;
    }
    
    [HttpPost("insert")]
    //[AllowAnonymous]
    public async Task<IActionResult> InsertCategoryAsync([FromBody] InsertCategoryRequest request)
    {
        var result = await _categoryService.InsertCategoryAsync(request);
        return Ok(result);
    }
    
    [HttpPost("update")]
    //[AllowAnonymous]
    public async Task<IActionResult> UpdateCategoryAsync([FromBody] UpdateCategoryRequest request)
    {
        var result = await _categoryService.UpdateCategoryAsync(request);
        return Ok(result);
    }
    
    [HttpPost("get")]
    //[AllowAnonymous]
    public async Task<IActionResult> GetCategoriesAsync([FromBody] GetCategoriesQuery query)
    {
        var result = await _categoryService.GetCategoriesAsync(query);
        return Ok(result);
    }
    
    [HttpPost("delete")]
    //[AllowAnonymous]
    public async Task<IActionResult> DeleteCategoryAsync([FromBody] DeleteTypeIntRequest request)
    {
        var result = await _categoryService.DeleteCategoryAsync(request);

        return Ok(result);
    }
}