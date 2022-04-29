namespace Bubble.CQRS.Query;
public class CheckIfUserExistsQuery: IRequest<Guid>
{
    public string UserName { get; set; }
}
