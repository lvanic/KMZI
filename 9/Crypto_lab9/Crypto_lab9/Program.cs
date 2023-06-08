using System.Numerics;
using System.Text;

int encodingLength = 8;
int z = 8;
int a = 31;
string M = "YahorIvanouski";
//byte[] bytes = Encoding.UTF8.GetBytes(M);
//M = Convert.ToBase64String(bytes);
//Console.WriteLine(M);

int[] d = Generate(z);
Console.WriteLine($"Закрытый ключ d: {Str(d)}");
int n = d.Sum(x => x) + 2;

int[] e = GenerateNormalSequence(d, a, n, z);
Console.WriteLine($"Открытый ключ e: {Str(e)}\n");

long OldTicks = DateTime.Now.Ticks;
int[] C = Encrypt(e, M, z);
Console.WriteLine($"\nЗашифров сооб C: {Str(C)}");
Console.WriteLine($"Время: {(DateTime.Now.Ticks - OldTicks) / 1000} мс\n");

int a_1Res = FindInverseModulo(a, n);

int[] S = new int[C.Length];
string M2 = "";

for (int i = 0; i < C.Length; i++)
{
    S[i] = (C[i] * a_1Res) % n;
}
Console.WriteLine($"Вектор  весов S: {Str(S)}      a^(-1) = {a_1}");

OldTicks = DateTime.Now.Ticks;
foreach (int Si in S)
{
    string M2i = Decrypt(d, Si, z);
    M2 += M2i + " ";
}

Console.WriteLine($"Время: {(DateTime.Now.Ticks - OldTicks) / 1000} мс\n");
M2 = M2.Replace(" ", "");
var stringArray = Enumerable.Range(0, M2.Length / encodingLength).Select(i => Convert.ToByte(M2.Substring(i * encodingLength, encodingLength), 2)).ToArray();
var str = Encoding.UTF8.GetString(stringArray);
//var str = Convert.ToBase64String(stringArray);
Console.WriteLine($"Расшифрованное сообщение: {str}");

int[] Generate(int z)
{
    Random rnd = new Random();
    int[] k = new int[z];
    int sum = 3;
    for (int i = 0; i < z; i++)
    {
        var value = sum + 1;
        k[i] = value;
        if (i == 0)
        {
            sum += k[i];
        }
        while (k[i] < sum)
        {
            k[i] += value;
        }
        sum += k[i];
    }
    return k;
}
int[] GenerateNormalSequence(int[] d, int a, int n, int z)
{
    int[] e = new int[z];

    for (int i = 0; i < z; i++)
    {
        e[i] = (d[i] * a) % n;
    }
    return e;
}

int[] Encrypt(int[] e, string M, int z)
{
    int j = 0;
    int[] result = new int[M.Length];
    int total = 0;
    foreach (char Mi in M)
    {
        total = 0;
        string Mi2 = '0' + GetBytes(Mi.ToString());
        for (int i = 0; i < Mi2.Length; i++)
        {
            if (Mi2[i] == '1') total += e[i];
        }
        result[j] = total;
        j++;
    }
    return result;
}
string Decrypt(int[] d, int Si, int z)
{
    string res = "";
    string res2 = "";

    for (int i = z; i > 0; i--)
    {
        if (Si >= d[i - 1])
        {
            res += '1';
            Si = Si - d[i - 1];
        }
        else
        {
            res += '0';
        }
    }
    res = res.Remove(0, z - encodingLength);
    for (int i = res.Length - 1; i > -1; i--)
    {
        res2 += res[i];
    }
    return res2;
}

int a_1(int a, int n)
{
    int res = 0;
    for (int i = 0; i < 10000; i++)
    {
        if (((a * i) % n) == 1) return (i);
    }
    return (res);
}

string Str(int[] a)
{
    string res = "";
    foreach (int x in a)
    {
        res += x.ToString() + "; ";
    }
    return res;
}

string GetBytes(String str)
{
    String strB = "";
    for (int i = 0; i < str.Length; i++)
    {
        strB += Convert.ToString((int)str[i], 2);
    }
    if(strB.Length == 6)
    {
        strB = strB.Insert(0, "0");
    }
    return strB;
}

static int FindInverseModulo(int number, int modulo)
{
    for (int i = 1; i < modulo; i++)
    {
        if ((number * i) % modulo == 1)
        {
            return i;
        }
    }
    return -1; // Обратное число не найдено
}