using System.Text;

var text = "A".ToUpper();
//A - Y

var reflectorPairs = new Dictionary<char, char>()
{
    { 'A' , 'Y'},
    { 'B' , 'R'},
    { 'C' , 'U'},
    { 'D' , 'H'},
    { 'E' , 'Q'},
    { 'F' , 'S'},
    { 'G' , 'L'},
    { 'I' , 'P'},
    { 'J' , 'X'},
    { 'K' , 'N'},
    { 'M' , 'O'},
    { 'T' , 'Z'},
    { 'V' , 'W'},
};

// Создаем экземпляры классов Rotor и Reflector

Rotor rotor1 = new Rotor("FSOKANUERHMBTIYCWLQPZXVGJD".ToCharArray());
Rotor rotor2 = new Rotor("LEYJVCNIXWPBQMDRTAKZGFUHOS".ToCharArray());
Rotor rotor3 = new Rotor("EKMFLGDQVZNTOWYHXUSPAIBRCJ".ToCharArray());
Reflector reflector = new Reflector(reflectorPairs);

// Создаем экземпляр класса EnigmaMachine и устанавливаем начальные позиции роторов
EnigmaMachine enigma = new EnigmaMachine(rotor1, rotor2, rotor3, reflector);
enigma.SetPositions("AAA"); // Устанавливаем начальные позиции роторов

var enigmaText = enigma.Encipher(text);

Console.WriteLine(enigmaText);
public class EnigmaMachine
{
    private readonly Rotor _rotor1;
    private readonly Rotor _rotor2;
    private readonly Rotor _rotor3;
    private readonly Reflector _reflector;

    public EnigmaMachine(Rotor rotor1, Rotor rotor2, Rotor rotor3, Reflector reflector)
    {
        _rotor1 = rotor1;
        _rotor2 = rotor2;
        _rotor3 = rotor3;
        _reflector = reflector;
    }

    public char Encipher(char input)
    {
        //if (_rotor1.GetPosition() == 'R' || _rotor2.GetPosition() == 'F')
        //{
        //    _rotor2.Rotate();
        //}
        //if (_rotor2.GetPosition() == 'W')
        //{
        //    _rotor3.Rotate();
        //}
        //_rotor1.Rotate();

        char output = _rotor1.Encipher(input);
        output = _rotor2.Encipher(output);
        output = _rotor3.Encipher(output);
        output = _reflector.Reflect(output);
        output = _rotor3.Encipher(output, true);
        output = _rotor2.Encipher(output, true);
        output = _rotor1.Encipher(output, true);

        // Возвращаем шифрованный символ
        return output;
    }

    public string Encipher(string input)
    {
        string output = "";
        foreach (char c in input)
        {
            if (char.IsLetter(c))
            {
                output += Encipher(char.ToUpper(c));
            }
            else
            {
                output += c;
            }
        }
        return output;
    }

    public void SetPositions(string positions)
    {
        if (positions.Length != 3)
        {
            throw new ArgumentException("Positions must be a string of length 3");
        }
        _rotor1.SetPosition(positions[0]);
        _rotor2.SetPosition(positions[1]);
        _rotor3.SetPosition(positions[2]);
    }
}


public class Rotor
{
    private readonly char[] _wiring;
    private readonly char[] _inverseWiring;
    private int _position;

    public Rotor(char[] wiring)
    {
        _wiring = wiring;
        _inverseWiring = new char[wiring.Length];
        for (int i = 0; i < wiring.Length; i++)
        {
            _inverseWiring[wiring[i] - 'A'] = (char)('A' + i);
        }
        _position = 0;
    }

    public char Encipher(char input, bool reverse = false)
    {
        int index = (input - 'A' + _position) % 26;
        char output = reverse ? _inverseWiring[index] : _wiring[index];
        return (char)('A' + (output - 'A' - _position + 26) % 26);
    }

    public void Rotate()
    {
        _position = (_position + 1) % 26;
    }

    public void SetPosition(char position)
    {
        _position = position - 'A';
    }

    public char GetPosition()
    {
        return (char)('A' + _position);
    }
}

public class Reflector
{
    private readonly Dictionary<char, char> _wiring;

    public Reflector(Dictionary<char, char> wiring)
    {
        _wiring = wiring;
    }

    public char Reflect(char input)
    {
        char value;
        if (_wiring.ContainsKey(input))
        {
            value = _wiring[input];
        }
        else
        {
            value = _wiring.First(x => x.Value == input).Key;
        }
        return value;
    }
}
