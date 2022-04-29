using Bubble.Shared.Models.Request;
using Bubble.Shared.Models.Response;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace Bubble.Blazor.Pages;

public partial class Index
{
    [Inject] public HttpClient Http { get; set; }

    List<GetArticlesPageAsReaderResponse> articles = new();
    int pagesAmount = 1;

    GetArticlesPageAsReaderRequest filter = new() 
    { PageNum=1, PageSize=8, 
        PubDate=DateTime.Today, 
        ArticleTitleSearch = "",
        PubDateComparisonOperator = Bubble.Shared.Enums.ComparisonOperators.Less_or_Equal, 
        Source = ""
    };

    private async Task FilterOut_Reader()
    {
        await UpdateArticlesList();
        await UpdatePagesAmount_Reader();
        StateHasChanged();
    }

    private async Task PageChanged(int i)
    {
        filter.PageNum = i;
        await UpdateArticlesList();
        StateHasChanged();
    }

    private async Task UpdateArticlesList()
    {
        var result = await Http.PostAsJsonAsync("api/Articles/GetArticlesAsReader", filter);
        if (result.IsSuccessStatusCode)
            articles = await result.Content.ReadFromJsonAsync<List<GetArticlesPageAsReaderResponse>>();
    }

    private async Task UpdatePagesAmount_Reader()
    {
        var result = await Http.PostAsJsonAsync("api/Articles/GetArticlesPagesAmountReader", filter);
        if (result.IsSuccessStatusCode)
            pagesAmount = await result.Content.ReadFromJsonAsync<int>();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await UpdateArticlesList();
            await UpdatePagesAmount_Reader();
            StateHasChanged();
        }
    }
}
