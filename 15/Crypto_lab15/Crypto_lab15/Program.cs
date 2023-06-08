using Aspose.Words;
using System.Drawing;
using System.Text;

SizeEncryption();
ColorEncryption();
SizeDecryption();
ColorDecryption();

static void SizeEncryption()
{
    var document = new Document("PPP.doc");
    double lines_count = document.Sections[0].Body.Paragraphs.Count;
    Console.WriteLine("Сообщение:");
    string data = Console.ReadLine();
    string bin = StringToBinary(data);
    if (bin.Length > Math.Round(lines_count))
        return;

    for (int i = 0; i < bin.Length; i++)
    {
        string additional = bin[i] == '0' ? "" : " ";
        document.Sections[0].Body.Paragraphs[i].Runs[0].Text += additional;
        if (i + 1 == bin.Length)
            document.Sections[0].Body.Paragraphs[i + 1].Runs[0].Text += "  ";
    }
    document.Save("size.doc");
}
static void ColorEncryption()
{
    var document = new Document("PPP.doc");
    double lines_count = document.Sections[0].Body.Paragraphs.Count;
    Console.WriteLine("Сообщение:");
    string data = Console.ReadLine();
    string bin = StringToBinary(data);
    if (bin.Length > Math.Round(lines_count))
        return;

    for (int i = 0; i < bin.Length; i++)
    {
        int additional = bin[i] == '0' ? 0 : 1;
        Color newC = Color.FromArgb(0, additional, 0);
        document.Sections[0].Body.Paragraphs[i].Runs[0].Font.Color = newC;

        if (i + 1 == bin.Length)
        {
            Color color = document.Sections[0].Body.Paragraphs[i + 1].Runs[0].Font.Color;
            Color new_color = Color.FromArgb(255, 1, 1);
            document.Sections[0].Body.Paragraphs[i + 1].Runs[0].Font.Color = new_color;
            //document.Sections[0].Body.Paragraphs[i + 1].Runs[0].Text += "QWERTY";
        }
    }
    document.Save("color.doc");
}

static void SizeDecryption()
{
    var document = new Document("size.doc");
    DocumentBuilder builder = new DocumentBuilder(document);

    int lines_count = document.Sections[0].Body.Paragraphs.Count;
    String arr = "";

    for (int i = 0; i < lines_count; i++)
    {
        if (document.Sections[0].Body.Paragraphs[i].Runs[0].Text.Contains("  "))
            break;

        if (document.Sections[0].Body.Paragraphs[i].Runs[0].Text.EndsWith(" "))
            arr += '1';
        else
            arr += '0';
    }
    Console.WriteLine("Message: " + BinaryToString(arr));
    //Console.WriteLine("Message: " + String.Join(".", arr));
}

static void ColorDecryption()
{
    var document = new Aspose.Words.Document("color.doc");
    DocumentBuilder builder = new DocumentBuilder(document);
    int lines_count = document.Sections[0].Body.Paragraphs.Count;
    string arr = "";
    for (int i = 0; i < lines_count; i++)
    {
        if (document.Sections[0].Body.Paragraphs[i].Runs[0].Font.Color.G == 1
            && document.Sections[0].Body.Paragraphs[i].Runs[0].Font.Color.B == 1)
            break;
        if (document.Sections[0].Body.Paragraphs[i].Runs[0].Font.Color.G == 1)
            arr += '1';
        else
            arr += '0';
    }
    Console.WriteLine("Сообщение: " + BinaryToString(arr));
}
static string StringToBinary(string data)
{
    string sb = "";
    foreach (char c in data.ToCharArray())
        sb += Convert.ToString(c, 2).PadLeft(8, '0');
    while (sb.Length % 8 != 0)
        sb = "0" + sb;
    return sb;
}
static string BinaryToString(string data)
{
    List<byte> byteList = new List<byte>();
    for (int i = 0; i + 8 - 1 <= data.Length; i += 8)
        byteList.Add(Convert.ToByte(data.Substring(i, 8), 2));
    return Encoding.ASCII.GetString(byteList.ToArray());
}
