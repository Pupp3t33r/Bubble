namespace Bubble.CQS.Handlers.Query;
public class GetFirst5UnratedArticlesQueryHandler : IRequestHandler<GetFirst5UnratedArticlesQuery, List<Article>>
{
    private readonly NewsDbContext _dbContext;

    public GetFirst5UnratedArticlesQueryHandler(NewsDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<List<Article>> Handle(GetFirst5UnratedArticlesQuery request, CancellationToken cancellationToken)
    {
        return await _dbContext.Articles.AsNoTracking()
            .Where(x => x.GoodnessRating == null)
            .Take(5).ToListAsync(cancellationToken);
    }
}
