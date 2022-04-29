using Bubble.Shared.Models.Request;
using Bubble.Shared.Models.Response;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;

namespace Bubble.Blazor.Pages;

public partial class Article
{
    [Inject] public HttpClient Http { get; set; }
    [Inject] public AuthenticationStateProvider AuthStateProvider { get; set; }
    [Parameter] public string ArticleId { get; set; }

    GetArticleResponse article;
    string newCommentText = String.Empty;

    private async Task<bool> PostCommentAsync()
    {
        var user = await AuthStateProvider.GetAuthenticationStateAsync();
        if (user is null)
        {
            return false;
        }
        var userName = user.User.Identity.Name;
        AddCommentRequest addComment = new()
        {
            ArticleId = Guid.Parse(ArticleId),
            CommentText = newCommentText,
            UserName = userName
        };
        var result = await Http.PostAsJsonAsync<AddCommentRequest>($"api/Comments/AddComment", addComment);
        return result.IsSuccessStatusCode;
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            article = await Http.GetFromJsonAsync<GetArticleResponse>($"api/Articles/{ArticleId}");
            StateHasChanged();
        }
    }
}
