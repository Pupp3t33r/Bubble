using Bubble.Shared.Models.Request;

namespace Bubble.CQRS.Query;
public class GetArticlesPageAsEditorQuery : IRequest<List<Article>>
{
    public GetArticlesPageAsEditorRequest filters { get; set; }
}
