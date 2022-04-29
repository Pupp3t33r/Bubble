
namespace Bubble.CQRS.Handlers.Query;
public class GetFilteredArticlesQueryHandler : IRequestHandler<GetFilteredArticlesQuery, List<Article>>
{
    private readonly NewsDbContext _dbContext;

    public GetFilteredArticlesQueryHandler(NewsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Article>> Handle(GetFilteredArticlesQuery request, CancellationToken cancellationToken)
    {
        var query = _dbContext.Articles.Include(article => article.Comments)
                                .ThenInclude(comment => comment.User)
                                .Include(article => article.Tags).AsNoTracking();

        if (!String.IsNullOrEmpty(request.ArticleName))
        {
            query = query.Where(article => article.Title.ToLower()
                                 .Contains(request.ArticleName.ToLower()));
        }
        if (!String.IsNullOrEmpty(request.Source))
        {
            query = query.Where(article => article.Source.ToLower()
                                 .Contains(request.Source.ToLower()));
        }
        if (request.TagIds.Count > 0)
        {
            foreach (var tagId in request.TagIds)
            {
                query = query.Where(article => article.Tags.Any(artTag => artTag.Id == tagId));
            }
        }

        List<Article> resultList = await query.Skip((request.PageNum - 1) * request.PageSize).Take(request.PageSize)
                                .ToListAsync(cancellationToken);

        return resultList;
    }
}
