namespace Bubble.CQRS.Handlers.Query;
public class GetArticlesPagesAmountEditorQueryHandler : IRequestHandler<GetArticlesPagesAmountEditorQuery, int>
{
    private readonly NewsDbContext _dbContext;

    public GetArticlesPagesAmountEditorQueryHandler(NewsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> Handle(GetArticlesPagesAmountEditorQuery request, CancellationToken cancellationToken)
    {
        var query = _dbContext.Articles.AsNoTracking();

        if (!String.IsNullOrEmpty(request.filters.Source))
        {
            query = query.Where(x => x.Source.ToLower() == request.filters.Source.ToLower());
        }
        if (!String.IsNullOrEmpty(request.filters.ArticleTitleSearch))
        {
            query = query.Where(x =>
                        x.Title.ToLower().Contains(request.filters.ArticleTitleSearch.ToLower()));
        }
        switch (request.filters.PubDateComparisonOperator)
        {
            case Shared.Enums.ComparisonOperators.Equal:
                query = query.Where(x => x.PublishDate.Date == request.filters.PubDate.Date);
                break;
            case Shared.Enums.ComparisonOperators.More:
                query = query.Where(x => x.PublishDate.Date > request.filters.PubDate.Date);
                break;
            case Shared.Enums.ComparisonOperators.More_or_Equal:
                query = query.Where(x => x.PublishDate.Date >= request.filters.PubDate.Date);
                break;
            case Shared.Enums.ComparisonOperators.Less:
                query = query.Where(x => x.PublishDate.Date < request.filters.PubDate.Date);
                break;
            case Shared.Enums.ComparisonOperators.Less_or_Equal:
                query = query.Where(x => x.PublishDate.Date == request.filters.PubDate.Date);
                break;
            default:
                break;
        }
        query = query.Where(x => x.GoodnessRating >= request.filters.GoodnessRatingMin &&
                                          x.GoodnessRating <= request.filters.GoodnessRatingMax);
        if (request.filters.Approved is not null)
        {
            query = request.filters.Approved == true ?
                    query.Where(x => x.Approved) :
                    query.Where(x => !x.Approved);
        }
        var itemCount = await query.CountAsync(cancellationToken);
        return (itemCount + request.filters.PageSize - 1) / request.filters.PageSize;
    }
}
