﻿namespace BuildingBlocks.Cryptography.Abstractions;
public interface IPasswordValidator
{
    bool ValidatePassword(string password, string hashedPassword);
}
