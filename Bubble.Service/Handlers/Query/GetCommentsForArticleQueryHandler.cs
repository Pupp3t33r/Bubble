namespace Bubble.CQS.Handlers.Query;
public class GetCommentsForArticleQueryHandler : IRequestHandler<GetCommentsForArticleQuery, List<Comment>>
{
    private readonly NewsDbContext _dbContext;

    public GetCommentsForArticleQueryHandler(NewsDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<List<Comment>> Handle(GetCommentsForArticleQuery request, CancellationToken cancellationToken)
    {
        var result = await _dbContext.Comments.AsNoTracking()
                        .Include(comment=>comment.User)
                        .Where(comment => comment.ArticleId == request.ArticleId).ToListAsync(cancellationToken);
        return result;
    }
}
