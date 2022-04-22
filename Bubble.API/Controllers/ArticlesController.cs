using AutoMapper;
using Bubble.Service.Query;
using Bubble.Shared.Models.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bubble.API.Controllers;

[ApiController]
[Route("Articles")]
public class ArticlesController: Controller
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    public ArticlesController(IMediator mediator, IMapper mapper)
    {
        (_mediator, _mapper) = (mediator, mapper);
    }

    [HttpGet("GetAll")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<List<GetArticlesResponse>>> GetAll()
    {
        try
        {
            IEnumerable<OnlinerScraper.Article> articles = OnlinerScraper.getOnlinerArticles();
            var response = await _mediator.Send(new GetAllArticlesQuery() {PageNum=1, PageSize=10 });
            return _mapper.Map<List<GetArticlesResponse>>(response);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
