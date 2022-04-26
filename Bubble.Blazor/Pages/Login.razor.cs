using Blazored.LocalStorage;
using Bubble.Shared.Models.Request;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;
using System.Text.RegularExpressions;
using MudBlazor;
using System.Security.Claims;

namespace Bubble.Blazor.Pages;

public partial class Login
{
    [Inject] public HttpClient Http { get; set; }
    [Inject] public AuthenticationStateProvider AuthStateProvider { get; set; }
    [Inject] public ILocalStorageService LocalStorage { get; set; }

    UserLoginRequest userLogin = new();
    CreateUserRequest userRegister = new();

    bool success;
    bool loginsuccess;
    string[] errors = { };
    MudTextField<string> pwField1;
    string UserRole;

    private IEnumerable<string> PasswordStrength(string pw)
    {
        if (string.IsNullOrWhiteSpace(pw))
        {
            yield return "Password is required!";
            yield break;
        }
        if (pw.Length < 8)
            yield return "Password must be at least of length 8";
        if (!Regex.IsMatch(pw, @"[A-Z]"))
            yield return "Password must contain at least one capital letter";
        if (!Regex.IsMatch(pw, @"[a-z]"))
            yield return "Password must contain at least one lowercase letter";
        if (!Regex.IsMatch(pw, @"[0-9]"))
            yield return "Password must contain at least one digit";
    }

    private string PasswordMatch(string arg)
    {
        if (pwField1.Value != arg)
            return "Passwords don't match";
        return null;
    }


    async Task HandleLogin()
    {
        var result = await Http.PostAsJsonAsync("api/Users/Login", userLogin);
        var token = await result.Content.ReadAsStringAsync();
        await LocalStorage.SetItemAsync("token", token);
        var state = await AuthStateProvider.GetAuthenticationStateAsync();
        UserRole = state.User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(r => r.Value).First();
    }
    async Task RegisterNewUser()
    {
        var result = await Http.PostAsJsonAsync("api/Users/RegisterNewUser", userRegister);
        var token = await result.Content.ReadAsStringAsync();
        Console.WriteLine(token);
    }

    async Task Logout()
    {
        await LocalStorage.RemoveItemAsync("token");
        await AuthStateProvider.GetAuthenticationStateAsync();
    }

    protected override async Task OnInitializedAsync()
    {
        var state = await AuthStateProvider.GetAuthenticationStateAsync();
        if (state.User is not null)
        {
            UserRole = state.User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(r => r.Value).FirstOrDefault();
        }
    }
}
