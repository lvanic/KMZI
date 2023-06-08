using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto_lab14
{
    public class LeastSignificantBrightness
    {
        public static void EmbedData(Bitmap sourceImage, byte[] data)
        {
            // Проверка, достаточно ли пикселей для осаждения данных
            int totalPixels = sourceImage.Width * sourceImage.Height;
            int requiredPixels = data.Length * 4; // Каждый байт данных осаждается в 4 пикселях (ARGB)
            if (requiredPixels > totalPixels)
            {
                Console.WriteLine("Недостаточно пикселей для осаждения данных.");
                return;
            }

            // Конвертация данных в биты
            BitArray bits = new BitArray(data);

            // Перебор пикселей изображения
            int bitIndex = 0;
            for (int y = 0; y < sourceImage.Height; y++)
            {
                for (int x = 0; x < sourceImage.Width; x++)
                {
                    Color pixel = sourceImage.GetPixel(x, y);

                    // Осаждение битов в младшие разряды компоненты яркости
                    for (int i = 0; i < 2; i++) // Осаждаем 2 бита в каждом пикселе
                    {
                        if (bitIndex >= bits.Length)
                            break;

                        int brightness = (int)(pixel.GetBrightness() * 255);
                        int modifiedBrightness = (brightness & 0xFC) | (bits[bitIndex] ? 0x01 : 0x00);

                        pixel = Color.FromArgb(pixel.A, modifiedBrightness, modifiedBrightness, modifiedBrightness);
                        sourceImage.SetPixel(x, y, pixel);
                        bitIndex++;
                    }

                    if (bitIndex >= bits.Length)
                        break;
                }

                if (bitIndex >= bits.Length)
                    break;
            }

            // Сохранение измененного изображения
            sourceImage.Save("output.bmp", ImageFormat.Bmp);
            Console.WriteLine("Изображение с осажденными данными сохранено.");
        }

        public static byte[] ExtractData(Bitmap image, int dataSize)
        {
            // Извлечение данных из младших разрядов яркости
            BitArray bits = new BitArray(dataSize);
            int bitIndex = 0;

            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    Color pixel = image.GetPixel(x, y);

                    for (int i = 0; i < 2; i++) // Извлекаем 2 бита из каждого пикселя
                    {
                        int brightness = (int)(pixel.GetBrightness() * 255);
                        bool bit = (brightness & 0x01) == 0x01;
                        bits[bitIndex] = bit;

                        bitIndex++;
                        if (bitIndex >= dataSize)
                            break;
                    }

                    if (bitIndex >= dataSize)
                        break;
                }

                if (bitIndex >= dataSize)
                    break;
            }

            // Конвертация битов в массив байт
            byte[] data = new byte[dataSize / 8];
            bits.CopyTo(data, 0);

            return data;
        }
    }
}
