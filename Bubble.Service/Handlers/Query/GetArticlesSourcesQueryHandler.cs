namespace Bubble.CQRS.Handlers.Query;
public class GetArticlesSourcesQueryHandler : IRequestHandler<GetArticlesSourcesQuery, List<string>>
{
    private readonly NewsDbContext _dbContext;

    public GetArticlesSourcesQueryHandler(NewsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<string>> Handle(GetArticlesSourcesQuery request, CancellationToken cancellationToken)
    {
        return await _dbContext.Articles.AsNoTracking().Select(x => x.Source).Distinct().ToListAsync(cancellationToken);
    }
}
