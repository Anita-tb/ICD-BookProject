using System.Threading.Tasks;
using ICD.BookProject.BusinessServiceContract;
using ICD.Framework.Abstraction.Session;
using ICD.FrameWork.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace ICD.BookProject.Api.Controllers;


[Route("api/Questionnaires")]
[ApiController]
public class QuestionnaireController : BaseController
{
    private readonly IQuestionnarieService _questionnaireService;
    
    public QuestionnaireController(IAppSession appSession , IQuestionnarieService questionnaireService) : base(appSession)
    {
        _questionnaireService = questionnaireService;
    }
    
    [HttpPost("get")]
    //[AllowAnonymous]
    public async Task<IActionResult> GetQuestionnairesAsync([FromBody] GetQuestionnairesQuery query)
    {
        var result = await _questionnaireService.GetQuestionnairesAsync(query);
        return Ok(result);
    }
    
    [HttpPost("insert")]
    //[AllowAnonymous]
    public async Task<IActionResult> InsertQuestionnaireAsync([FromBody] InsertQuestionnaireRequest request)
    {
        var result = await _questionnaireService.InsertQuestionnaireAsync(request);
        return Ok(result);
    }
    
    [HttpPost("update")]
    //[AllowAnonymous]
    public async Task<IActionResult> UpdateQuestionnaireAsync([FromBody] UpdateQuestionnaireRequest request)
    {
        var result = await _questionnaireService.UpdateQuestionnaireAsync(request);
        return Ok(result);
    }
    
    [HttpPost("delete")]
    //[AllowAnonymous]
    public async Task<IActionResult> DeleteQuestionnaireAsync([FromBody] DeleteTypeIntRequest request)
    {
        var result = await _questionnaireService.DeleteQuestionnaireAsync(request);

        return Ok(result);
    }

    
}