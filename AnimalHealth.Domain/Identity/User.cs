﻿namespace AnimalHealth.Domain.Identity;

public class User
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Login { get; set; }
    public string? Password { get; set; }
}