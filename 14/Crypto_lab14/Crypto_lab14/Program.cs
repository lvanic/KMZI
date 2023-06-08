using System.Drawing;
using System.Runtime.InteropServices;

const int GENERIC_READ = unchecked((int)0x80000000);
const int GENERIC_WRITE = 0x40000000;
const int FILE_SHARE_READ = 1;
const int FILE_SHARE_WRITE = 2;
const int INVALID_HANDLE_VALUE = -1;
const int OPEN_EXISTING = 3;

string containerPath = $"C:\\work\\img\\container.bmp"; // Путь к фотографии
string catPath = $"C:\\work\\img\\cat.bmp";
string outputPath = $"C:\\work\\img\\output.bmp";

var result = HideImageInLSB(containerPath, catPath, outputPath, true, true);

//GenerateColorMatrices(new Bitmap(containerPath));
//GenerateColorMatrices(result);

//byte[] dataToEmbed = Encoding.UTF8.GetBytes("03<<33?");
//var image = new Bitmap(catPath);

//LeastSignificantBrightness.EmbedData(image, dataToEmbed);

//// Извлечение данных из осажденного изображения
//byte[] extractedData = LeastSignificantBrightness.ExtractData(image, dataToEmbed.Length * 8);
//string extractedText = Encoding.UTF8.GetString(extractedData);
//Console.WriteLine("Извлеченные данные: " + extractedText);

static void GenerateColorMatrices(Bitmap containerImagePath)
{
    // Загрузка контейнерного изображения
    Bitmap containerImage = new Bitmap(containerImagePath);

    // Создание черно-белых изображений для каждого цветового канала
    Bitmap redImage = new Bitmap(containerImage.Width, containerImage.Height);
    Bitmap greenImage = new Bitmap(containerImage.Width, containerImage.Height);
    Bitmap blueImage = new Bitmap(containerImage.Width, containerImage.Height);

    // Перебор пикселей контейнерного изображения
    for (int y = 0; y < containerImage.Height; y++)
    {
        for (int x = 0; x < containerImage.Width; x++)
        {
            // Получение цвета пикселя
            Color pixel = containerImage.GetPixel(x, y);

            // Извлечение младшего бита из каждого цветового канала
            byte r = (byte)(pixel.R & 0x01);
            byte g = (byte)(pixel.G & 0x01);
            byte b = (byte)(pixel.B & 0x01);

            // Установка цвета пикселя в соответствующем черно-белом изображении
            Color redPixel = Color.FromArgb(r * 255, r * 255, r * 255);
            Color greenPixel = Color.FromArgb(g * 255, g * 255, g * 255);
            Color bluePixel = Color.FromArgb(b * 255, b * 255, b * 255);

            redImage.SetPixel(x, y, redPixel);
            greenImage.SetPixel(x, y, greenPixel);
            blueImage.SetPixel(x, y, bluePixel);
        }
    }

    // Отображение черно-белых изображений
    //ShowImage(redImage, 0);
    //ShowImage(greenImage, 0);
    ShowImage(blueImage, 0);
    Console.ReadKey();
}

static void ShowImage(Image _image, int step)
{
    Point location = new Point(1, 1);
    Size imageSize = new Size(40, 20); // desired image size in characters
    // draw some placeholders
    Console.SetCursorPosition(location.X - 1, location.Y);
    Console.Write(">");
    Console.SetCursorPosition(location.X + imageSize.Width, location.Y);
    Console.Write("<");
    Console.SetCursorPosition(location.X - 1, location.Y + imageSize.Height - 1);
    Console.Write(">");
    Console.SetCursorPosition(location.X + imageSize.Width, location.Y + imageSize.Height - 1);
    Console.WriteLine("<");

    using (Graphics g = Graphics.FromHwnd(GetConsoleWindow()))
    {
        using (var image = _image)
        {
            Size fontSize = GetConsoleFontSize();

            // translating the character positions to pixels
            Rectangle imageRect = new Rectangle(
                location.X * fontSize.Width,
                location.Y * fontSize.Height,
                imageSize.Width * fontSize.Width,
                imageSize.Height * fontSize.Height);
            g.DrawImage(image, imageRect);
        }
    }
}

