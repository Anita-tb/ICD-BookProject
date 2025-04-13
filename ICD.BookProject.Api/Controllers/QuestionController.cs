using System.Threading.Tasks;
using ICD.BookProject.BusinessServiceContract;
using ICD.Framework.Abstraction.Session;
using ICD.FrameWork.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace ICD.BookProject.Api.Controllers;

[Route("api/Questions")]
[ApiController]
public class QuestionController : BaseController
{
    private readonly IQuestionService _questionService;
    public QuestionController(IAppSession appSession , IQuestionService questionService) : base(appSession)
    {
        _questionService = questionService;
    }
    
    [HttpPost("insert")]
    //[AllowAnonymous]
    public async Task<IActionResult> InsertQuestionAsync([FromBody] InsertQuestionRequest request)
    {
        var result = await _questionService.InsertQuestionAsync(request);
        return Ok(result);
    }
    
    [HttpPost("get")]
    //[AllowAnonymous]
    public async Task<IActionResult> GetQuestionsAsync([FromBody] GetQuestionsQuery query)
    {
        var result = await _questionService.GetQuestionsAsync(query);
        return Ok(result);
    }
    
    [HttpPost("update")]
    //[AllowAnonymous]
    public async Task<IActionResult> UpdateQuestionAsync([FromBody] UpdateQuestionRequest request)
    {
        var result = await _questionService.UpdateQuestionAsync(request);
        return Ok(result);
    }
    
    [HttpPost("delete")]
    //[AllowAnonymous]
    public async Task<IActionResult> DeleteQuestionAsync([FromBody] DeleteTypeIntRequest request)
    {
        var result = await _questionService.DeleteQuestionAsync(request);

        return Ok(result);
    }
    
    
}