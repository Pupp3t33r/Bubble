using Bubble.Shared.Enums;

namespace Bubble.Shared.Models.Request;
public class GetArticlesPagesAmountRequest
{
    public int PageSize { get; set; } = 5;
    public string Source { get; set; } = String.Empty;
    public string ArticleTitleSearch { get; set; } = String.Empty;
    public DateTime PubDate { get; set; }
    public ComparisonOperators PubDateComparisonOperator { get; set; }
    public bool AsEditor { get; set; }
    public bool? Approved { get; set; } = null;
    public int GoodnessRatingMin { get; set; }
    public int GoodnessRatingMax { get; set; }
}
