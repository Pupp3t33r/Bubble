using Bubble.Shared.Models.Request;

namespace Bubble.CQS.Query;
public class GetArticlesPagesAmountEditorQuery : IRequest<int>
{
    public GetArticlesPageAsEditorRequest filters { get; set; }
}
