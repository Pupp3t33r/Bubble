namespace Bubble.Service.Handlers.Query;
public class GetRoleByUserIdQueryHandler : IRequestHandler<GetRoleByUserIdQuery, string>
{
    private readonly NewsDbContext _dbContext;

    public GetRoleByUserIdQueryHandler(NewsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<string> Handle(GetRoleByUserIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users
                    .Include(user=>user.Role)
                    .AsNoTracking().FirstOrDefaultAsync(user=>user.Id==request.UserId);

        return user!=null ? user.Role.Name:String.Empty;
    }
}
