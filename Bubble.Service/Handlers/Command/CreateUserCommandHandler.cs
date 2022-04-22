
namespace Bubble.Service.Handlers.Command;
public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, User>
{
    private readonly NewsDbContext _dbContext;

    public CreateUserCommandHandler(NewsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<User> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        await _dbContext.Users.AddAsync(request.User);
        await _dbContext.SaveChangesAsync();
        return request.User;
    }
}
