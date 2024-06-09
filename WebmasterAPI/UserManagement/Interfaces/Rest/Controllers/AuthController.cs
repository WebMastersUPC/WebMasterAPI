using System.Net.Mime;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebmasterAPI.Authentication.Domain.Services;
using WebmasterAPI.Authentication.Domain.Services.Communication;
using WebmasterAPI.Authentication.Resources;
using WebmasterAPI.Models;

namespace WebmasterAPI.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
[Produces(MediaTypeNames.Application.Json)]

public class AuthController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    
    public AuthController(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    
    [HttpPost("register-developer")]
    public async Task<IActionResult> RegisterDeveloper(RegisterDeveloperRequest model)
    {
        try
        {
            await _userService.RegisterDeveloperAsync(model);
            return Ok(new { message = "Registration succesful", status = "200"});
        }
        catch (Exception e)
        {
            return BadRequest(new { message = e.InnerException?.Message ?? e.Message });
        }
    }
    
    [HttpPost("register-enterprise")]
    public async Task<IActionResult> RegisterEnterprise(RegisterEnterpriseRequest model)
    {
        try
        {
            await _userService.RegisterEnterpriseAsync(model);
            return Ok(new { message = "Registration succesful", status = "200" });
        }
        catch (Exception e)
        {
            return BadRequest(new { message = e.InnerException?.Message ?? e.Message });
        }
    }
    
    [AllowAnonymous]
    [HttpPost("sign-in")]
    public async Task<IActionResult> Authenticate(AuthenticateRequest request)
    {
        var response = await _userService.AuthenticateAsync(request);
        return Ok(response);
    }
    
    [AllowAnonymous]
    [HttpGet("sign-in/{Mail}&{Password}")]
    public async Task<IActionResult> Authenticate(string Mail, string Password)
    {
        AuthenticateRequest request = new AuthenticateRequest
        {
            Mail = Mail,
            Password = Password
        };
        var response = await _userService.AuthenticateAsync(request);
        return Ok(response);
    }

}