using System.Diagnostics;

int xx;
double yy;
for (xx = 141; xx < 175; xx++)
{
    yy = Math.Pow(xx, 3) - xx + 1;
    yy = yy % 751;
    Console.Write($"({xx},{yy}); ");
}

Point P = new Point(59, 386);
Point Q = new Point(70, 195);
Point R = new Point(72, 254);
int k = 11, l = 5;

Point p1 = P * k;
Point p2 = P + Q;
Point p3 = p1 + (Q * l) + (-R);
Point p4 = P + (-Q) + R;

Console.WriteLine("\n\nЗадание 1:");
Console.WriteLine($"kP          ({p1.x}, {p1.y})");
Console.WriteLine($"P+Q         ({p2.x}, {p2.y})");
Console.WriteLine($"kP+lQ-R     ({p3.x}, {p3.y})");
Console.WriteLine($"P-Q+R       ({p4.x}, {p4.y})\n\n\n");

var sw = new Stopwatch();
sw.Start();
Point m1 = new Point(194, 546); // е
Point m2 = new Point(192, 719); // г
Point m3 = new Point(205, 372); // о
Point m4 = new Point(206, 106); // р
Point[] M = { m1, m2, m3, m4 };
Point[] C = new Point[8];
Point[] M2 = new Point[4];
Point G = new Point(0, 1);
int d = 29;
int j = 0;
Point Q_ = G * d;

Console.WriteLine("\nЗадание 2:");
Console.WriteLine($"Тайный ключ: {d}");
Console.WriteLine($"Открытый ключ: ({Q_.x}, {Q_.y})");

for (int i = 0; i < 4; i++)
{
    C[j] = G * k; 
    j++;
    C[j] = M[i] + Q_ * k; 
    j++;
}

Console.WriteLine($"Открытый текст: ({M[0].x}, {M[0].y}), ({M[1].x}, {M[1].y}), ({M[2].x}, {M[2].y}), ({M[3].x}, {M[3].y})");
Console.WriteLine($"Шифротекст:     ({C[0].x}, {C[0].y}), ({C[1].x}, {C[1].y}), ({C[2].x}, {C[2].y}), \n" +
    $"\t\t({C[3].x}, {C[3].y}), ({C[4].x}, {C[4].y}), ({C[5].x}, {C[5].y}), ({C[6].x}, {C[6].y}), " +
    $"({C[7].x}, {C[7].y})");

M2[0] = C[1] + ((-C[0]) * d);
M2[1] = C[3] + ((-C[2]) * d);
M2[2] = C[5] + ((-C[4]) * d);
M2[3] = C[7] + ((-C[6]) * d);
sw.Stop();

Console.WriteLine($"Расшифр текст: ({M2[0].x}, {M2[0].y}), ({M2[1].x}, {M2[1].y}), ({M2[2].x}, {M2[2].y}), ({M2[3].x}, {M2[3].y})\n\n\n");

d = 10;
Point G_ = new Point(416, 55);
int q = 13;
Point Q_sign = G_ * d;
int H = 198 % 13; //И
Point kG = G_ * k;

int r = (int)kG.x % q;
if (r == 0) Console.WriteLine("Замените параметр k");

int t = a_1(k, q);

int s = (H * t + d * r) % q;
if (s == 0) Console.WriteLine("Замените параметр k");

Console.WriteLine("Задание 3:");
Console.WriteLine($"Открытый ключ Q: ({Q_sign.x}, {Q_sign.y})");
Console.WriteLine($"Точка kG: ({kG.x}, {kG.y})");
Console.WriteLine($"Хеш: {H}");
Console.WriteLine($"Отсылаем стороне B (r,s) = ({r},{s})");

if (r < 1 || s > q) Console.WriteLine("Не подтверждено!");
int w = a_1(s, q);
int u1 = (w * H) % q;
int u2 = (w * r) % q;

Point ver = G_ * u1 + Q_sign * u2;
int v = (int)ver.x % q;
if (v != r) Console.WriteLine("Подтверждено!");

Console.WriteLine($"Время затраченное:{sw.Elapsed}");

static int a_1(int a, int n)
{
    int res = 0;
    for (int i = 0; i < 10000; i++)
    {
        if (((a * i) % n) == 1) return (i);
    }
    return (res);
}

class Point
{
    static int p = 751;
    public static int a = -1;
    public static int b = 1;
    public long x;
    public long y;

    public Point(long x, long y)
    {
        this.x = x;
        this.y = y;
    }

    static public void DrawFunction()
    {
        for (int i = 4; i < 10; i++)
        {
            int y = (int)Math.Sqrt((Math.Pow(i, 3) + (a * i) + b) % p);
            Console.SetCursorPosition(Console.CursorTop + y / 1000, Console.CursorLeft + i);
            Console.Write("*");
        }
        Console.ForegroundColor = ConsoleColor.White;
    }

    public static long GetLambda(Point p1, Point p2)
    {
        long lambda;

        if (p1 == p2)
        {
            long a, b;
            long g = gcd(2 * p1.y, Point.p, out a, out b);

            lambda = ((3 * p1.x * p1.x + Point.a) * a);
        }
        else
        {
            long a, b;
            long dx = p2.x - p1.x;
            dx = (dx % Point.p + Point.p) % Point.p;
            long dy = p2.y - p1.y;
            dy = (dy % Point.p + Point.p) % Point.p;
            long g = gcd(dx, Point.p, out a, out b);

            lambda = (dy * a);
        }
        lambda = (lambda % Point.p + Point.p) % Point.p;

        return lambda;
    }

    static long gcd(long a, long b, out long x, out long y)
    {
        if (a == 0)
        {
            x = 0; y = 1;
            return b;
        }
        long x1, y1;
        long d = gcd(b % a, a, out x1, out y1);
        x = y1 - (b / a) * x1;
        y = x1;
        return d;
    }

    // P + Q

    public static Point operator +(Point p1, Point p2)
    {
        long x;
        long y;
        long lambda = Point.GetLambda(p1, p2);

        if (p1.x == p2.x && p2.y == -(p1.y))
        {
            return new Point(0, 0);
        }

        x = lambda * lambda - p1.x - p2.x;
        x = (x % Point.p + Point.p) % Point.p;
        y = lambda * (p1.x - x) - p1.y;
        y = (y % Point.p + Point.p) % Point.p;

        return new Point(x, y);
    }

    // P * k

    public static Point operator *(Point p1, int multiplier)
    {
        Point tmp = p1;

        for (int i = 1; i < multiplier; ++i)
        {
            tmp += p1;
        }

        return new Point(tmp.x, tmp.y);
    }

    // - P

    public static Point operator -(Point a)
    {
        return a * (Point.p - 1);
    }

    // P == Q

    public static bool operator ==(Point p1, Point p2)
    {
        return p1.x == p2.x && p1.y == p2.y;
    }

    // P != Q

    public static bool operator !=(Point p1, Point p2)
    {
        return p1.x != p2.x && p1.y != p2.y;
    }


    public static int FindOrder(Point G)
    {
        Point tmp;

        for (int i = 1; i < 70000; i++)
        {
            tmp = G;
            tmp *= i;

            if (tmp == new Point(0, 0))
                return i;
        }

        return -1;
    }
}