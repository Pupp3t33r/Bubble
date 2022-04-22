
namespace Bubble.Service.Handlers.Query;
public class GetUserPasswordHashHandler : IRequestHandler<GetUserPasswordHashQuery, string>
{
    private readonly NewsDbContext _dbContext;

    public GetUserPasswordHashHandler(NewsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<string> Handle(GetUserPasswordHashQuery request, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(user => user.Id == request.UserId);
        return user!=null ? user.EncryptedPassword : String.Empty;
    }
}
