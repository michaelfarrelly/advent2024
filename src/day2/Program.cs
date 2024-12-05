Console.WriteLine("Running..");
// Day2Part1();
Day2Part2();
return;

static void Day2Part1()
{
    Console.WriteLine("Day 2");
    // load the data
    var lines = File.ReadLines("src/day2/input.txt");
    
    Console.WriteLine(string.Join(",", lines));
    
    // process the data
    foreach (var line in lines)
    {
        var isSafe = IsRecordSafe(ToNumbers(line));
        Console.WriteLine("{0}: {1}", line, isSafe);
        
    }

    Console.WriteLine(lines
        .Select(l => IsRecordSafe(ToNumbers(l)))
        .Count(l => l == true));

}

static void Day2Part2()
{
    Console.WriteLine("Day 2:Part2");
    // load the data
    var lines = File.ReadLines("src/day2/inputFull.txt");
    
    Console.WriteLine(string.Join(",", lines));
    
    // process the data
    foreach (var line in lines)
    {
        var isSafe = IsRecordSafe2(ToNumbers(line));
        Console.WriteLine("{0}: {1}", line, isSafe);
        
    }

    Console.WriteLine(lines
        .Select(l => IsRecordSafe2(ToNumbers(l)))
        .Count(l => l == true));
}


static List<int> ToNumbers(string record)
{
    return record
        .Split(" ")
        .Select(r => int.Parse(r))
        .ToList();
}

static bool IsRecordSafe(List<int> numbers)
{
    // - The levels are either all increasing or all decreasing.
    // - Any two adjacent levels differ by at least one and at most three.

    var dir = numbers[0] - numbers[1] > 0 
        ? // decreasing
          -1 
        : numbers[0] - numbers[1] < 0 
            ?
            // increasing
            1 : 
            // no change
            0;
    if (dir == 0)
    {
        // no change on first digits, not safe
        return false;
    }
    
    for (var index = 1; index < numbers.Count; index++)
    {
        var diff = numbers[index - 1] - numbers[index];
        if (Math.Abs(diff) < 1 || Math.Abs(diff) > 3)
        {
            return false;
        }

        if (dir < 0 && diff < 0)
        {
            return false;
        }
        
        if (dir > 0 && diff > 0)
        {
            return false;
        }
    }

    return true;
}


static bool IsRecordSafe2(List<int> numbers, bool deep = true)
{
    // - The levels are either all increasing or all decreasing.
    // - Any two adjacent levels differ by at least one and at most three.
    if (deep)
    {
        Console.WriteLine(":{0}", string.Join("-", numbers));
    
    }
    if (deep == false)
    {
        Console.WriteLine(" - {0}", string.Join("-", numbers));
    }
    
    // brute!

    if (IsRecordSafe(numbers))
    {
        return true;
    }
    
    for (var i = 0; i < numbers.Count; i++)
    {
        if (IsRecordSafe(RemoveIndex(numbers, i)))
        {
            return true;
        }
    }
    return false;
}

static List<int> RemoveIndex(List<int> numbers, int index)
{
    return numbers
        .Where((n, index1) => index1 != index)
        .ToList();
}
