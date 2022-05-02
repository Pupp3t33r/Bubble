using Bubble.Data;
using Bubble.CQS.Query;
using Microsoft.EntityFrameworkCore;

namespace Bubble.CQS.Handlers.Query;
public class CheckIfUserExistsQueryHandler : IRequestHandler<CheckIfUserExistsQuery, Guid>
{
    private readonly NewsDbContext _dbContext;

    public CheckIfUserExistsQueryHandler(NewsDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Guid> Handle(CheckIfUserExistsQuery request, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users
                .FirstOrDefaultAsync(x=>x.Name.Equals(request.UserName), cancellationToken);
        return user != null ? user.Id : Guid.Empty;
    }
}
