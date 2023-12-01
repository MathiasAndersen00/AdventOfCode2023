namespace AdventOfCode.Day1;

public class Trebuchet
{
    public int GetCombinedCalibrationValue()
    {
        var calibrationList = GetCalibrationDocument();
        var combinedCalibrationValue = 0;

        foreach (var line in calibrationList)
        {
            var convertedLine = ConvertSpelledOutDigits(line);
            var digits = convertedLine.Where(char.IsNumber).ToList();
            if (digits.Count > 0)
            {
                int firstDigit = digits[0] - '0';
                int lastDigit = digits[digits.Count - 1] - '0';
                combinedCalibrationValue += firstDigit * 10 + lastDigit;
            }
        }

        return combinedCalibrationValue;
    }

    private List<string> GetCalibrationDocument()
    {
        var fileName = "CalibrationDocument.txt";
        var path = Path.Combine(Environment.CurrentDirectory, @"Day1\", fileName);
        var calibrationDocument = File.ReadAllLines(path);
        var calibrationList = new List<string>(calibrationDocument);
        return calibrationList;
    }

    private string ConvertSpelledOutDigits(string input)
    {
        var wordToDigit = new Dictionary<string, string>
        {
            {"one", "1"},
            {"two", "2"},
            {"three", "3"},
            {"four", "4"},
            {"five", "5"},
            {"six", "6"},
            {"seven", "7"},
            {"eight", "8"},
            {"nine", "9"},
        };

        foreach (var word in wordToDigit)
        {
            input = input.Replace(word.Key, $"{word.Key.First()}{word.Value}{word.Key.Last()}");
        }

        return input;
    }
}