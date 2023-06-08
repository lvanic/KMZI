string text = "Rozzłościł się jeszcze więcej i szukając kociołka pobiegł pod stodołę. Gdzie jest kociołek? Gdzie uciekł ten nicpoń?  wołał rozwścieczony.Robotnicy myśleli, że gospodarz dowiedział się od kogoś o pszenicy. Zaczęli prosić: Nie gniewajcie się, panie! Przecieżeśmy nienaumyślnie pozwolili mu wziąć waszą pszenicę. Jaką znów pszenicę?  wrzasnął gospodarz.  To on pszenicę zabrał?Dopiero teraz dowiedział się, co się stało na gumnie poprzedniego dnia. Wtem zobaczył kociołek, który wyskoczył zza studni na gospodarskie obejście, zaczął tańczyć i podrygiwać na trzech nóżkach. Twarz gospodarza pośmiała z gniewu. Biegał za kociołkiem, wymachiwał rękami i krzyczał: To on! To on zabrał mi ciasto i pszenicę, i pieniądze! Łapać go! Dajcie mi siekierę, to go roztrzaskam!Wtem kociołek przyskoczył do gospodarza, capnął go za łokieć pazurkami i nuż ciągnąć ku wrotom. Puszczaj! Puszczaj!  krzyczał bogacz przerażony.Ale łapki kociołka wczepiły się w jego rękaw żelaznymi pazurkami i ciągnęły go, ciągnęły, aż wyciągnęły na gościniec. Powlokły go przez wieś.Staruszkowie stali oboje na progu swojej chatki odświętnie ubrani, bo wybierali się właśnie do swego dawnego chlebodawcy z podziękowaniem za to, co im przyniósł kociołek. Wtem zobaczyli, co się dzieje i zawołali zdumieni i przestraszeni: Kociołku, co robisz? Dokąd ciągniesz naszego gospodarza? Skoczę z nim, skoczę, skoczę aż na koniec świata!  zakrzyknął kociołek.Kociołek z gospodarzem znikli w dali i nikt nie zobaczył ich już nigdy. Staruszkowie mieli teraz wszystko, czego im było potrzeba. Obdzielili innych biedaków we wsi i żyli szczęśliwie.";
string PollishAlphabet = "aąbcćdeęfghijklłmnńoóprsśtuwyzźż.,?!„:;\" ";
Console.WriteLine(text.Length);

Console.WriteLine("---------------------------------------------------PermutationCipher---------------------------------------------------");
var timeStart = DateTime.Now;
var PerCol = PermutationCipherWriteByColumnsReadByRows(text);
var timeEnd = DateTime.Now;
Console.WriteLine("Время шифрования - " + (timeEnd.Millisecond - timeStart.Millisecond) + " мс");
Console.WriteLine(PerCol);
Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------");
timeStart = DateTime.Now;
Console.WriteLine(PermutationCipherWriteByColumnsReadByRowsDecrypt(PerCol));
timeEnd = DateTime.Now;
Console.WriteLine("Время расшифрования - " + (timeEnd.Millisecond - timeStart.Millisecond) + " мс");
Console.WriteLine("-----------------------------------------------MultiplePermutationCipher-----------------------------------------------");
timeStart = DateTime.Now;
var mult = crypt(text, new int[] { 5, 1, 2, 3, 4 }, new int[] { 3, 9, 1, 5, 6, 8, 7, 4, 2 });
timeEnd = DateTime.Now;
Console.WriteLine("Время шифрования - " + (timeEnd.Millisecond - timeStart.Millisecond) + " мс");
Console.WriteLine(mult);
Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------");
timeStart = DateTime.Now;
Console.WriteLine(decrypt(mult, new int[] { 5, 1, 2, 3, 4 }, new int[] { 3, 9, 1, 5, 6, 8, 7, 4, 2 }));
timeEnd = DateTime.Now;
Console.WriteLine("Время расшифрования - " + (timeEnd.Millisecond - timeStart.Millisecond) + " мс");
Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------");

var letterCountText = GetLettersCount(text, PollishAlphabet);

for(int i = 0; i< letterCountText.Length; i++)
{
    Console.WriteLine(letterCountText[i]);
}

Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------");
var letterCountCrypto = GetLettersCount(PerCol, PollishAlphabet);
for (int i = 0; i < letterCountCrypto.Length; i++)
{
    Console.WriteLine(letterCountCrypto[i] + " " + PollishAlphabet[i]);
}
//Route permutation writing - by columns reading - by rows
static string PermutationCipherWriteByColumnsReadByRows(string text)
{
    string result = "";
    int rows = text.Length / 5;
    int columns = 5;
    char[,] matrix = new char[rows, columns];
    int index = 0;
    for (int i = 0; i < columns; i++)
    {
        for (int j = 0; j < rows; j++)
        {
            matrix[j, i] = text[index];
            index++;
        }
    }
    foreach (char c in matrix)
    {
        result += c;
    }
    return result;
}

// Decrypt route permutation writing - by columns reading - by rows
static string PermutationCipherWriteByColumnsReadByRowsDecrypt(string text)
{
    string result = "";
    int rows = text.Length / 5;
    int columns = 5;
    char[,] matrix = new char[rows, columns];
    int index = 0;

    for (int i = 0; i < rows; i++)
    {
        for (int j = 0; j < columns; j++)
        {
            matrix[i, j] = text[index];
            index++;
        }
    }
    for (int i = 0; i < columns; i++)
    {
        for (int j = 0; j < rows; j++)
        {
            result += matrix[j, i];
        }
    }
    return result;
}

// decrypt Multiple permutation, key words - Yahor Ivanouski
string crypt(string message, int[] colsKey, int[] rowsKey)
{
    var colsCount = colsKey.Length;
    var rowsCount = rowsKey.Length;
    var result = new List<char>();
    for(var row = 0; row < rowsCount; row++)
    {
        for (var col = 0; col < colsCount; col++)
        {
            result.Add(' ');
        }
    }

    for (var row = 0; row < rowsCount; row++)
    {
        for (var col = 0; col < colsCount; col++)
        {
            var newCol = colsKey[col] - 1;
            var newRow = rowsKey[row] - 1;
            var value = message[row * colsCount + col];
            result[newRow * colsCount + newCol] = value;
            //Console.WriteLine(value);
        }
    }
    string resultick = null;
    foreach (var chark in result) {
        resultick += chark;
    }
    return resultick;
}

string decrypt(string message, int[] colsKey, int[] rowsKey)
{
    var colsCount = colsKey.Length;
    var rowsCount = rowsKey.Length;

    var result = new List<char>();
    for (var row = 0; row < rowsCount; row++)
    {
        for (var col = 0; col < colsCount; col++)
        {
            result.Add(' ');
        }
    }
    for (var row = 0; row < rowsCount; row++)
    {
        for (var col = 0; col < colsCount; col++)
        {
            var newCol = colsKey[col] - 1;
            var newRow = rowsKey[row] - 1;
            result[row * colsCount + col] = message[newRow * colsCount + newCol];
        }
    }
    string resultick = null;
    foreach (var chark in result)
    {
        resultick += chark;
    }
    return resultick;
}
int[] GetLettersCount(string text, string alphabet)
{
    int[] lettersCount = new int[alphabet.Length];
    int textLength = text.Length;

    for (int i = 0; i < textLength; i++)
    {
        for (int j = 0; j < alphabet.Length; j++)
        {
            if (text[i] == alphabet[j])
            {
                lettersCount[j]++;
            }
        }
    }

    return lettersCount;
}