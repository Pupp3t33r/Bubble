using Bubble.Shared.Enums;

namespace Bubble.Shared.Models.Request;
public class GetArticlesPageAsReaderRequest
{
    public int PageNum { get; set; } = 1;
    public int PageSize { get; set; } = 5;
    public string Source { get; set; } = String.Empty;
    public string ArticleTitleSearch { get; set; } = String.Empty;
    public DateTime PubDate { get; set; }
    public ComparisonOperators PubDateComparisonOperator { get; set; }
}
