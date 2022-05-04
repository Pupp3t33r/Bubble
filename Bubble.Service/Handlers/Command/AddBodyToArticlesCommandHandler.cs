namespace Bubble.CQS.Handlers.Command;
public class AddBodyToArticlesCommandHandler : IRequestHandler<AddBodyToArticlesCommand, int>
{
    private readonly NewsDbContext _dbContext;

    public AddBodyToArticlesCommandHandler(NewsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> Handle(AddBodyToArticlesCommand request, CancellationToken cancellationToken)
    {
        foreach (var article in request.Articles)
        {
            var articleToUpdate = await _dbContext.Articles.FirstOrDefaultAsync(x => x.Id == article.Id);
            if (articleToUpdate is not null)
                articleToUpdate.ArticleText = article.ArticleText;
        }
        return await _dbContext.SaveChangesAsync();
    }
}
