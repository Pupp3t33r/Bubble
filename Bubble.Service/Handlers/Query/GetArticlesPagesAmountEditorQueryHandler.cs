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
                query = query.Where(x => x.PublishDate.Date <= request.filters.PubDate.Date);
                break;
            default:
                break;
        }
        if (request.filters.Rated == Shared.Enums.YesNoAll.Yes)
        {
            switch (request.filters.GoodnessRatingComparisonOperator)
            {
                case Shared.Enums.ComparisonOperators.Equal:
                    query = query.Where(x => x.GoodnessRating == request.filters.GoodnessRating);
                    break;
                case Shared.Enums.ComparisonOperators.More:
                    query = query.Where(x => x.GoodnessRating > request.filters.GoodnessRating);
                    break;
                case Shared.Enums.ComparisonOperators.More_or_Equal:
                    query = query.Where(x => x.GoodnessRating >= request.filters.GoodnessRating);
                    break;
                case Shared.Enums.ComparisonOperators.Less:
                    query = query.Where(x => x.GoodnessRating < request.filters.GoodnessRating);
                    break;
                case Shared.Enums.ComparisonOperators.Less_or_Equal:
                    query = query.Where(x => x.GoodnessRating <= request.filters.GoodnessRating);
                    break;
                default:
                    break;
            }
        }
        else if (request.filters.Rated == Shared.Enums.YesNoAll.No)
        {
            query = query.Where(x => x.GoodnessRating == null);
        }

        switch (request.filters.Approved)
        {
            case Shared.Enums.YesNoAll.Yes:
                query = query.Where(x => x.Approved);
                break;
            case Shared.Enums.YesNoAll.No:
                query = query.Where(x => !x.Approved);
                break;
            case Shared.Enums.YesNoAll.All:
                break;
            default:
                break;
        }
        var itemCount = await query.CountAsync(cancellationToken);
        return (itemCount + request.filters.PageSize - 1) / request.filters.PageSize;
    }
}
