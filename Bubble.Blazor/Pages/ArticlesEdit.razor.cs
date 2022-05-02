using Bubble.Shared.Models.Request;
using Bubble.Shared.Models.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using Bubble.Shared.Enums;

namespace Bubble.Blazor.Pages;

[Authorize(Roles ="Editor, Administrator")]
public partial class ArticlesEdit
{
    [Inject] public HttpClient Http { get; set; }

    List<GetArticlesPageAsEditorResponse> articles = new();
    int pagesAmount = 1;

    GetArticlesPageAsEditorRequest filter = new()
    {
        PageNum = 1,
        PageSize = 8,
        PubDate = DateTime.Today,
        ArticleTitleSearch = "",
        PubDateComparisonOperator = ComparisonOperators.Less_or_Equal,
        Source = "",
        Approved = YesNoAll.All,
        Rated = YesNoAll.All,
        GoodnessRating = 500,
        GoodnessRatingComparisonOperator = ComparisonOperators.Less_or_Equal
    };
    GetArticlesPageAsEditorRequest currFilter;

    private async Task FilterOut()
    {
        currFilter = (GetArticlesPageAsEditorRequest)filter.Clone();
        await UpdateArticlesList();
        await UpdatePagesAmount();
        StateHasChanged();
    }

    private async Task PageChanged(int i)
    {
        currFilter.PageNum = i;
        await UpdateArticlesList();
        StateHasChanged();
    }

    private async Task DeleteArticle (GetArticlesPageAsEditorResponse article)
    {
        var result = await Http.DeleteAsync($"api/Articles/DeleteArticle/{article.Id}");
        articles.Remove(article);
    }

    private async Task UpdateArticlesList()
    {
        var result = await Http.PostAsJsonAsync("api/Articles/GetArticlesAsEditor", currFilter);
        if (result.IsSuccessStatusCode)
            articles = await result.Content.ReadFromJsonAsync<List<GetArticlesPageAsEditorResponse>>();
    }

    private async Task UpdatePagesAmount()
    {
        var result = await Http.PostAsJsonAsync("api/Articles/GetArticlesPagesAmountEditor", currFilter);
        if (result.IsSuccessStatusCode)
            pagesAmount = await result.Content.ReadFromJsonAsync<int>();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            currFilter = (GetArticlesPageAsEditorRequest)filter.Clone();
            await UpdateArticlesList();
            await UpdatePagesAmount();
            StateHasChanged();
        }
    }
}
