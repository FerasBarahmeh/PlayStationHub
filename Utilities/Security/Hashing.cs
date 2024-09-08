using System.Security.Cryptography;
using System.Text;

namespace Utilities.Security;

public class Hashing
{
    public static string Hash(string Data)
    {
        if (Data == null) return "";
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] Hashed = sha256.ComputeHash(Encoding.UTF8.GetBytes(Data));
            return BitConverter.ToString(Hashed).Replace("-", "").ToLower();
        }
    }
    public static bool CompareHashed(string NormalString, string HashedString)
    {
        return Hash(NormalString) == HashedString;
    }
}
