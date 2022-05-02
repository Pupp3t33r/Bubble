using Bubble.Data.Entities;

namespace Bubble.CQS.Query;

public class GetAllArticlesQuery : IRequest<List<Article>>
{
    public int PageNum { get; set; }
    public int PageSize { get; set; }
}
