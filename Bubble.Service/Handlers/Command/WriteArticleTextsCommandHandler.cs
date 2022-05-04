namespace Bubble.CQS.Handlers.Command;
public class WriteArticleTextsCommandHandler : IRequestHandler<WriteArticleTextsCommand, int>
{
    private readonly NewsDbContext _dbContext;

    public WriteArticleTextsCommandHandler(NewsDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<int> Handle(WriteArticleTextsCommand request, CancellationToken cancellationToken)
    {
        foreach (var article in request.Articles)
        {
            var articleToWrite = await _dbContext.Articles.FirstOrDefaultAsync(x=>x.Id==article.Id);
            if (articleToWrite is not null)
            {
                articleToWrite.ArticleText = article.ArticleText;
            }
        }

        return await _dbContext.SaveChangesAsync();
    }
}
