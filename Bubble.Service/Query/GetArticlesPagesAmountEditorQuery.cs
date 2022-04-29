using Bubble.Shared.Models.Request;

namespace Bubble.CQRS.Query;
public class GetArticlesPagesAmountEditorQuery : IRequest<int>
{
    public GetArticlesPageAsEditorRequest filters { get; set; }
}
