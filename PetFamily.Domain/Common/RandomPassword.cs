using System.Security.Cryptography;

namespace PetFamily.Domain.Common;

public static class RandomPassword
{
    public static string Generate(int length = 12)
    {
        const string passwordOptions = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        var password = new string(Enumerable.Repeat(passwordOptions, length)
            .Select(s => s[RandomNumberGenerator.GetInt32(s.Length)])
            .ToArray());

        return password;
    }
}