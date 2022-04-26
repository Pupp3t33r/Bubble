using Bubble.Shared.Models.Request;

namespace Bubble.Service.Query;
public class GetArticlesPageAsReaderQuery: IRequest<List<Article>>
{
    public GetArticlesPageAsReaderRequest ArticlesRequest { get; set; }
}
