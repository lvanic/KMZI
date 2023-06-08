string text = "Jakie ciasto?  wrzasnął skąpiec.  Kiedy to się stało i jak?Dopiero teraz dowiedział się, jak to było z ciastem dwa dni temu. Rozzłościł się jeszcze więcej i szukając kociołka pobiegł pod stodołę. Gdzie jest kociołek? Gdzie uciekł ten nicpoń?  wołał rozwścieczony.Robotnicy myśleli, że gospodarz dowiedział się od kogoś o pszenicy. Zaczęli prosić: Nie gniewajcie się, panie! Przecieżeśmy nienaumyślnie pozwolili mu wziąć waszą pszenicę. Jaką znów pszenicę?  wrzasnął gospodarz.  To on pszenicę zabrał?Dopiero teraz dowiedział się, co się stało na gumnie poprzedniego dnia. Wtem zobaczył kociołek, który wyskoczył zza studni na gospodarskie obejście, zaczął tańczyć i podrygiwać na trzech nóżkach. Twarz gospodarza pośmiała z gniewu. Biegał za kociołkiem, wymachiwał rękami i krzyczał: To on! To on zabrał mi ciasto i pszenicę, i pieniądze! Łapać go! Dajcie mi siekierę, to go roztrzaskam!Wtem kociołek przyskoczył do gospodarza, capnął go za łokieć pazurkami i nuż ciągnąć ku wrotom. Puszczaj! Puszczaj!  krzyczał bogacz przerażony.Ale łapki kociołka wczepiły się w jego rękaw żelaznymi pazurkami i ciągnęły go, ciągnęły, aż wyciągnęły na gościniec. Powlokły go przez wieś.Staruszkowie stali oboje na progu swojej chatki odświętnie ubrani, bo wybierali się właśnie do swego dawnego chlebodawcy z podziękowaniem za to, co im przyniósł kociołek. Wtem zobaczyli, co się dzieje i zawołali zdumieni i przestraszeni: Kociołku, co robisz? Dokąd ciągniesz naszego gospodarza? Skoczę z nim, skoczę, skoczę aż na koniec świata!  zakrzyknął kociołek.Kociołek z gospodarzem znikli w dali i nikt nie zobaczył ich już nigdy. Staruszkowie mieli teraz wszystko, czego im było potrzeba. Obdzielili innych biedaków we wsi i żyli szczęśliwie.".ToLower();
string PollishAlphabet = "aąbcćdeęfghijklłmnńoóprsśtuwyzźż.,?!„:;\" ";
Console.WriteLine(text.Length);
Console.WriteLine("-----------------------------------------------------AffineCaesar-----------------------------------------------------");
Console.WriteLine("Подстановка 5, 7");

var AffineCaesarStart = DateTime.Now;
string EncryptText = AffineCaesar(text.ToLower(), 5, 7);
var AffineCaesarEnd = DateTime.Now;

Console.WriteLine(EncryptText);

Console.WriteLine("----------------------------------------------------------------------------------------------------------------------");
Console.WriteLine("Время шифрования: " + (AffineCaesarEnd - AffineCaesarStart).TotalMilliseconds + " мс");
Console.WriteLine("----------------------------------------------------------------------------------------------------------------------");

Console.WriteLine("Подстановка в обратную сторону 5, 7");
var ReverseAffineCaesarStart = DateTime.Now;
string DecryptText = ReverseAffineCaesar(EncryptText.ToLower(), 5, 7);
var ReverseAffineCaesarEnd = DateTime.Now;
Console.WriteLine(DecryptText);

Console.WriteLine("----------------------------------------------------------------------------------------------------------------------");
Console.WriteLine("Время дешифрования: " + (ReverseAffineCaesarEnd - ReverseAffineCaesarStart).TotalMilliseconds + " мс");

Console.WriteLine("------------------------------------------------------PortaCipher------------------------------------------------------");
Console.WriteLine("Шифрование Порты");
var PortaCipherStart = DateTime.Now;
string portaCipher = PortaCipher(text);
var PortaCipherEnd = DateTime.Now;
Console.WriteLine(portaCipher);

