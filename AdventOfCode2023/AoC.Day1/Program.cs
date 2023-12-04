
using System.Text.RegularExpressions;

Console.WriteLine("Select which task to run: 1 or 2");
var task = Console.ReadLine();

switch (task)
{
    case "1":
        Task1();
        break;
    case "2":
        Task2();
        break;
    default:
        Console.WriteLine("Invalid task");
        break;
}

Console.ReadLine();
return;

void Task1()
{
    var input = File.ReadAllLines("../../../inputs/task1.txt");
   
    var numbersOnly = input
        .Select(str => 
            Regex.Replace(str, @"\D", string.Empty))
        .ToList();

    var firstAndLast = numbersOnly
        .Select(GetFirstAndLastCharsAsTwoDigitInt);
    
    Console.WriteLine(firstAndLast.Sum());
}

void Task2()
{
    var input = File.ReadAllLines("../../../inputs/task1.txt");
    var list = new List<string>(input);
    var numbers = new Dictionary<string, int> {
        {"one" ,   1},
        {"two" ,  2},
        {"three" , 3},
        {"four" , 4},
        {"five" , 5},
        {"six" , 6},
        {"seven" , 7},
        {"eight" , 8},
        {"nine" , 9 }
    };
    
    var total = 0;
    foreach (var item in list)
    {
        var firstDigit = FindDigit(item, numbers);
        var lastDigit = FindDigit(
            new string(
                item.Reverse().ToArray()),
            numbers.ToDictionary(k => 
                new string(k.Key.Reverse().ToArray()), k => k.Value));
        total += int.Parse(firstDigit + lastDigit);
    }

    Console.WriteLine(total);
}

string FindDigit(string item, Dictionary<string, int> numbers)
{
    var index = 0;
    var digit = 0;
    foreach (var c in item)
    {
        var sub = item.AsSpan(index++);
        
        foreach(var n in numbers)
        {
            if (sub.StartsWith(n.Key))
            {
                digit = n.Value;
            }
        }

        if (c >= 48 && c <= 57)
        {
            digit = c - 48;
            break;
        }
    }
    
    return digit.ToString();
}

int GetFirstAndLastCharsAsTwoDigitInt(string input)
{
    var first = input[0];
    var last = input[^1];
    return int.Parse($"{first}{last}");
}