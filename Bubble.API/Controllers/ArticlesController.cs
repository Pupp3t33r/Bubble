using AutoMapper;
using Bubble.Service.Interfaces;
using Bubble.Service.Query;
using Bubble.Shared.Models.Request;
using Bubble.Shared.Models.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bubble.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ArticlesController: Controller
{
    private readonly IArticleService _articleService;
    public ArticlesController(IArticleService articleService)
    {
        _articleService = articleService;
    }

    [HttpPost("GetArticlesAsReader")]
    public async Task<ActionResult<List<GetArticlesAsReaderResponse>>> GetArticlesAsReader(GetArticlesPageAsReaderRequest request)
    {
        try
        {
            return await _articleService.GetArticlesPageAsReader(request);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
