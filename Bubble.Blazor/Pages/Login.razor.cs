using Blazored.LocalStorage;
using Bubble.Shared.Models.Request;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;

namespace Bubble.Blazor.Pages;

public partial class Login
{
    [Inject] public HttpClient Http { get; set; }
    [Inject] public AuthenticationStateProvider AuthStateProvider { get; set; }
    [Inject] public ILocalStorageService LocalStorage { get; set; }

    UserLoginRequest userLogin = new();

    async Task HandleLogin()
    {
        var result = await Http.PostAsJsonAsync("api/Users/Login", userLogin);
        var token = await result.Content.ReadAsStringAsync();
        Console.WriteLine(token);
        await LocalStorage.SetItemAsync("token", token);
        await AuthStateProvider.GetAuthenticationStateAsync();
    }
}
