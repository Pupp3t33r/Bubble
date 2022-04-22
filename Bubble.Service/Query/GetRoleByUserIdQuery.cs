namespace Bubble.Service.Query;
public class GetRoleByUserIdQuery: IRequest<string>
{
    public Guid UserId { get; set; }
}
