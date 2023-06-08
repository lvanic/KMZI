using System.Diagnostics;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;

int g_main = 0;
BigInteger a = 0;

// Исходный текст для шифрования
string plainText = "qwertyuiopasdfghjklzqwertyuiopasdfghjklz";

// Ключи для шифрования RSA
RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
string publicKey = rsa.ToXmlString(false);
string privateKey = rsa.ToXmlString(true); 
                                           
// Зашифрование RSA                        
Stopwatch swRSAEncrypt = new Stopwatch();  
swRSAEncrypt.Start();                      
byte[] encryptedRSA = RSAEncrypt(Encoding.UTF8.GetBytes(plainText), publicKey);
swRSAEncrypt.Stop();
Console.WriteLine($"RSA encryption time: {swRSAEncrypt.Elapsed}");
Console.Write($"RSA decrypted text: ");
foreach (byte b in encryptedRSA)
{
    Console.Write(Convert.ToChar(b));
}
Console.WriteLine("");

// Расшифрование RSA
Stopwatch swRSADecrypt = new Stopwatch();
swRSADecrypt.Start();
string decryptedRSA = Encoding.UTF8.GetString(RSADecrypt(encryptedRSA, privateKey));
swRSADecrypt.Stop();
Console.WriteLine($"RSA decryption time: {swRSADecrypt.Elapsed}");
Console.WriteLine($"RSA decrypted text: {decryptedRSA}");

//Гамаля

int p = 0;
Random random = new Random();


p = Search_p();


int x = random.Next(1, p - 1); //Генерирую закрытый ключ
BigInteger y = BigInteger.Pow(g_main, x) % p; //Нахожу открытый ключ


Console.Write($"Числа [{p}, {g_main}, {y}] ---> [p, g, y]\n");
Console.Write($"Открытый ключ - это <y>\n");
Console.Write($"Закрытый ключ: {x}\n");


Console.WriteLine();

//Зашифрование
long OldTicks = DateTime.Now.Ticks;
List<BigInteger> array_cipher_text = new List<BigInteger>();
array_cipher_text = Cipher(plainText, p, y);
Console.WriteLine("Зашифрованное сообщение: ");
for (int i = 0; i != plainText.Length; i++)
{
    Console.WriteLine($"{i}:[{a}, {array_cipher_text[i]}] ");
}
Console.WriteLine($"Время: {(DateTime.Now.Ticks - OldTicks) / 1000} мс\n\n");


//Расшифрование
OldTicks = DateTime.Now.Ticks;
Console.Write($"Расшифрованное сообщение: {plainText = Cipher_RAZ(plainText.Length, array_cipher_text, x, p)}\n");
Console.WriteLine($"Время: {(DateTime.Now.Ticks - OldTicks) / 1000} мс\n");
////////////////////////////////////////////


/*Definition number <g>*/
bool Search_g(int p, int g)
{
    bool boolean = false;
    List<BigInteger> array_mod_number = new List<BigInteger>();

    BigInteger integer = ((BigInteger.Pow(g, 1)) % p);
    array_mod_number.Add(integer);

    for (int i = 2; i != p; i++)
    {
        integer = BigInteger.Pow(g, i) % p;
        for (int j = 0; j != i - 1; j++)
        {
            if (array_mod_number[j] == integer)
            {
                g--;
                array_mod_number.Clear();
                i = 1;
                integer = BigInteger.Pow(g, 1) % p;
                array_mod_number.Add(integer);
                break;
            }

            if ((j == i - 2) && (array_mod_number[j] != integer))
            {
                array_mod_number.Add(integer);
            }
        }
    }
    g_main = g;
    boolean = true;
    return boolean;
}


/*Definition simple number <p>*/
int Search_p()
{
    Random random = new Random();
    int p = 0;
    Boolean boolean = false;
    do
    {
        p = random.Next(2000, 2500);

        for (int i = 2; i != p; i++)
        {
            if (i == p - 1)
            {
                boolean = Search_g(p, p - 1);
                break;
            }
            if (p % i == 0) break;
        }
    }
    while (boolean == false);
    return p;
}


/*Cipher*/
List<BigInteger> Cipher(string text, int p, BigInteger y)
{
    List<BigInteger> array = new List<BigInteger>();
    Random random = new Random();
    int k = random.Next(1, p - 1);


    for (int i = 0; i != text.Length; i++)
    {
        a = BigInteger.Pow(g_main, k) % p;
        array.Add((BigInteger.Pow(y, k) * (int)text[i]) % p);
    }
    return array;
}


