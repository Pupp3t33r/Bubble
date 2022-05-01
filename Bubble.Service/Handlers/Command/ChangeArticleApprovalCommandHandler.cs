namespace Bubble.CQRS.Handlers.Command;
public class ChangeArticleApprovalCommandHandler : IRequestHandler<ChangeArticleApprovalCommand, bool>
{
    private readonly NewsDbContext _dbContext;

    public ChangeArticleApprovalCommandHandler(NewsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> Handle(ChangeArticleApprovalCommand request, CancellationToken cancellationToken)
    {
        var article = await _dbContext.Articles.FirstOrDefaultAsync(x => x.Id == request.ArticleId);
        article.Approved = !article.Approved;
        await _dbContext.SaveChangesAsync();
        return article.Approved;
    }
}
