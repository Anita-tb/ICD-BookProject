using System.Threading.Tasks;
using ICD.BookProject.BusinessServiceContract;
using ICD.Framework.Abstraction.Session;
using ICD.FrameWork.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ICD.BookProject.Api.Controllers;

[Route("api/Users")]
[ApiController]
public class UserController : BaseController
{
    private readonly IUserService _userService;
    public UserController(IAppSession appSession , IUserService userService) : base(appSession)
    {
        _userService = userService;
    }

    [HttpPost("insert")]
    [AllowAnonymous]
    public async Task<IActionResult> InsertUserAsync([FromBody] InsertUserRequest request)
    {
        var result = await _userService.InsertUserAsync(request);
        return Ok(result);
    }
    
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> LoginUserAsync([FromBody] LoginUserRequest request)
    {
        var result = await _userService.LoginUserAsync(request);
        return Ok(result);
    }
    
    [HttpPost("update")]
    //[AllowAnonymous]
    public async Task<IActionResult> UpdateUserAsync([FromBody] UpdateUserRequest request)
    {
        var result = await _userService.UpdateUserAsync(request);
        return Ok(result);
    }
    
    [HttpPost("delete")]
    //[AllowAnonymous]
    public async Task<IActionResult> DeleteUserAsync([FromBody] DeleteTypeLongRequest request)
    {
        var result = await _userService.DeleteUserAsync(request);

        return Ok(result);
    }
    
    [HttpPost("get")]
    [AllowAnonymous]
    public async Task<IActionResult> GetUsersAsync([FromBody] GetUsersQuery query)
    {
        var result = await _userService.GetUsersAsync(query);
        return Ok(result);
    }
}