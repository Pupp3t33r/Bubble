namespace Bubble.CQRS.Query;
public class GetUserPasswordHashQuery: IRequest<string>
{
    public Guid UserId { get; set; }
}
