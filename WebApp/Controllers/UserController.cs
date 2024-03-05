using Application.Features.Auth.Commands.Register;
using Application.Features.Auth.Dtos;
using Application.Features.Users.Commands.Delete;
using Application.Features.Users.Queries.GetList;
using Application.Services.Repositories;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Core.Security.Dtos;
using Core.Security.Entities;
using Microsoft.AspNetCore.Mvc;
using Persistence.Repositories;

namespace WebApp.Controllers;

public class UserController : BaseController
{
    IOperationClaimRepository _operationClaimRepository;
    IUserOperationClaimRepository _userOperationClaimRepository;

    public UserController(IOperationClaimRepository operationClaimRepository, IUserOperationClaimRepository userOperationClaimRepository)
    {
        _operationClaimRepository = operationClaimRepository;
        _userOperationClaimRepository = userOperationClaimRepository;
    }

    public async Task<IActionResult> GetList()
    {
        GetListUserQuery getListUserQuery = new() { PageRequest = PageRequest };
        GetListResponse<GetListUserListItemDto> response =await Mediator.Send(getListUserQuery);

        return View(response);

    }
    public async Task<IActionResult> Add()
    {
        Paginate<OperationClaim> operationClaims = await _operationClaimRepository.GetListAsync();
        ViewBag.OperationClaims = operationClaims.Items;
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Add(UserForRegisterDto userForRegisterDto)
    {
       

         RegisterCommand registerCommand = new()
        {
            UserForRegisterDto = userForRegisterDto,
            IpAddress = GetIpAddress(),
        };

        RegisteredDto registeredDto = await Mediator.Send(registerCommand);
        UserOperationClaim userOperationClaim = new(5, 3);


        await _userOperationClaimRepository.AddAsync(userOperationClaim);
        return View();
        
    }
    public async Task<IActionResult> Delete(int id)
    {
        DeleteUserCommand deleteUserCommand = new() { Id = id };

        DeletedUserResponse response =await Mediator.Send(deleteUserCommand);

        return RedirectToAction("GetList");
    }
}
