﻿namespace Bubble.Shared.Models.Request;
public class CreateUserRequest
{
    public string Name { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
}
