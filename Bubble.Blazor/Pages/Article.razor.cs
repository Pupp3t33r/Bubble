using Bubble.Shared.Models.Response;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace Bubble.Blazor.Pages;

public partial class Article
{
    [Inject] public HttpClient Http { get; set; }
    [Parameter] public string ArticleId { get; set; }

    GetArticleResponse article;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            article = await Http.GetFromJsonAsync<GetArticleResponse>($"api/Articles/{ArticleId}");
            StateHasChanged();
        }
    }
}
