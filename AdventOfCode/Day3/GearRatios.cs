namespace AdventOfCode.Day3;

public class GearRatios
{
    public int AddAllPartNumbers()
    {
        var total = 0;
        var grid = GetEngineSchematicAsGrid();
        List<List<string>> sequences = new List<List<string>>();

        for (int i = 0; i < grid.Count; i++)
        {
            for (int j = 0; j < grid[i].Count; j++)
            {
                bool hasLeftNumber = false;

                if (j != 0)
                {
                    hasLeftNumber = IsNumber(grid[i][j - 1]);
                }

                if (IsNumber(grid[i][j]) && !hasLeftNumber)
                {
                    var sequence = new List<string>();
                    sequence = GetSequence(grid, i, j, sequence);

                    if (IsAdjacentToSymbol(grid, sequence, i, j))
                    {
                        sequences.Add(sequence);
                    }
                }
            }
        }

        foreach (var sequence in sequences)
        {
            var number = int.Parse(string.Join("", sequence));
            total += number;
        }

        return total;
    }

    public int AddPartNumbersAdjacentToStar()
    {
        //var total = 0;
        //var grid = GetEngineSchematicAsGrid();
        //List<List<string>> sequences = new List<List<string>>();

        //for (int i = 0; i < grid.Count; i++)
        //{
        //    for (int j = 0; j < grid[i].Count; j++)
        //    {
        //        bool hasLeftNumber = false;

        //        if (j != 0)
        //        {
        //            hasLeftNumber = IsNumber(grid[i][j - 1]);
        //        }

        //        if (IsNumber(grid[i][j]) && !hasLeftNumber)
        //        {
        //            var sequence = new List<string>();
        //            sequence = GetSequence(grid, i, j, sequence);

        //            if (IsAdjacentToSymbol(grid, sequence, i, j))
        //            {
        //                sequences.Add(sequence);
        //            }
        //        }
        //    }
        //}

        //foreach (var sequence in sequences)
        //{
        //    var number = int.Parse(string.Join("", sequence));
        //    total += number;
        //}

        return 0;
    }

    private List<string> GetSequence(List<List<string>> grid, int i, int j, List<string> sequence)
    {
        if (i < 0 || i >= grid.Count || j < 0 || j >= grid[i].Count || !IsNumber(grid[i][j]))
        {
            return sequence;
        }

        sequence.Add(grid[i][j]);

        GetSequence(grid, i, j + 1, sequence);

        return sequence;
    }

    private bool IsAdjacentToSymbol(List<List<string>> grid, List<string> sequence, int i, int j)
    {
        for (var l = 0; l <= sequence.Count-1; l++)
        {
            // Check right
            if (j < grid[i].Count - 1 && !IsNumber(grid[i][j + 1]) && grid[i][j + 1] != ".")
            {
                return true;
            }

            // Check left
            if (j > 0 && !IsNumber(grid[i][j - 1]) && grid[i][j - 1] != ".")
            {
                return true;
            }

            // Check above
            if (i > 0 && !IsNumber(grid[i - 1][j]) && grid[i - 1][j] != ".")
            {
                return true;
            }

            // Check below
            if (i < grid.Count - 1 && !IsNumber(grid[i + 1][j]) && grid[i + 1][j] != ".")
            {
                return true;
            }

            // Check diagonals
            if (i > 0 && j > 0 && !IsNumber(grid[i - 1][j - 1]) && grid[i - 1][j - 1] != ".")
            {
                return true;
            }

            if (i < grid.Count - 1 && j < grid[i].Count - 1 && !IsNumber(grid[i + 1][j + 1]) && grid[i + 1][j + 1] != ".")
            {
                return true;
            }

            if (i > 0 && j < grid[i].Count - 1 && !IsNumber(grid[i - 1][j + 1]) && grid[i - 1][j + 1] != ".")
            {
                return true;
            }

            if (i < grid.Count - 1 && j > 0 && !IsNumber(grid[i + 1][j - 1]) && grid[i + 1][j - 1] != ".")
            {
                return true;
            }

            j++;
        }
        return false;
    }

    private List<List<string>> GetEngineSchematicAsGrid()
    {
        var fileName = "EngineSchematic.txt";
        var path = Path.Combine(Environment.CurrentDirectory, @"Day3\", fileName);

        StreamReader reader = new StreamReader(path);
        var grid = new List<List<string>>();

        while (!reader.EndOfStream)
        {
            string line = reader.ReadLine();
            var row = new List<string>();
            foreach (char c in line)
            {
                row.Add(c.ToString());
            }
            grid.Add(row);
        }
        reader.Close();

        return grid;
    }

    private bool IsNumber(string s)
    {
        return int.TryParse(s, out _);
    }
}
