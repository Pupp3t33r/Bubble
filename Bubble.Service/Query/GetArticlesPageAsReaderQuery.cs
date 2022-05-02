using Bubble.Shared.Models.Request;

namespace Bubble.CQS.Query;
public class GetArticlesPageAsReaderQuery: IRequest<List<Article>>
{
    public GetArticlesPageAsReaderRequest ArticlesRequest { get; set; }
}
