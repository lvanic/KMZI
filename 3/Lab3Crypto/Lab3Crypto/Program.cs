var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapGet("/getgcd", (long a, long b, long? c) =>
{
    if (c != null)
    {
        return NumberCalculator.GCD(a, b, c.Value);
    }
    else
    {
        return NumberCalculator.GCD(a, b);
    }

})
.WithName("GetGCD");

app.MapGet("/getprimes", (long m, long n, bool isSlowOn) =>
{
    var start1 = DateTime.Now;
    var result1 = NumberCalculator.FindPrimesFast(m, n);
    var end1 = DateTime.Now;
    var start2 = DateTime.Now;
    List<long> result2 = new List<long>();
    if (isSlowOn)
    {
        result2 = NumberCalculator.FindPrimes(m, n);
    }

    var end2 = DateTime.Now;
    return new
    {
        Fast = new
        {
            result = result1,
            count = result1.Count,
            time = end1 - start1,
            log = n/Math.Log(n)
        },
        Slow = new
        {
            result = result2,
            count = result2.Count,
            time = end2 - start2,
            log = n / Math.Log(n)   
        }
    };

})
.WithName("GetPrimes");

app.MapGet("/prime", (ulong number) =>
{
    return NumberCalculator.IsPrime(number);
}).WithName("IsNumberPrime");

app.MapGet("/prime-factorization", (long number) =>
{
    return NumberCalculator.PrimeFactorization(number, 2);
}).WithName("GetPrimeFactorization");

app.Run();

const int m = 499;
const int n = 531;
static class NumberCalculator
{
    public static List<string> PrimeFactorization(long number, int buf)
    {
        if (IsPrime(number))
        {
            return new List<string> { "1", Convert.ToString(number) };
        }
        List<string> result = new List<string>
        {
            "1"
        };
        long cycleCount = number;
        int pow = 1;
        for (int i = 2; 2 * i <= cycleCount;)
        {
            if (number % i == 0)
            {
                number /= i;
                if (result.Contains($"{i}"))
                {
                    result.Remove(result.Last());
                    pow++;
                    result.Add($"{i} ^ {pow}");

                }
                else
                {
                    result.Add($"{i}");
                }
            }
            else
            {
                pow = 1;
                i++;
            }
        }
        return result;
    }

    //Find the greatest common divisor of the numbers m and n.
    public static long GCD(long a, long b)
    {
        if (a == 0)
            return b;
        return GCD(b % a, a);
    }
    public static long GCD(long a, long b, long c)
    {
        return GCD(GCD(a, b), c);
    }

    public static List<long> FindPrimes(long m, long n)
    {
        List<long> primes = new List<long>();
        for (long i = m; i <= n; i++)
        {
            if (IsPrime(i))
            {
                primes.Add(i);
            }
        }
        return primes;
    }

    public static bool IsPrime(long n)
    {
        if (n <= 1)
            return false;
        if (n <= 3)
            return true;
        if (n % 2 == 0 || n % 3 == 0)
            return false;
        for (long i = 5; i * i <= n; i = i + 6)
            if (n % i == 0 || n % (i + 2) == 0)
                return false;
        return true;
    }
    public static bool IsPrime(ulong n)
    {
        if (n <= 1)
            return false;
        if (n <= 3)
            return true;
        if (n % 2 == 0 || n % 3 == 0)
            return false;
        for (ulong i = 5; i * i <= n; i = i + 6)
            if (n % i == 0 || n % (i + 2) == 0)
                return false;
        return true;
    }

    public static List<long> FindPrimesFast(long m, long n)
    {
        List<long> primes = new List<long>();
        bool[] isPrime = new bool[n + 1];

        for (long i = 0; i <= n; i++)
        {
            isPrime[i] = true;
        }

        for (long p = m; p * p <= n; p++)
        {
            if (isPrime[p] == true)
            {
                for (long i = p * p; i <= n; i += p)
                {
                    isPrime[i] = false;
                }
            }
        }

        for (long i = m; i <= n; i++)
        {
            if (isPrime[i] == true)
            {
                primes.Add(i);
            }
        }
        return primes;
    }

}