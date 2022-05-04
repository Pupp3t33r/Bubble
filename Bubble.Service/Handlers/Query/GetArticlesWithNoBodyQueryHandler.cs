namespace Bubble.CQS.Handlers.Query;
public class GetArticlesWithNoBodyQueryHandler : IRequestHandler<GetArticlesWithNoBodyQuery, List<Article>>
{
    private readonly NewsDbContext _dbContext;

    public GetArticlesWithNoBodyQueryHandler(NewsDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<List<Article>> Handle(GetArticlesWithNoBodyQuery request, CancellationToken cancellationToken)
    {
        var result =  await _dbContext.Articles.AsNoTracking().Where(x => x.ArticleText == null).ToListAsync(cancellationToken);
        return result??new List<Article>();
    }
}
