namespace Bubble.Service.Handlers.Command;
public class AddNewArticlesCommandHandler: IRequestHandler<AddNewArticlesCommand, int>
{
    private readonly NewsDbContext _dbContext;

    public AddNewArticlesCommandHandler(NewsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> Handle(AddNewArticlesCommand request, CancellationToken cancellationToken)
    {
        await _dbContext.Articles.AddRangeAsync(request.ArticlesToWrite);
        return await _dbContext.SaveChangesAsync();
    }
}
