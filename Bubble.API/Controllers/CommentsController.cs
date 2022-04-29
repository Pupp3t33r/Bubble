using Bubble.APIServices.Interfaces;
using Bubble.Shared.Models.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bubble.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommentsController: Controller
{
    private readonly ICommentService _commentService;

    public CommentsController(ICommentService commentService)
    {
        _commentService = commentService;
    }

    [HttpPost("AddComment")]
    [Authorize]
    public async Task<ActionResult<int>> AddComment(AddCommentRequest addCommentRequest)
    {
        try
        {
            return await _commentService.AddCommentAsync(addCommentRequest);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
