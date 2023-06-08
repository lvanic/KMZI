string pollishAlpabet = "aąbcćdeęfghijklłmnńoóprsśtuwyzźż,.„ ";
string bulgarianAlphabet = "абвгдежзийклмнопрстуфхцчшщъьюя,.„ ";
string englishAlphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
Console.WriteLine(englishAlphabet);
string binaryAlphabet = "01";

string meEnglish = "Ivanovski Jahor";
var shanonEnthropyEnglish = ShanonEntropy(meEnglish, englishAlphabet);
Console.WriteLine($"Shanon count of information lectos - {CountOfInformation(meEnglish, shanonEnthropyEnglish)}");


//string russianAlphabet = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя,.„ ";

//string textOnRussianLenguage = "войдя по ступенькам входа на курган.";

//Console.WriteLine($"Shanon entrophy russian - {ShanonEntropy(textOnRussianLenguage, russianAlphabet)}");

string textOnPollishLanguage = "klikając przycisk „AKCEPTUJĘ” lub zamykając to okno zgadzasz" +
    " się, aby serwis Ceneo.pl sp z.o.o. i jego Zaufani Partnerzy przetwarzali Twoje dane osobowe " +
    "zapisywane w plikach cookies lub za pomocą podobnej technologii w celach marketingowych (w tym poprzez " +
    "profilowanie i analizowanie) podmiotów innych niż Ceneo.pl, obejmujących w szczególności wyświetlanie " +
    "spersonalizowanych reklam w serwisie Ceneo.pl.\r\nWyrażenie zgody jest dobrowolne. Wycofanie zgody nie" +
    " zabrania serwisowi Ceneo.pl przetwarzania dotychczas zebranych danych.\r\n\r\nWyrażając zgodę, otrzymasz" +
    " reklamy produktów, które są dopasowane do Twoich potrzeb. Sprawdź Zaufanych Partnerów Ceneo.pl. Pamiętaj," +
    " że oni również mogą korzystać ze swoich zaufanych podwykonawców.\r\n\r\nInformujemy także, że korzystając " +
    "z serwisu Ceneo.pl, wyrażasz zgodę na przechowywanie w Twoim urządzeniu plików cookies lub stosowanie innych " +
    "podobnych technologii oraz na wykorzystywanie ich do dopasowywania treści marketingowych i reklam, o ile pozwala " +
    "na to konfiguracja Twojej przeglądarki. Jeżeli nie zmienisz ustawień Twojej przeglądarki, cookies będą zapisywane w" +
    " pamięci Twojego urządzenia. Więcej w Polityce Plików Cookies.".ToLower();

string textOnBulgarianLanguage = "Ние използваме „бисквитки“ и други подобни инструменти," +
    " за показване съдържанието на сайта и подобряване на потребителското изживяване." +
    " Повече информация за „бисквитките“ и опциите за управлението им може да намерите в раздела Политика за използване на бисквитки".ToLower();


Console.WriteLine("1 Task");
Console.WriteLine("-----------------------------------------------------------------------------------");
Console.WriteLine($"Length pollish text - {textOnPollishLanguage.Length}");
var shanonEntrophyPollish = ShanonEntropy(textOnPollishLanguage, pollishAlpabet);
Console.WriteLine($"Shanon entrophy pollish - {shanonEntrophyPollish}");
Console.WriteLine("-----------------------------------------------------------------------------------");
Console.WriteLine($"Length bulgarian text - {textOnBulgarianLanguage.Length}");
var shanonEntrophyBulgarian = ShanonEntropy(textOnBulgarianLanguage, bulgarianAlphabet);
Console.WriteLine($"Shanon entrophy bulgarian - {shanonEntrophyBulgarian}");

Console.WriteLine(Environment.NewLine);
Console.WriteLine("2 Task");
Console.WriteLine("-----------------------------------------------------------------------------------");
var binnaryPollish = ToASCII(textOnPollishLanguage, 2);
var shanonEntrophyBinaryPollish = ShanonEntropy(binnaryPollish, binaryAlphabet);
Console.WriteLine($"Length binnary pollish text - {binnaryPollish.Length}");
Console.WriteLine($"Shanon entrophy binnary pollish - {shanonEntrophyBinaryPollish}");
Console.WriteLine("-----------------------------------------------------------------------------------");
var binnaryBulgarian = ToASCII(textOnBulgarianLanguage, 2);
var shanonEntrophyBinaryBulgarian = ShanonEntropy(binnaryBulgarian, binaryAlphabet);
Console.WriteLine($"Length binnary bulgarian text - {binnaryBulgarian.Length}");
Console.WriteLine($"Shanon entrophy binnary bulgarian - {shanonEntrophyBinaryBulgarian}");

