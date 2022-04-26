namespace Bubble.Service.Handlers.Query;
public class GetAllArticlesUrlQueryHandler : IRequestHandler<GetAllArticlesUrlQuery, List<string>>
{
    private readonly NewsDbContext _dbContext;

    public GetAllArticlesUrlQueryHandler(NewsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<string>> Handle(GetAllArticlesUrlQuery request, CancellationToken cancellationToken)
    {
        return await _dbContext.Articles.Select(article => article.SourceURL).ToListAsync();
    }
}
