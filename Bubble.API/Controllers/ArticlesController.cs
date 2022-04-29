using Bubble.APIServices.Interfaces;
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
            return await _articleService.GetArticlesPageAsReaderAsync(request);
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
            return await _articleService.GetArticlesPageAsEditorAsync(request);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetArticleResponse>> GetArticle(Guid id)
    {
        try
        {
            return await _articleService.GetArticleAsync(id);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("GetArticlesPagesAmountReader")]
    public async Task<ActionResult<int>> GetArticlesPagesAmount_Reader(GetArticlesPageAsReaderRequest request)
    {
        try
        {
            return await _articleService.GetArticlesPagesAmountReaderAsync(request);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("GetArticlesPagesAmountEditor")]
    public async Task<ActionResult<int>> GetArticlesPagesAmount_Editor(GetArticlesPageAsEditorRequest request)
    {
        try
        {
            return await _articleService.GetArticlesPagesAmountEditorAsync(request);
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
            return await _articleService.GetArticlesSourcesAsync();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