/*Расшифровка*/
string Cipher_RAZ(int length_text, List<BigInteger> array_number, int x, int p)
{
    string save_text = "";
    BigInteger integer;

    for (int i = 0; i != length_text; i++)
    {
        integer = (array_number[i] * (BigInteger.Pow(a, p - 1 - x))) % p;
        save_text += (char)integer;
    }
    return save_text;
}



// Шифрование RSA
byte[] RSAEncrypt(byte[] data, string publicKey)
{
    RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
    rsa.FromXmlString(publicKey);
    return rsa.Encrypt(data, true);
}

// Расшифрование RSA
static byte[] RSADecrypt(byte[] data, string privateKey)
{
    RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();

    rsa.FromXmlString(privateKey);
    return rsa.Decrypt(data, true);
}

static void GenerateKeys(out BigInteger p, out BigInteger g, out BigInteger x, out BigInteger y)
{
    // Генерация простого числа p
    using (var rng = new RNGCryptoServiceProvider())
    {
        byte[] bytes = new byte[32]; // 256-bit key
        rng.GetBytes(bytes);
        bytes[bytes.Length - 1] |= 0x80; // ensure the highest bit is set to make it a large prime
        p = new BigInteger(bytes);
    }

    // Генерация примитивного корня g
    g = GetPrimitiveRoot(p);

    // Генерация закрытого ключа x
    using (var rng = new RNGCryptoServiceProvider())
    {
        byte[] bytes = new byte[32]; // 256-bit key
        rng.GetBytes(bytes);
        x = new BigInteger(bytes) % (p - 2) + 1; // 1 <= x <= p-1
    }

    // Вычисление открытого ключа y
    y = BigInteger.ModPow(g, x, p);
}
static BigInteger GetPrimitiveRoot(BigInteger p)
{
    // Поиск примитивного корня g
    BigInteger eulerPhi = p - 1;
    BigInteger[] primeFactors = eulerPhi.GetPrimeFactors();
    for (BigInteger g = 2; g < p; g++)
    {
        bool isPrimitiveRoot = true;
        foreach (BigInteger factor in primeFactors)
        {
            if (BigInteger.ModPow(g, eulerPhi / factor, p) == 1)
            {
                isPrimitiveRoot = false;
                break;
            }
        }
        if (isPrimitiveRoot)
            return g;
    }
    throw new Exception("Primitive root not found");
}
static BigInteger[] Encrypt(string message, BigInteger p, BigInteger g, BigInteger y)
{
    // Преобразование сообщения в массив байтов
    byte[] messageBytes = System.Text.Encoding.UTF8.GetBytes(message);

    // Шифрование каждого байта сообщения
    BigInteger[] encryptedMessage = new BigInteger[messageBytes.Length];
    using (var rng = new RNGCryptoServiceProvider())
    {
        for (int i = 0; i < messageBytes.Length; i++)
        {
            // Генерация случайного числа k
            byte[] kBytes = new byte[32]; // 256-bit key
            rng.GetBytes(kBytes);
            BigInteger k = new BigInteger(kBytes) % (p - 2) + 1; // 1 <= k <= p-1

            // Вычисление шифротекста
            BigInteger a = BigInteger.ModPow(g, k, p);
            BigInteger b = (BigInteger.ModPow(y, k, p) * messageBytes[i]) % p;
            encryptedMessage[i] = a;
            encryptedMessage[i + messageBytes.Length] = b;
        }
    }

    return encryptedMessage;
}

static string Decrypt(BigInteger[] encryptedMessage, BigInteger p, BigInteger x)
{
    // Расшифровка каждого блока шифротекста
    byte[] decryptedBytes = new byte[encryptedMessage.Length / 2];
    for (int i = 0; i < decryptedBytes.Length; i++)
    {
        BigInteger a = encryptedMessage[i];
        BigInteger b = encryptedMessage[i + decryptedBytes.Length];

        // Вычисление обратного элемента
        BigInteger aInverse = BigInteger.ModPow(a, p - 1 - x, p);

        // Расшифровка байта
        decryptedBytes[i] = (byte)((b * aInverse) % p);
    }

    // Преобразование расшифрованных байтов в строку
    string decryptedMessage = System.Text.Encoding.UTF8.GetString(decryptedBytes);
    return decryptedMessage;
}


static class BigIntegerExtensions
{
    public static BigInteger[] GetPrimeFactors(this BigInteger number)
    {
        // Вычисление простых делителей числа
        var primeFactors = new System.Collections.Generic.List<BigInteger>();
        BigInteger currentNumber = number;
        BigInteger divisor = 2;
        while (currentNumber > 1)
        {
            if (currentNumber % divisor == 0)
            {
                primeFactors.Add(divisor);
                currentNumber /= divisor;
            }
            else
            {
                divisor++;
            }
        }
        return primeFactors.ToArray();
    }
}