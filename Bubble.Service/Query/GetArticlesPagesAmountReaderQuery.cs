using Bubble.Shared.Models.Request;

namespace Bubble.CQRS.Query;
public class GetArticlesPagesAmountReaderQuery : IRequest<int>
{
    public GetArticlesPageAsReaderRequest filters { get; set; }
}
