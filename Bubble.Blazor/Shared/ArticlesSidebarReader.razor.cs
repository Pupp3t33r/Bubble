using Bubble.Shared.Models.Request;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace Bubble.Blazor.Shared;

public partial class ArticlesSidebarReader
{
    [Inject] public HttpClient Http { get; set; }
    [Parameter] public GetArticlesPageAsReaderRequest Filter { get; set; }
    [Parameter] public EventCallback<GetArticlesPageAsReaderRequest> FilterChanged { get; set; }

    private List<string> Sources = new();
    private DateTime? pubDate = DateTime.Now;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            Sources = await Http.GetFromJsonAsync<List<string>>("api/Articles/GetSources");
            pubDate = Filter.PubDate;
        }
    }
}
