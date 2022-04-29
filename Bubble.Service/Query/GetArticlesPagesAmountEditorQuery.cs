using Bubble.Shared.Models.Request;

namespace Bubble.Service.Query;
public class GetArticlesPagesAmountEditorQuery : IRequest<int>
{
    public GetArticlesPageAsEditorRequest filters { get; set; }
}