static Bitmap HideImageInLSB(string sourceImagePath, string secretImagePath, string outputImagePath, bool show, bool type)
{
    // Загрузка изображений
    Bitmap sourceImage = new Bitmap(sourceImagePath);
    Bitmap secretImage = new Bitmap(secretImagePath);

    // Проверка, что изображения имеют одинаковый размер
    if (sourceImage.Width != secretImage.Width || sourceImage.Height != secretImage.Height)
    {
        Console.WriteLine("Изображения имеют разные размеры. Невозможно выполнить скрытие.");
        return null;
    }

    // Создание результирующего изображения
    Bitmap outputImage = new Bitmap(sourceImage);

    if (type)
    {
        for (int y = 0; y < sourceImage.Height; y++)
        {
            for (int x = 0; x < sourceImage.Width; x++)
            {
                // Получение пикселей исходного и секретного изображений
                Color sourcePixel = sourceImage.GetPixel(x, y);
                Color secretPixel = secretImage.GetPixel(x, y);

                // Изменение младших битов пикселя исходного изображения
                int r = (sourcePixel.R & 0xFE) | (secretPixel.R & 0x01);
                int g = (sourcePixel.G & 0xFE) | (secretPixel.G & 0x01);
                int b = (sourcePixel.B & 0xFE) | (secretPixel.B & 0x01);

                // Создание нового пикселя для результирующего изображения
                Color outputPixel = Color.FromArgb(r, g, b);

                // Установка нового пикселя в результирующее изображение
                outputImage.SetPixel(x, y, outputPixel);
            }
        }
    }
    else
    {
        // Перебор пикселей изображений
        for (int y = sourceImage.Height; y > 0 ; y--)
        {
            for (int x = sourceImage.Width; x > 0; x--)
            {
                // Получение пикселей исходного и секретного изображений
                Color sourcePixel = sourceImage.GetPixel(x, y);
                Color secretPixel = secretImage.GetPixel(x, y);

                // Изменение младших битов пикселя исходного изображения
                int r = (sourcePixel.R & 0xFE) | (secretPixel.R & 0x01);
                int g = (sourcePixel.G & 0xFE) | (secretPixel.G & 0x01);
                int b = (sourcePixel.B & 0xFE) | (secretPixel.B & 0x01);

                // Создание нового пикселя для результирующего изображения
                Color outputPixel = Color.FromArgb(r, g, b);

                // Установка нового пикселя в результирующее изображение
                outputImage.SetPixel(x, y, outputPixel);
            }
        }
    }
    var outpuImageReturn = new Bitmap(outputImage);
    if (show)
    {
        Point location = new Point(1, 1);
        Size imageSize = new Size(40, 20); // desired image size in characters
                                           // draw some placeholders
        Console.SetCursorPosition(location.X - 1, location.Y);
        Console.Write(">");
        Console.SetCursorPosition(location.X + imageSize.Width, location.Y);
        Console.Write("<");
        Console.SetCursorPosition(location.X - 1, location.Y + imageSize.Height - 1);
        Console.Write(">");
        Console.SetCursorPosition(location.X + imageSize.Width, location.Y + imageSize.Height - 1);
        Console.WriteLine("<");

        using (Graphics g = Graphics.FromHwnd(GetConsoleWindow()))
        {
            using (var image = outputImage)
            {
                Size fontSize = GetConsoleFontSize();

                // translating the character positions to pixels
                Rectangle imageRect = new Rectangle(
                    location.X * fontSize.Width,
                    location.Y * fontSize.Height,
                    imageSize.Width * fontSize.Width,
                    imageSize.Height * fontSize.Height);
                g.DrawImage(image, imageRect);
            }
        }
    }
    return outpuImageReturn;
}
static Size GetConsoleFontSize()
{
    // getting the console out buffer handle
    IntPtr outHandle = CreateFile("CONOUT$", GENERIC_READ | GENERIC_WRITE,
        FILE_SHARE_READ | FILE_SHARE_WRITE,
        IntPtr.Zero,
        OPEN_EXISTING,
        0,
        IntPtr.Zero);
    int errorCode = Marshal.GetLastWin32Error();
    if (outHandle.ToInt32() == INVALID_HANDLE_VALUE)
    {
        throw new IOException("Unable to open CONOUT$", errorCode);
    }

    ConsoleFontInfo cfi = new ConsoleFontInfo();
    if (!GetCurrentConsoleFont(outHandle, false, cfi))
    {
        throw new InvalidOperationException("Unable to get font information.");
    }

    return new Size(cfi.dwFontSize.X, cfi.dwFontSize.Y);
}

[DllImport("kernel32.dll", SetLastError = true)]
static extern IntPtr GetConsoleWindow();

[DllImport("kernel32.dll", SetLastError = true)]
static extern IntPtr CreateFile(
    string lpFileName,
    int dwDesiredAccess,
    int dwShareMode,
    IntPtr lpSecurityAttributes,
    int dwCreationDisposition,
    int dwFlagsAndAttributes,
    IntPtr hTemplateFile);

[DllImport("kernel32.dll", SetLastError = true)]
static extern bool GetCurrentConsoleFont(
    IntPtr hConsoleOutput,
    bool bMaximumWindow,
    [Out][MarshalAs(UnmanagedType.LPStruct)] ConsoleFontInfo lpConsoleCurrentFont);

[StructLayout(LayoutKind.Sequential)]
internal class ConsoleFontInfo
{
    internal int nFont;
    internal Coord dwFontSize;
}

[StructLayout(LayoutKind.Explicit)]
internal struct Coord
{
    [FieldOffset(0)]
    internal short X;
    [FieldOffset(2)]
    internal short Y;
}
