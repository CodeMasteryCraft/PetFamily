using System.Security.Cryptography;
using PasswordGenerator;

namespace PetFamily.Domain.Common;

public static class RandomPassword
{
    public static string Generate(int length = 16)
    {
        var pwd = new Password(length);
        var password = pwd.Next();
        return password;
    }
    
    public static string OwnMethod(int length = 12)
    {
        const string passwordOptions = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()";
        var password = new string(Enumerable.Repeat(passwordOptions, length).Select(s => s[RandomNumberGenerator.GetInt32(s.Length)]).ToArray());
        return password;
    }

    public static string OnlyDigit(int length = 4)
    {
        var pwd = new Password(length).IncludeNumeric();
        var password = pwd.Next();
        return password;
    }
}