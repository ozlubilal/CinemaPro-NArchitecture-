using Application.Features.Auth.Commands.Login;
using Application.Features.Auth.Commands.Register;
using Application.Features.Auth.Dtos;
using Core.Security.Dtos;
using Core.Security.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

public class AuthController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
    {
        RegisterCommand registerCommand = new()
        {
            UserForRegisterDto = userForRegisterDto,
            IpAddress = GetIpAddress(),
        };

        RegisteredDto registeredDto =await Mediator.Send(registerCommand);
        return View(registeredDto);
    }
    [HttpGet]
    public async Task<IActionResult> Login()
    {
        
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
    {
        LoginCommand loginCommand = new()
        {
            UserForLoginDto = userForLoginDto,
            IpAddress = GetIpAddress(),
        };

        LoginedDto loginedDto =await Mediator.Send(loginCommand);
        if (loginedDto != null)
        {
            // Access Token'i Response Header'a ekleyerek tarayıcıya gönder
            Response.Headers.Add("Authorization", "Bearer " + loginedDto.AccessToken.Token);

            // Refresh Token'i Cookie olarak tarayıcıya yükle
            SetRefreshTokenToCookie(loginedDto.RefreshToken);

            // JWT tokeni cookie'ye ekle
            Response.Cookies.Append("accessToken", loginedDto.AccessToken.Token);
        }

        if (loginedDto.OperationClaimId == 1)
            return RedirectToAction("Index", "Admin");
        if (loginedDto.OperationClaimId == 3)
            return RedirectToAction("Index","UserHome");
        else
            return RedirectToAction("Login", "Auth");
       // return View(loginedDto);
    }

    private void SetRefreshTokenToCookie(RefreshToken refreshToken)
    {
        CookieOptions cookieOptions = new() { HttpOnly = true, Expires = DateTime.Now.AddDays(7) };
        Response.Cookies.Append("refreshToken", refreshToken.Token, cookieOptions);
    }
}
