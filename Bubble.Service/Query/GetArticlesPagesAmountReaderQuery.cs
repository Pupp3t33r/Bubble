using Bubble.Shared.Models.Request;

namespace Bubble.CQS.Query;
public class GetArticlesPagesAmountReaderQuery : IRequest<int>
{
    public GetArticlesPageAsReaderRequest filters { get; set; }
}
