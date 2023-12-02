using System.Text.RegularExpressions;

namespace AdventOfCode.Day2;

public class CubeConundrum
{
    public int GetPossibleGames()
    {
        var totalOfPossibleGameIds = 0;
        var gameRecord = GetGameRecord();
        var gamesToBeRemoved = new List<string>();

        foreach (var game in gameRecord)
        {
            string[] gameSets = game.Split(';');
            string gameNumber = "";
            bool skipSets = false;

            foreach (var set in gameSets)
            {
                if (!skipSets)
                {
                    string colorSequence;

                    if (set == gameSets[0])
                    {
                        string[] parts = set.Split(":");
                        gameNumber = parts[0];
                        colorSequence = parts[1];
                    }
                    else
                    {
                        colorSequence = set;
                    }

                    string[] cubeParts = colorSequence.Split(",");

                    if (!IsGamePossibleMatch(cubeParts))
                    {
                        gamesToBeRemoved.Add(game);
                        skipSets = true;
                    }
                }
            }
        }
        foreach (var game in gamesToBeRemoved)
        {
            gameRecord.Remove(game);
        }

        foreach (var game in gameRecord)
        {
            var parts = game.Split(':');
            var gameNumberWithDescription = parts[0];
            var gameNumberChar = gameNumberWithDescription.Where(char.IsNumber);
            var gameNumberString = string.Join("", gameNumberChar);
            var gameNumber = int.Parse(gameNumberString);
            totalOfPossibleGameIds += gameNumber;
        }

        return totalOfPossibleGameIds;
    }

    public int GetSumOfCubePower()
    {
        var gameRecord = GetGameRecord();
        var sumOfMultipliedHighestNumbers = 0;

        foreach (var game in gameRecord)
        {
            string[] gameSets = game.Split(';');
            string gameNumber = "";

            var redCubes = new List<int>();
            var blueCubes = new List<int>();
            var greenCubes = new List<int>();

            foreach (var set in gameSets)
            {
                string colorSequence;

                if (set == gameSets[0])
                {
                    string[] parts = set.Split(":");
                    gameNumber = parts[0];
                    colorSequence = parts[1];
                }
                else
                {
                    colorSequence = set;
                }

                string[] cubeParts = colorSequence.Split(",");

                foreach (string cube in cubeParts)
                {
                    MatchCollection greenMatch = Regex.Matches(cube, @"(\d+)\s+green");
                    MatchCollection blueMatch = Regex.Matches(cube, @"(\d+)\s+blue");
                    MatchCollection redMatch = Regex.Matches(cube, @"(\d+)\s+red");

                    foreach (Match match in redMatch)
                    {
                        redCubes.Add(int.Parse(match.Groups[1].Value));
                    }
                    foreach (Match match in greenMatch)
                    {
                        greenCubes.Add(int.Parse(match.Groups[1].Value));
                    }
                    foreach (Match match in blueMatch)
                    {
                        blueCubes.Add(int.Parse(match.Groups[1].Value));

                    }
                }
            }
            var highestRedNumber = redCubes.Max();
            var highestGreenNumber = greenCubes.Max();
            var highestBlueNumber = blueCubes.Max();

            var highestNumbersMultiplied = highestRedNumber * highestGreenNumber * highestBlueNumber;
            sumOfMultipliedHighestNumbers += highestNumbersMultiplied;
        }
        return sumOfMultipliedHighestNumbers;
    }

    private bool IsGamePossibleMatch(string[] cubeParts)
    {
        foreach (string cube in cubeParts)
        {
            MatchCollection greenMatch = Regex.Matches(cube, @"(\d+)\s+green");
            MatchCollection blueMatch = Regex.Matches(cube, @"(\d+)\s+blue");
            MatchCollection redMatch = Regex.Matches(cube, @"(\d+)\s+red");

            foreach (Match match in redMatch)
            {
                var cubeNumber = int.Parse(match.Groups[1].Value);
                if (cubeNumber > 12)
                {
                    return false;
                }
            }
            foreach (Match match in greenMatch)
            {
                var cubeNumber = int.Parse(match.Groups[1].Value);
                if (cubeNumber > 13)
                {
                    return false;
                }
            }
            foreach (Match match in blueMatch)
            {
                var cubeNumber = int.Parse(match.Groups[1].Value);
                if (cubeNumber > 14)
                {
                    return false;
                }
            }
        }
        return true;
    }
    private List<string> GetGameRecord()
    {
        var fileName = "GameRecord.txt";
        var path = Path.Combine(Environment.CurrentDirectory, @"Day2\", fileName);
        var gameRecordFile = File.ReadAllLines(path);
        var gameRecordList = new List<string>(gameRecordFile);
        return gameRecordList;
    }
}
