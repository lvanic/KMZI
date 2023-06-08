using System.Diagnostics;

int a = 421;
int c = 1663;
int n = 7875;
int seed = 1234;
int count = 10;

for (int i = 0; i < count; i++)
{
    seed = (a * seed + c) % n;
    double random = (double)seed / n;
    Console.WriteLine(random);
}

Console.WriteLine("-------------------");

byte[] key = new byte[] { 123, 125, 41, 84, 203 };
RC4 rc4 = new RC4(key);
Stopwatch stopwatch = new Stopwatch();
stopwatch.Start();
byte[] pseudoRandomBytes = rc4.GeneratePseudoRandomBytes(100);
stopwatch.Stop();
Console.WriteLine(BitConverter.ToString(pseudoRandomBytes));
Console.WriteLine("Time elapsed: {0} ms", stopwatch.ElapsedMilliseconds);

class RC4
{
    private readonly byte[] key;
    private byte[] state = new byte[256];
    private int x, y;

    public RC4(byte[] key)
    {
        this.key = key;
        for (int i = 0; i < 256; i++)
        {
            state[i] = (byte)i;
        }
        int j = 0;
        for (int i = 0; i < 256; i++)
        {
            j = (j + key[i % key.Length] + state[i]) % 256;
            Swap(i, j);
        }
    }

    private void Swap(int i, int j)
    {
        byte temp = state[i];
        state[i] = state[j];
        state[j] = temp;
    }

    public byte[] GeneratePseudoRandomBytes(int length)
    {
        byte[] pseudoRandomBytes = new byte[length];
        for (int k = 0; k < length; k++)
        {
            x = (x + 1) % 256;
            y = (y + state[x]) % 256;
            Swap(x, y);
            pseudoRandomBytes[k] = state[(state[x] + state[y]) % 256];
        }
        return pseudoRandomBytes;
    }
}