using Bubble.Shared.Models.Request;

namespace Bubble.Service.Query;
public class GetArticlesPagesAmountQuery : IRequest<int>
{
    public GetArticlesPagesAmountRequest filters { get; set; }
}
