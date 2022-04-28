using Bubble.Service.Interfaces;
using Bubble.Shared.Models.Request;
using Bubble.Shared.Models.Response;
using Microsoft.AspNetCore.Authorization;
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
    public async Task<ActionResult<List<GetArticlesPageAsReaderResponse>>> GetArticlesAsReader(GetArticlesPageAsReaderRequest request)
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
    [HttpPost("GetArticlesAsEditor"), Authorize(Roles = "Editor, Administrator")]
    public async Task<ActionResult<List<GetArticlesPageAsEditorResponse>>> GetArticlesAsEditor(GetArticlesPageAsEditorRequest request)
    {
        try
        {
            return await _articleService.GetArticlesPageAsEditor(request);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpPost("GetArticlesPagesAmount")]
    public async Task<ActionResult<int>> GetArticlesPagesAmount(GetArticlesPagesAmountRequest request)
    {
        try
        {
            return await _articleService.GetArticlesPagesAmount(request);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpGet("GetSources")]
    public async Task<ActionResult<List<string>>> GetSources()
    {
        try
        {
            return await _articleService.GetArticlesSources();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
