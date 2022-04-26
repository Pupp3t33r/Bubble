using AutoMapper;
using Bubble.Service.Interfaces;
using Bubble.Shared.Models.Request;
using Bubble.Shared.Models.Response;

namespace Bubble.Service.Services;
public class ArticleService: IArticleService
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    public ArticleService(IMediator mediator, IMapper mapper)
    {
        (_mediator, _mapper) = (mediator, mapper);
    }

    public async Task AddNewArticlesToDB()
    {
        var articleRecords = (await OnlinerScraper.GetOnlinerArticlesAsync()).ToList();
        var articles = _mapper.Map<List<Article>>(articleRecords);
        var dbUrls = await GetAllArticleUrls();
        List<Article> articlesToWrite = new();
        foreach (var article in articles)
        {
            if (!dbUrls.Any(url=> url==article.SourceURL))
            {
                article.Source = "Onliner";
                article.Approved = false;
                articlesToWrite.Add(article);
            }
        }
        await _mediator.Send(new AddNewArticlesCommand { ArticlesToWrite = articlesToWrite });
    }

    public async Task<List<GetArticlesAsReaderResponse>> GetArticlesPageAsReader(GetArticlesPageAsReaderRequest request)
    {
        var articles = await _mediator.Send(new GetArticlesPageAsReaderQuery { ArticlesRequest = request });
        return _mapper.Map<List<GetArticlesAsReaderResponse>>(articles);
    }

    private async Task<List<string>> GetAllArticleUrls()
    {
        return await _mediator.Send(new GetAllArticlesUrlQuery());
    }

}
