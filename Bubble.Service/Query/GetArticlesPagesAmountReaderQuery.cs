using Bubble.Shared.Models.Request;

namespace Bubble.Service.Query;
public class GetArticlesPagesAmountReaderQuery : IRequest<int>
{
    public GetArticlesPageAsReaderRequest filters { get; set; }
}
