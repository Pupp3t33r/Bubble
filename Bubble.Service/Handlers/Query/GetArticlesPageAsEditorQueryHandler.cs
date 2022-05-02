using Bubble.Shared.Enums;

namespace Bubble.CQS.Handlers.Query;
public class GetArticlesPageAsEditorQueryHandler : IRequestHandler<GetArticlesPageAsEditorQuery, List<Article>>
{
    private readonly NewsDbContext _dbContext;

    public GetArticlesPageAsEditorQueryHandler(NewsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Article>> Handle(GetArticlesPageAsEditorQuery request, CancellationToken cancellationToken)
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
            case ComparisonOperators.Equal:
                query = query.Where(x => x.PublishDate.Date == request.filters.PubDate.Date);
                break;
            case ComparisonOperators.More:
                query = query.Where(x => x.PublishDate.Date > request.filters.PubDate.Date);
                break;
            case ComparisonOperators.More_or_Equal:
                query = query.Where(x => x.PublishDate.Date >= request.filters.PubDate.Date);
                break;
            case ComparisonOperators.Less:
                query = query.Where(x => x.PublishDate.Date < request.filters.PubDate.Date);
                break;
            case ComparisonOperators.Less_or_Equal:
                query = query.Where(x => x.PublishDate.Date <= request.filters.PubDate.Date);
                break;
            default:
                break;
        }

        if (request.filters.Rated==YesNoAll.Yes)
        {
            switch (request.filters.GoodnessRatingComparisonOperator)
            {
                case ComparisonOperators.Equal:
                    query = query.Where(x => x.GoodnessRating == request.filters.GoodnessRating);
                    break;
                case ComparisonOperators.More:
                    query = query.Where(x => x.GoodnessRating > request.filters.GoodnessRating);
                    break;
                case ComparisonOperators.More_or_Equal:
                    query = query.Where(x => x.GoodnessRating >= request.filters.GoodnessRating);
                    break;
                case ComparisonOperators.Less:
                    query = query.Where(x => x.GoodnessRating < request.filters.GoodnessRating);
                    break;
                case ComparisonOperators.Less_or_Equal:
                    query = query.Where(x => x.GoodnessRating <= request.filters.GoodnessRating);
                    break;
                default:
                    break;
            }
        }
        else if (request.filters.Rated==YesNoAll.No)
        {
            query = query.Where(x => x.GoodnessRating == null);
        }

        switch (request.filters.Approved)
        {
            case YesNoAll.Yes:
                query = query.Where(x => x.Approved);
                break;
            case YesNoAll.No:
                query = query.Where(x => !x.Approved);
                break;
            case YesNoAll.All:
                break;
            default:
                break;
        }
        List<Article> resultList = await query.Skip((request.filters.PageNum - 1) * request.filters.PageSize)
                                              .Take(request.filters.PageSize)
                                              .ToListAsync(cancellationToken);

        return resultList;
    }
}
