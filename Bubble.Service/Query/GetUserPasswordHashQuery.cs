namespace Bubble.Service.Query;
public class GetUserPasswordHashQuery: IRequest<string>
{
    public Guid UserId { get; set; }
}
