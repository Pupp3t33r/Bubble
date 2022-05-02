namespace Bubble.CQS.Query;
public class GetRoleIdByRoleNameQuery: IRequest<Guid>
{
    public string Name { get; set; }
}
