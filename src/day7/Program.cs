// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");

Day7Part1(args[0]);
// Day7Part2(args[0]);
return;

static void Day7Part1(string filename = "src/day7/input.txt")
{
    // load the data
    var lines = File
        .ReadLines(filename)
        .ToList();
    
    // Console.WriteLine(string.Join("\n", lines));

    foreach (var line in lines)
    {
        var e = Equation.From(line);
        e.Solve();
    }
}


internal class Equation
{
    public static Equation From(string input)
    {
        var eqParts= input.Split(":", StringSplitOptions.RemoveEmptyEntries);

        var result = long.Parse(eqParts[0]);

        var parts = eqParts[1].Split(" ", StringSplitOptions.RemoveEmptyEntries)
            .Select(p => long.Parse(p))
            .ToList();

        return new Equation()
        {
            Result = result,
            Parts = parts
        };
    }

    public bool Solve()
    {
        // using only + or *, attempt parts to equal result;

        // generate all possible sets
        var operatorCount = this.Parts.Count - 1;
        // when 2 parts, only 2 iterations (+ or -)
        // when 3 parts, 1+1+1, 1+1*1, 1*1*1, 1*1+1
        var iterations = operatorCount * operatorCount;

        for (var i = 0; i < iterations; i++)
        {
            Console.WriteLine(i);
        }

        return false;
    }

    private long Result;
    private List<long> Parts;
}