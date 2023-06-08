using System.Security.Cryptography;
using System.Text;

string inputPath = "in.txt";
string outputPath = "out.txt";
string keyPath1 = "key1.txt";
string keyPath2 = "key2.txt";

var input = File.ReadAllText(inputPath);
var key1 = File.ReadAllText(keyPath1);
var key2 = File.ReadAllText(keyPath2);

long OldTicks = DateTime.Now.Ticks;
var encoded = Encode(input, key1, key2);
Console.WriteLine($"Время зашифрования: {(DateTime.Now.Ticks - OldTicks) / 1000} мс");

OldTicks = DateTime.Now.Ticks;
var decoded = Decode(encoded, key1, key2);
Console.WriteLine($"Время расшифрования: {(DateTime.Now.Ticks - OldTicks) / 1000} мс");

//Console.WriteLine("\nencoded: {0}", encoded);
//Console.WriteLine("\ndecoded: {0}", decoded);

using StreamWriter sw = new StreamWriter(outputPath, false, Encoding.Unicode);
sw.WriteLine(encoded);


static string Encode(string input, string key1, string key2)
{
    var toEncryptArray = UTF8Encoding.UTF8.GetBytes(input);

    var hashmd5 = new MD5CryptoServiceProvider();
    var keyArray1 = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key1));
    var keyArray2 = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key2));
    hashmd5.Clear();

    var tdes = new TripleDESCryptoServiceProvider();
    tdes.Key = keyArray1;
    tdes.Mode = CipherMode.ECB;
    tdes.Padding = PaddingMode.Zeros;

    var cTransform1 = tdes.CreateEncryptor();
    byte[] resultArray = cTransform1.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
    tdes.Clear();

    tdes.Key = keyArray2;
    var cTransform2 = tdes.CreateDecryptor();
    resultArray = cTransform2.TransformFinalBlock(resultArray, 0, resultArray.Length);
    tdes.Clear();

    tdes.Key = keyArray1;
    var cTransform3 = tdes.CreateEncryptor();
    resultArray = cTransform3.TransformFinalBlock(resultArray, 0, resultArray.Length);
    tdes.Clear();

    return Convert.ToBase64String(resultArray, 0, resultArray.Length);
}

static string Decode(string input, string key1, string key2)
{
    var toEncryptArray = Convert.FromBase64String(input);

    var hashmd5 = new MD5CryptoServiceProvider();
    var keyArray1 = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key1));
    var keyArray2 = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key2));
    hashmd5.Clear();

    var tdes = new TripleDESCryptoServiceProvider();
    tdes.Key = keyArray1;
    tdes.Mode = CipherMode.ECB;
    tdes.Padding = PaddingMode.Zeros;

    var cTransform1 = tdes.CreateDecryptor();
    byte[] resultArray = cTransform1.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
    tdes.Clear();

    tdes.Key = keyArray2;
    var cTransform2 = tdes.CreateEncryptor();
    resultArray = cTransform2.TransformFinalBlock(resultArray, 0, resultArray.Length);
    tdes.Clear();

    tdes.Key = keyArray1;
    var cTransform3 = tdes.CreateDecryptor();
    resultArray = cTransform3.TransformFinalBlock(resultArray, 0, resultArray.Length);
    tdes.Clear();
    return UTF8Encoding.UTF8.GetString(resultArray);
}
