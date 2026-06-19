using Legacy.Profile.Api.ErrorHandling;
using Legacy.Profile.Api.ErrorHandling.FailureResult;
using Legacy.Profile.Api.Mapping;
using Legacy.Profile.Api.Routing;
using Legacy.Profile.Application.Services.Profile;
using Legacy.Profile.Application.Services.Profile.Result;
using Legacy.Profile.Contracts.Request;
using Legacy.Profile.Contracts.Response;
using Legacy.Shared;
using Microsoft.AspNetCore.Mvc;
using Legacy.Profile.Application.Common;

namespace Legacy.Profile.Api.Controllers;

public class ProfileController : SystemController
{
    private readonly IProfileService _profileService;

    public ProfileController(IProfileService profileService)
    {
        _profileService = profileService ?? throw new ArgumentNullException(nameof(_profileService));
    }

    [HttpPost(ApiRoute.Create)]
    [ProducesResponseType(typeof(ProfileResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(ProfileRequest request, CancellationToken cancellationToken)
    {
        var result = await _profileService.CreateAsync(request.Map(), cancellationToken);

        return result.Match<IActionResult>(Created, failure => Problem(failure.Errors));
    }

    [HttpPost(ApiRoute.Update)]
    [ProducesResponseType(typeof(UpdateProfileResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(UpdateProfileRequest request, CancellationToken cancellationToken)
    {
        var result = await _profileService.UpdateAsync(request.Map(), cancellationToken);

        return result.Match<IActionResult>(Updated, 
            failure => Problem(failure.Errors));
    }

    [HttpGet(ApiRoute.GetAll)]
    [ProducesResponseType(typeof(GetAllResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAll([FromQuery] GetAllRequest request, CancellationToken cancellationToken)
    {
        var options = request.Map();

        if (options is null)
            return Problem(statusCode: StatusCodes.Status400BadRequest, 
                title: GeneralFailureResult.Throw(ErrorCode.Filter));

        var result = await _profileService.GetAllAsync(options, cancellationToken);
        var recordCount = await _profileService.GetRecordCountAsync(options, cancellationToken);

        return result.Match<IActionResult>(
            success => Ok(success.Map(request.Page, request.PageSize, recordCount)), 
            failure => Problem(failure.Errors)
        );
    }

    [HttpGet(ApiRoute.Get)]
    [ProducesResponseType(typeof(ProfileResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetById([FromRoute] string userId, CancellationToken cancellationToken)
    {
        var isValidUserId = int.TryParse(userId, out var convertedId);

        if (!isValidUserId) 
            return Problem(statusCode: StatusCodes.Status400BadRequest, 
                title: GeneralFailureResult.Throw(ErrorCode.Filter));

        int id = Convert.ToInt32(userId);
        var result = await _profileService.GetByIdAsync(id, cancellationToken);

        return result.Match<IActionResult>(
                success => Ok(success.Map()), 
                failure => Problem(failure.Errors)
            );
    }

    private ObjectResult Created(CreateProfileResult result)
    {
        var response = result.Profile.Map();

        return CreatedAtAction(nameof(GetById), new
        {
            id = response.UserId,
        }, response);
    }

    private ObjectResult Updated(UpdateProfileResult result)
    {
        var response = result.Profile.Map();

        return CreatedAtAction(nameof(GetById), new
        {
            idOrGuid = response.UserId,
        }, response);
    }
}
