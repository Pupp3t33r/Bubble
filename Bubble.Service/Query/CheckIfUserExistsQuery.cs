namespace Bubble.Service.Query;
public class CheckIfUserExistsQuery: IRequest<Guid>
{
    public string UserName { get; set; }
}
