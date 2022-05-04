namespace Bubble.CQS.Handlers.Query;
public class GetAllArticlesWithNoBodyQueryHandler : IRequestHandler<GetAllArticlesWithNoBodyQuery, List<Article>>
{
    private readonly NewsDbContext _dbContext;

    public GetAllArticlesWithNoBodyQueryHandler(NewsDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<List<Article>> Handle(GetAllArticlesWithNoBodyQuery request, CancellationToken cancellationToken)
    {
        return await _dbContext.Articles.AsNoTracking().Where(article => article.ArticleText == null).ToListAsync();
    }
}
