using Bubble.Data;
using Bubble.CQS.Query;
using Microsoft.EntityFrameworkCore;

namespace Bubble.CQS.Handlers.Query;
public class GetRoleIdByRoleNameQueryHandler : IRequestHandler<GetRoleIdByRoleNameQuery, Guid>
{
    private readonly NewsDbContext _dbContext;

    public GetRoleIdByRoleNameQueryHandler(NewsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Guid> Handle(GetRoleIdByRoleNameQuery request, CancellationToken cancellationToken)
    {
        var role = await _dbContext.AccessRoles.FirstOrDefaultAsync(role => role.Name == request.Name, cancellationToken);

        return role != null ? role.Id : Guid.Empty;
    }
}
