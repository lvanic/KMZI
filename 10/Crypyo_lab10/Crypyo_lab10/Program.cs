using System.Diagnostics;

int[] aValues = { 5, 10, 15, 20, 25, 30, 35 };
ulong[] xValues = { 1000000007, 1000000009, 1000000021, 1000000033, 1000000087 };
ulong[] nValues ={
            20000000000,
            40000000000
        };

Stopwatch stopwatch = new Stopwatch();
foreach (int a in aValues)
{
    foreach (ulong x in xValues)
    {
        foreach (ulong n in nValues)
        {
            stopwatch.Reset();
            stopwatch.Start();
            ulong y = ModuloMultiplication(a, x, n);
            Thread.Sleep(1);
            stopwatch.Stop();

            Console.WriteLine($"a = {a}, x = {x}, n = {Convert.ToString((long)n, 2)}, y = {y}, time = {stopwatch.ElapsedMilliseconds} ms");
        }
    }
}

static ulong ModuloMultiplication(int a, ulong x, ulong n)
{
    ulong result = 1;
    ulong temp = (ulong)a;

    while (x > 0)
    {
        if ((x & 1) == 1)
        {
            result = (result * temp) % n;
        }
        temp = (temp * temp) % n;
        x >>= 1;
    }

    return result;
}
