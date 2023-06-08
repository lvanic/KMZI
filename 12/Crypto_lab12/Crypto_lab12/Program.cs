using System.Numerics;
using System.Security.Cryptography;
using System.Text;

Console.WriteLine($"ЭЦП на основе Эль-Гамаля");

int p = 2137;
int g = 2127;
int x = 1116;
int y = (int)BigInteger.ModPow(g, x, p);

int k = 7;
int a = (int)BigInteger.ModPow(g, k, p);
Console.WriteLine($" p={p}\n g={g}\n x={x}\n y={y}\n k={k}\n a={a}\n\n");

int H = 2119;
int m = p - 1;
int k_1 = obr(k, p - 1);
var b = new BigInteger((k_1 * (H - (x * a) % m) % m) % m);
Console.WriteLine($" H={H}\n k_1={k_1}\n b={b}\n S = {a},{b} \n\n");

Console.WriteLine("\n Верификация:");
var ya = BigInteger.ModPow(y, a, p);
var ab = BigInteger.ModPow(a, b, p);
var pr1 = BigInteger.ModPow(ya * ab, 1, p);
var pr2 = BigInteger.ModPow(g, H, p);

if (pr1 == pr2)
    Console.WriteLine($" {pr1}  =  {pr2}\n Верификация пройдена успешно");

Console.WriteLine($"\nЭЦП на основе RSA\n");

var rsa = new RSA();
string M = File.ReadAllText("in.txt");
p = 101;
int q = 103;
string hash = M.GetHashCode().ToString();
int n = p * q;
m = (p - 1) * (q - 1);
int d = rsa.Calculate_d(m);
int e_ = rsa.Calculate_e(d, m);
Console.WriteLine($" p = {p}\n q = {q}\n n = {n}\n ф(n) = {m}\n d = {d}\n e = {e_}\n M = {M}\n Хеш = {hash}\n");
List<string> sign = rsa.RSA_Encode(hash, e_, n);

while (true)
{
    var key = Console.ReadKey();
    if (key.Key != ConsoleKey.Escape)
    {
        List<string> input = new List<string>();
        string hash2 = File.ReadAllText("in.txt").GetHashCode().ToString();

        string result = rsa.RSA_Decode(sign, d, n);
        Console.WriteLine($"Хэш эл подписи = {result}");
        Console.WriteLine($"Хэш файла = {hash2}");

        if (result.Equals(hash2)) Console.WriteLine("Файл подлинный. Подпись верна. \n");
        else Console.WriteLine("Внимание! Файл НЕ подлинный!!!\n");
    }
    else
    {
        break;
    }
}

Console.WriteLine($"ЭЦП на основе Шнорра");
Console.InputEncoding = Encoding.ASCII;
var t = DateTime.Now;
Shnorr.Do();
Console.WriteLine("Shnorr:" + (DateTime.Now - t));

static int obr(int a, int n)
{
    int res = 0;
    for (int i = 0; i < 10000; i++)
    {
        if (((a * i) % n) == 1) return (i);
    }
    return (res);
}

class RSA
{
    public static readonly char[] characters = new char[] { '#', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '-' };

    //проверка: простое ли число?
    public bool IsTheNumberSimple(long n)
    {
        if (n < 2) return false;

        if (n == 2) return true;

        for (long i = 2; i < n; i++)
            if (n % i == 0) return false;

        return true;
    }

    //вычисление параметра e
    public int Calculate_e(int d, int m)
    {
        int e = 10;

        while (true)
        {
            if ((e * d) % m == 1) break;
            else e++;
        }
        return (int)e;
    }

    //вычисление параметра d. d должно быть взаимно простым с m
    public int Calculate_d(int m)
    {
        int d = m - 1;

        for (int i = 2; i <= m; i++)
            if ((m % i == 0) && (d % i == 0)) ///если имеют общие делители
            {
                d--;
                i = 1;
            }
        return d;
    }

    //зашифрование
    public List<string> RSA_Encode(string hash, int e, int n)
    {

        List<string> result = new List<string>();

        BigInteger bi;

        for (int i = 0; i < hash.Length; i++)
        {
            int index = Array.IndexOf(characters, hash[i]);

            bi = new BigInteger(index);
            bi = BigInteger.Pow(bi, (int)e);

            BigInteger n_ = new BigInteger((int)n);

            bi = bi % n_;
            result.Add(bi.ToString());
        }
        return result;
    }

    //расшифровать
    public string RSA_Decode(List<string> input, int d, int n)
    {
        try
        {
            string result = "";
            BigInteger bi;

            foreach (string item in input)
            {
                bi = new BigInteger(Convert.ToDouble(item));
                bi = BigInteger.Pow(bi, (int)d);

                BigInteger n_ = new BigInteger((int)n);

                bi = bi % n_;
                int index = Convert.ToInt32(bi.ToString());
                result += characters[index].ToString();
            }

            return result;
        }
        catch (Exception ex) { return ""; }
    }
}

public static class Shnorr
{
    public static void Do()
    {
        BigInteger p = 2267;
        BigInteger q = 103;

        string text = File.ReadAllText(".\\Test.txt");
        BigInteger g = 354;
        BigInteger obg = 967;
        int x = 30;

        BigInteger y = BigInteger.ModPow(obg, x, p);
        BigInteger a = BigInteger.Pow(g, 13) % p;
        BigInteger hash = ElGamal.CalculateMd5Hash(text + a.ToString());

        File.WriteAllText(".\\shnorr.txt", hash.ToString());
        BigInteger b = (13 + x * hash) % q;
        BigInteger dov = BigInteger.ModPow(g, b, p);
        BigInteger X = (dov * BigInteger.ModPow(y, hash, p)) % p;
        BigInteger hash2 = ElGamal.CalculateMd5Hash((text + X.ToString()));

        var f = hash == hash2;
        Console.WriteLine(f);
        string text2 = File.ReadAllText(".\\FakeTest.txt");
        BigInteger hash3 = ElGamal.CalculateMd5Hash((text2 + X.ToString()));
        var f2 = hash == hash3;
        Console.WriteLine(f2);
    }
}

public static class ElGamal
{
    public static BigInteger CalculateMd5Hash(string input)
    {
        var md5 = MD5.Create();
        var inputBytes = Encoding.ASCII.GetBytes(input);
        var hash = md5.ComputeHash(inputBytes);
        return new BigInteger(hash.Concat(new byte[] { 0 }).ToArray());
    }
}