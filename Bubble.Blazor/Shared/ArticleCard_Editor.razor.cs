using Bubble.Shared.Models.Response;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace Bubble.Blazor.Shared;

public partial class ArticleCard_Editor
{
    [Inject] public HttpClient Http { get; set; }
    [Parameter] public GetArticlesPageAsEditorResponse ArticleDetails { get; set; }
    [Parameter] public EventCallback<GetArticlesPageAsEditorResponse> ArticleDetailsChanged { get; set; }
    [Parameter] public EventCallback DeleteArticle { get; set; }

    private string goodnessRating => ArticleDetails.GoodnessRating is null ? "n/a" : ArticleDetails.GoodnessRating.ToString();

    private string approved => ArticleDetails.Approved ? "Да" : "Нет";

    private async Task ChangeArticleApproval()
    {
        ArticleDetails.Approved = await Http.GetFromJsonAsync<bool>($"api/Articles/ChangeArticleApproval/{ArticleDetails.Id}");
        StateHasChanged();
    }
}
