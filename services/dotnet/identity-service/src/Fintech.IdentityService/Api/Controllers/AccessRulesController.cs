using Fintech.IdentityService.Api.Requests;
using Fintech.IdentityService.Api.Responses;
using Fintech.IdentityService.Application.AccessRules.CreateAccessRule;
using Fintech.IdentityService.Application.AccessRules.GetAccessRuleById;
using Fintech.IdentityService.Application.AccessRules.ListAccessRulesBySubject;
using Fintech.IdentityService.Domain.Enums;
using Fintech.IdentityService.Domain.ValueObjects;
using Microsoft.AspNetCore.Mvc;

namespace Fintech.IdentityService.Api.Controllers;

[ApiController]
[Route("api/access-rules")]
public sealed class AccessRulesController : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<AccessRuleResponse>> Create(
        [FromBody] CreateAccessRuleRequest request,
        [FromServices] CreateAccessRuleHandler handler)
    {
        var result = await handler.HandleAsync(
            new CreateAccessRuleCommand(
                request.SubjectType,
                request.SubjectId,
                request.Effect,
                request.EndpointPattern,
                request.HttpVerb),
            HttpContext.RequestAborted);

        var response = new AccessRuleResponse(
            result.AccessRuleId,
            result.SubjectType,
            result.SubjectId,
            result.Effect,
            result.EndpointPattern,
            result.HttpVerb,
            result.IsActive,
            result.CreatedAtUtc);

        return CreatedAtAction(nameof(GetById), new { accessRuleId = response.AccessRuleId }, response);
    }

    [HttpGet("{accessRuleId:guid}")]
    public async Task<ActionResult<AccessRuleResponse>> GetById(
        Guid accessRuleId,
        [FromServices] GetAccessRuleByIdHandler handler)
    {
        var result = await handler.HandleAsync(
            new GetAccessRuleByIdQuery(AccessRuleId.From(accessRuleId)),
            HttpContext.RequestAborted);

        if (result is null)
        {
            return NotFound();
        }

        return Ok(new AccessRuleResponse(
            result.AccessRuleId,
            result.SubjectType,
            result.SubjectId,
            result.Effect,
            result.EndpointPattern,
            result.HttpVerb,
            result.IsActive,
            result.CreatedAtUtc));
    }

    [HttpGet("subjects/{subjectType}/{subjectId:guid}")]
    public async Task<ActionResult<IReadOnlyList<AccessRuleResponse>>> ListBySubject(
        AccessRuleSubjectType subjectType,
        Guid subjectId,
        [FromServices] ListAccessRulesBySubjectHandler handler)
    {
        var results = await handler.HandleAsync(
            new ListAccessRulesBySubjectQuery(subjectType, subjectId),
            HttpContext.RequestAborted);

        return Ok(results.Select(result => new AccessRuleResponse(
            result.AccessRuleId,
            result.SubjectType,
            result.SubjectId,
            result.Effect,
            result.EndpointPattern,
            result.HttpVerb,
            result.IsActive,
            result.CreatedAtUtc)));
    }
}
