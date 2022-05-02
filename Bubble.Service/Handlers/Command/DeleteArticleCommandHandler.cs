namespace Bubble.CQS.Handlers.Command;
public class DeleteArticleCommandHandler : IRequestHandler<DeleteArticleCommand, int>
{
    private readonly NewsDbContext _dbContext;

    public DeleteArticleCommandHandler(NewsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> Handle(DeleteArticleCommand request, CancellationToken cancellationToken)
    {
        var article = await _dbContext.Articles.FirstOrDefaultAsync(x => x.Id == request.ArticleId);
        _dbContext.Articles.Remove(article);
        return await _dbContext.SaveChangesAsync();
    }
}
