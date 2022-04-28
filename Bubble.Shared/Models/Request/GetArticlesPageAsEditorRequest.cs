using Bubble.Shared.Enums;

namespace Bubble.Shared.Models.Request;
public class GetArticlesPageAsEditorRequest
{
    public int PageNum { get; set; } = 1;
    public int PageSize { get; set; } = 5;
    public string Source { get; set; } = String.Empty;
    public string ArticleTitleSearch { get; set; } = String.Empty;
    public DateTime PubDate { get; set; } = DateTime.Now;
    public ComparisonOperators PubDateComparisonOperator { get; set; } = ComparisonOperators.Less_or_Equal;
    public bool? Approved { get; set; } = null;
    public int GoodnessRatingMin { get; set; }
    public int GoodnessRatingMax { get; set; }
}