Console.WriteLine(Environment.NewLine);
Console.WriteLine("3 Task");
Console.WriteLine("-----------------------------------------------------------------------------------");
const string mePollish = "iwanowski jahor genadevich";
Console.WriteLine($"Shanon count of information pollish(0) - {CountOfInformation(mePollish, shanonEntrophyPollish)}");
Console.WriteLine($"Shanon ASCII count of information pollish(0) - {CountOfInformation(ToASCII(mePollish, 2), shanonEntrophyBinaryPollish)}");
Console.WriteLine("-----------------------------------------------------------------------------------");
const string meBulgarian = "ивановски яхор генадевич";
Console.WriteLine($"Shanon count of information bulgarian(0) - {CountOfInformation(meBulgarian, shanonEntrophyBulgarian)}");
Console.WriteLine($"Shanon ASCII count of information bulgarian(0) - {CountOfInformation(ToASCII(meBulgarian, 2), shanonEntrophyBinaryBulgarian)}");

Console.WriteLine(Environment.NewLine);
Console.WriteLine("4 Task");

Console.WriteLine("Pollish");
Console.WriteLine("-----------------------------------------------------------------------------------");
Console.WriteLine($"Shanon count of information pollish(0.1) - 0");
Console.WriteLine($"Shanon ASCII count of information pollish(0.1) - {CountOfInformationWithProbability(ToASCII(mePollish, 2), 0.1)}");

Console.WriteLine("-----------------------------------------------------------------------------------");
Console.WriteLine($"Shanon count of information pollish(0.5) - 0");
Console.WriteLine($"Shanon ASCII count of information pollish(0.5) - {CountOfInformationWithProbability(ToASCII(mePollish, 2), 0.5)}");

Console.WriteLine("-----------------------------------------------------------------------------------");
Console.WriteLine($"Shanon count of information pollish(1) - 0");
Console.WriteLine($"Shanon ASCII count of information pollish(1) - {CountOfInformationWithProbability(ToASCII(mePollish, 2), 0.999999999999999)}");

Console.WriteLine(Environment.NewLine);
Console.WriteLine("Bulgarian");

Console.WriteLine("-----------------------------------------------------------------------------------");
Console.WriteLine($"Shanon count of information bulgarian(0.1) - 0");
Console.WriteLine($"Shanon ASCII count of information bulgarian(0.1) - {CountOfInformationWithProbability(ToASCII(meBulgarian, 2), 0.1)}");

Console.WriteLine("-----------------------------------------------------------------------------------");
Console.WriteLine($"Shanon count of information bulgarian(0.5) - 0");
Console.WriteLine($"Shanon ASCII count of information bulgarian(0.5) - {CountOfInformationWithProbability(ToASCII(meBulgarian, 2), 0.5)}");

Console.WriteLine("-----------------------------------------------------------------------------------");
Console.WriteLine($"Shanon count of information bulgarian(1) - 0");
Console.WriteLine($"Shanon ASCII count of information bulgarian(1) - {CountOfInformationWithProbability(ToASCII(meBulgarian, 2), 0.999999999999999)}");
Console.WriteLine("-----------------------------------------------------------------------------------");


//var letterCountPollish = GetLettersCount(textOnPollishLanguage, pollishAlpabet);
//var letterCountBulgarian = GetLettersCount(textOnBulgarianLanguage, bulgarianAlphabet);

//foreach (var letter in pollishAlpabet)
//{
//    Console.WriteLine($"{letterCountPollish[pollishAlpabet.IndexOf(letter)]}");
//}
//Console.WriteLine("-----------------------------------------------------------------------------------");
//foreach (var letter in bulgarianAlphabet)
//{
//    Console.WriteLine($"{letterCountBulgarian[bulgarianAlphabet.IndexOf(letter)]}");
//}

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

double CountOfInformation(string text, double shanonEntrophy)
{
    return text.Length * shanonEntrophy;
}
double CountOfInformationWithError(string text, double error)
{
    var q = 1 - error;
    var h = (-error * Math.Log2(error) - q * Math.Log2(q));
    return text.Length * (1 - h);
}
double CountOfInformationWithProbability(string text, double probability)
{
    var q = 1 - probability;
    var h = (-probability * Math.Log2(probability) - q * Math.Log2(q));
    return text.Length * (1 - h);
}
double ShanonEntropy(string text, string alphabet)
{
    double entropy = 0;
    int textLength = text.Length;
    int[] lettersCount = new int[alphabet.Length];

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

    for (int i = 0; i < lettersCount.Length; i++)
    {
        if (lettersCount[i] != 0)
        {
            double probability = (double)lettersCount[i] / textLength;
            entropy += (probability * Math.Log(probability, 2));
        }
    }

    return -entropy;
}

string ToASCII(string text, int mode)
{
    string pollishText = "";
    for (int i = 0; i < text.Length; i++)
    {
        pollishText += Convert.ToString(text[i], mode);
    }

    return pollishText;
}