﻿namespace UserManagement.DataAccess.Models;

public class User
{
    public Guid Id { get; set; }
    public required string Email { get; set; }
    public required string PasswordHash { get; set; }
    public Guid ProvinceId { get; set; }
}
