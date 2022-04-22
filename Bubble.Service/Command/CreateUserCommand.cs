namespace Bubble.Service.Command;
public class CreateUserCommand: IRequest<User>
{
    public User User { get; set; }
}