Console.WriteLine("----------------------------------------------------------------------------------------------------------------------");
Console.WriteLine("Время шифрования: " + (PortaCipherEnd - PortaCipherStart).TotalMilliseconds + " мс");
Console.WriteLine("----------------------------------------------------------------------------------------------------------------------");

Console.WriteLine("Дешифрование Порты");
var ReversePortaCipherStart = DateTime.Now;
string reversePortaCipher = ReversePortaCipher(portaCipher);
var ReversePortaCipherEnd = DateTime.Now;
Console.WriteLine(reversePortaCipher);

Console.WriteLine("----------------------------------------------------------------------------------------------------------------------");
Console.WriteLine("Время дешифрования: " + (ReversePortaCipherEnd - ReversePortaCipherStart).TotalMilliseconds + " мс");
Console.WriteLine("----------------------------------------------------------------------------------------------------------------------");

string colok = "qcoql";
Console.WriteLine("-------------------------------------------------------qcoql-----------------------------------------------------------");
Console.WriteLine(FindInv(26, 5));
Console.WriteLine("----------------------------------------------------------------------------------------------------------------------");

int FindInv(int n, int a) 
{
    int inv = 0;
    for (int i = 0; i < PollishAlphabet.Length; i++)
    {
        if ((i * a) % PollishAlphabet.Length == 1)
        {
            inv = i;
            break;
        }
    }
    return inv;
}

//Substitution cipher based on affine substitution system Caesar on pollish alphabet
string AffineCaesar(string text, int a, int b)
{
    string result = "";
    for (int i = 0; i < text.Length; i++)
    {
        int index = Array.IndexOf(PollishAlphabet.ToArray(), text[i]);
        result += PollishAlphabet[(a * index + b) % PollishAlphabet.Length];
    }
    return result;
}
//decrypt Substitution cipher based on affine substitution system Caesar on pollish alphabet
string ReverseAffineCaesar(string text, int a, int b)
{
    int inv = 0;
    for(int i = 0; i < PollishAlphabet.Length; i++)
    {
        if((i * a) % PollishAlphabet.Length == 1)
        {
            inv = i;
            break;
        }
    }

    string result = "";
    for (int i = 0; i < text.Length; i++)
    {
        int index = Array.IndexOf(PollishAlphabet.ToArray(), text[i]);
        result += PollishAlphabet[(inv * (index + PollishAlphabet.Length - b)) % PollishAlphabet.Length];
    }
    return result;
}


//Porta Cipher
string PortaCipher(string text)
{
    var matrix = new int[PollishAlphabet.Length, PollishAlphabet.Length];
    int incr = 0;
    for(int i = 0; i < PollishAlphabet.Length; i++)
    {
        for(int j = 0; j < PollishAlphabet.Length; j++)
        {
            matrix[i, j] = incr;
            incr++;
        }
    }   

    string result = "";

    for (int i = 0; i < text.Length; i += 2)
    {
        int index = Array.IndexOf(PollishAlphabet.ToArray(), text[i]);
        int indexNext;
        try
        {
            indexNext = Array.IndexOf(PollishAlphabet.ToArray(), text[i + 1]);
        }catch (Exception e)
        {
            indexNext = 1;
        }

        result += matrix[index, indexNext];
        result += " ";
    }
    
    return result.Trim();
}

//reverse Porta Cipher
string ReversePortaCipher(string text)
{
    var matrix = new int[PollishAlphabet.Length, PollishAlphabet.Length];
    int incr = 0;
    for (int i = 0; i < PollishAlphabet.Length; i++)
    {
        for (int j = 0; j < PollishAlphabet.Length; j++)
        {
            matrix[i, j] = incr;
            incr++;
        }
    }

    string result = "";
    var nubmers = text.Split(' ');
    //find index of number in matrix
    for (int i = 0; i < nubmers.Length; i++)
    {
        int number = Convert.ToInt32(nubmers[i]);
        for (int j = 0; j < PollishAlphabet.Length; j++)
        {
            for (int k = 0; k < PollishAlphabet.Length; k++)
            {
                if (matrix[j, k] == number)
                {
                    result += PollishAlphabet[j];
                    result += PollishAlphabet[k];
                }
            }
        }
    }
    return result;
}


foreach(var item in GetLettersCount(text, PollishAlphabet))
{
    Console.WriteLine(item);
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