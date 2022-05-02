namespace Bubble.CQS.Handlers.Query;
public class GetArticleQueryHandler : IRequestHandler<GetArticleQuery, Article>
{
    private readonly NewsDbContext _dbContext;

    public GetArticleQueryHandler(NewsDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Article> Handle(GetArticleQuery request, CancellationToken cancellationToken)
    {
        return await _dbContext.Articles
                .FirstOrDefaultAsync(x => x.Id == request.ArticleId, cancellationToken);
    }
}
