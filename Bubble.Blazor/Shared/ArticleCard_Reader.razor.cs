using Bubble.Shared.Models.Response;
using Microsoft.AspNetCore.Components;

namespace Bubble.Blazor.Shared;

public partial class ArticleCard_Reader
{
    [Parameter] public GetArticlesPageAsReaderResponse ArticleDetails { get; set; }
}
