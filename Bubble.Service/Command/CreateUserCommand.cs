namespace Bubble.CQRS.Command;
public class CreateUserCommand: IRequest<User>
{
    public User User { get; set; }
}
