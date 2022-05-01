using Bubble.Shared.Models.Response;
using Microsoft.AspNetCore.Components;

namespace Bubble.Blazor.Shared;

public partial class ArticleCard_Editor
{
    [Parameter] public GetArticlesPageAsEditorResponse ArticleDetails { get; set; }
}
