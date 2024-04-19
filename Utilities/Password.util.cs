
using System.Security.Cryptography;
using System.Text;

namespace JobNet.Utilities;

public static class PasswordUtil
{
    const int keySize = 64;
    const int iterations = 350000;
    static HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;
    public static string HashPassword(string password, out byte[] salt)
    {
        salt = RandomNumberGenerator.GetBytes(keySize);
        var hash = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(password),
            salt,
            iterations,
            hashAlgorithm,
            keySize);
        return Convert.ToHexString(hash);
    }
    public static bool VerifyPassword(string password, string hash, byte[] salt)
    {
        var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, hashAlgorithm, keySize);
        return CryptographicOperations.FixedTimeEquals(hashToCompare, Convert.FromHexString(hash));
    }
    public static string GenerateRandomPassword(int minLength, int maxLength)
    {
        string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()-=_+";
        StringBuilder passwordBuilder = new();
        Random random = new();
        int passwordLength = random.Next(minLength, maxLength + 1);
        for (int i = 0; i < passwordLength; i++)
        {
            int randomIndex = random.Next(0, validChars.Length);
            char randomChar = validChars[randomIndex];
            passwordBuilder.Append(randomChar);
        }
        return passwordBuilder.ToString();
    }
}