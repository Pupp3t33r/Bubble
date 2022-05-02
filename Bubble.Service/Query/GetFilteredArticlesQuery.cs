using Bubble.Data.Entities;

namespace Bubble.CQS.Query;
public class GetFilteredArticlesQuery: IRequest<List<Article>>
{
    public string ArticleName { get; set; }
    public string Source { get; set; }
    public List<Guid> TagIds { get; set; }
    public int PageNum { get; set; }
    public int PageSize { get; set; }
}
