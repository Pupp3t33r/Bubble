using Bubble.Shared.Models.Request;
using Bubble.Shared.Models.Response;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace Bubble.Blazor.Pages;

public partial class Index
{
    [Inject] public HttpClient Http { get; set; }

    List<GetArticlesAsReaderResponse> articles = new();

    GetArticlesPageAsReaderRequest filter = new() 
    { PageNum=1, PageSize=5, 
        PubDate=DateTime.Today, 
        ArticleTitleSearch = "",
        PubDateComparisonOperator = Bubble.Shared.Enums.ComparisonOperators.Less_or_Equal, 
        Source = ""};

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            var result = await Http.PostAsJsonAsync("api/Articles/GetArticlesAsReader", filter);
            if (result.IsSuccessStatusCode)
                articles = await result.Content.ReadFromJsonAsync<List<GetArticlesAsReaderResponse>>(); 
            StateHasChanged();
        }
    }
}
