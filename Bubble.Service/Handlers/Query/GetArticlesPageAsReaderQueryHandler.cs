namespace Bubble.Service.Handlers.Query;
public class GetArticlesPageAsReaderQueryHandler : IRequestHandler<GetArticlesPageAsReaderQuery, List<Article>>
{
    private readonly NewsDbContext _dbContext;

    public GetArticlesPageAsReaderQueryHandler(NewsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Article>> Handle(GetArticlesPageAsReaderQuery request, CancellationToken cancellationToken)
    {
        var query = _dbContext.Articles.AsNoTracking();

        if (!String.IsNullOrEmpty(request.ArticlesRequest.Source))
        {
            query = query.Where(x => x.Source.ToLower() == request.ArticlesRequest.Source.ToLower());
        }
        if (!String.IsNullOrEmpty(request.ArticlesRequest.ArticleTitleSearch))
        {
            query = query.Where(x=>
                        x.Source.ToLower().Contains(request.ArticlesRequest.ArticleTitleSearch.ToLower()));
        }
        switch (request.ArticlesRequest.PubDateComparisonOperator)
        {
            case Shared.Enums.ComparisonOperators.Equal:
                query = query.Where(x => x.PublishDate.Date==request.ArticlesRequest.PubDate.Date);
                break;
            case Shared.Enums.ComparisonOperators.More:
                query = query.Where(x => x.PublishDate.Date > request.ArticlesRequest.PubDate.Date);
                break;
            case Shared.Enums.ComparisonOperators.More_or_Equal:
                query = query.Where(x => x.PublishDate.Date >= request.ArticlesRequest.PubDate.Date);
                break;
            case Shared.Enums.ComparisonOperators.Less:
                query = query.Where(x => x.PublishDate.Date < request.ArticlesRequest.PubDate.Date);
                break;
            case Shared.Enums.ComparisonOperators.Less_or_Equal:
                query = query.Where(x => x.PublishDate.Date <= request.ArticlesRequest.PubDate.Date);
                break;
            default:
                break;
        }

        query = query.Where(x => x.Approved == true).OrderByDescending(x=>x.PublishDate);

        List<Article> resultList = await query.Skip((request.ArticlesRequest.PageNum - 1) * request.ArticlesRequest.PageSize)
                                              .Take(request.ArticlesRequest.PageSize)
                                              .ToListAsync(cancellationToken);

        return resultList;
    }
}
