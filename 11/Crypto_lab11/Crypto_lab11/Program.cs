using System.Text;

Console.WriteLine($"-----------------------SHA256-----------------------");
long OldTicks = DateTime.Now.Ticks;
string text = "1234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890";
string salt = CreateSalt(15);
string hash = GenerateSHA256(text, salt);

Console.WriteLine("M:  " + text + "\nСоль: " + salt + "\nХэш:  " + hash);
Console.WriteLine($"Время: {(DateTime.Now.Ticks - OldTicks) / 1000} мс");

static string CreateSalt(int size)
{
    var rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
    var buff = new byte[size];
    rng.GetBytes(buff);
    return Convert.ToBase64String(buff);
}

static string GenerateSHA256(string input, string salt)
{
    byte[] bytes = System.Text.Encoding.UTF8.GetBytes(input + salt);
    System.Security.Cryptography.SHA256Managed sha256hashstring = new System.Security.Cryptography.SHA256Managed();
    byte[] hash = sha256hashstring.ComputeHash(bytes);
    return ToHex(hash);
}

static string ToHex(byte[] ba)
{

    StringBuilder hex = new StringBuilder(ba.Length * 2);
    foreach (byte b in ba)
    {
        hex.AppendFormat("{0:x2}", b);
    }
    return hex.ToString();
}