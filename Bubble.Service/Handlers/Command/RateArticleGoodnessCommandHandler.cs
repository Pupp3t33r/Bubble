namespace Bubble.CQS.Handlers.Command;
public class RateArticleGoodnessCommandHandler : IRequestHandler<RateArticleGoodnessCommand, int>
{
    private readonly NewsDbContext _dbContext;

    public RateArticleGoodnessCommandHandler(NewsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> Handle(RateArticleGoodnessCommand request, CancellationToken cancellationToken)
    {
        var articleToRate = await _dbContext.Articles.FirstAsync(x=>x.Id == request.Id);
        articleToRate.GoodnessRating = request.GoodnessRating;
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
