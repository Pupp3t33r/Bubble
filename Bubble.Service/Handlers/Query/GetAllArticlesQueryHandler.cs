
namespace Bubble.Service.Handlers.Query;
public class GetAllArticlesQueryHandler : IRequestHandler<GetAllArticlesQuery, List<Article>>
{
    private readonly NewsDbContext _dbContext;

    public GetAllArticlesQueryHandler(NewsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Article>> Handle(GetAllArticlesQuery request, CancellationToken cancellationToken)
    {
        List<Article> resultList = new();
        resultList = await _dbContext.Articles.AsNoTracking()
                            .Skip((request.PageNum - 1) * request.PageSize).Take(request.PageSize)
                            .ToListAsync();
        return resultList;
    }
}
