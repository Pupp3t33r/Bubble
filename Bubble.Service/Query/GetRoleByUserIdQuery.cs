namespace Bubble.CQRS.Query;
public class GetRoleByUserIdQuery: IRequest<string>
{
    public Guid UserId { get; set; }
}
