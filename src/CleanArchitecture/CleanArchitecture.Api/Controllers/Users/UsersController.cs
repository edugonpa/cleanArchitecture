using System.Net;
using Asp.Versioning;
using CleanArchitecture.Api.Utils;
using CleanArchitecture.Application.Users.GetUsersDapperPagination;
using CleanArchitecture.Application.Users.GetUsersPagination;
using CleanArchitecture.Application.Users.LoginUser;
using CleanArchitecture.Application.Users.RegisterUser;
using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Users;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Api.Controllers.Users;

[ApiController]
// [ApiVersion(ApiVersions.V1, Deprecated = true)]
[ApiVersion(ApiVersions.V1)]
// [ApiVersion(ApiVersions.V2)]
[Route("api/v{version:apiVersion}/users")]
public class UserController : ControllerBase
{
    private readonly ISender _sender;

    public UserController(ISender sernder)
    {
        _sender = sernder;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    [MapToApiVersion(ApiVersions.V1)]
    public async Task<IActionResult> LoginV1(
        [FromBody] LoginUserRequest request,
        CancellationToken cancellationToken
    )
    {
        var command = new LoginCommand(request.Email, request.Password);
        var result = await _sender.Send(command, cancellationToken);

        if(result.IsFailure)
        {
            return Unauthorized(result.Error);
        }

        return Ok(result.Value);
    }

    // [AllowAnonymous]
    // [HttpPost("login")]
    // [MapToApiVersion(ApiVersions.V2)]
    // public async Task<IActionResult> LoginV2(
    //     [FromBody] LoginUserRequest request,
    //     CancellationToken cancellationToken
    // )
    // {
    //     var command = new LoginCommand(request.Email, request.Password);
    //     var result = await _sender.Send(command, cancellationToken);

    //     if(result.IsFailure)
    //     {
    //         return Unauthorized(result.Error);
    //     }

    //     return Ok(result.Value);
    // }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register(
        [FromBody] RegisterUserRequest request,
        CancellationToken cancellationToken
    )
    {
        var command = new RegisterUserCommand(
            request.Email, 
            request.Nombre,
            request.Apellidos,
            request.Password);
            
        var result = await _sender.Send(command, cancellationToken);

        if(result.IsFailure)
        {
            return Unauthorized(result.Error);
        }

        return Ok(result.Value);
    }

    [AllowAnonymous]
    [HttpGet("getPagination", Name = "PaginationUsers")]
    [ProducesResponseType(typeof(PagedResults<User, UserId>), 
        (int)HttpStatusCode.OK
        )]
    public async Task<ActionResult<PagedResults<User, UserId>>> GetPagination(
        [FromQuery] GetUsersPaginationQuery paginationQuery
    )
    {
        var resultados = await _sender.Send(paginationQuery);
        return Ok(resultados);
    }

    [AllowAnonymous]
    [HttpGet("getPaginationDapper", Name = "GetPaginationDapper")]
    [ProducesResponseType(typeof(PagedDapperResults<UserPaginationData>), 
        (int)HttpStatusCode.OK
        )]
    public async Task<ActionResult<PagedDapperResults<UserPaginationData>>> getPaginationDapper(
        [FromQuery] GetUsersDapperPaginationQuery paginationQuery
    )
    {
        var resultados = await _sender.Send(paginationQuery);
        return Ok(resultados);
    }
}