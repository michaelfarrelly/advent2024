Console.WriteLine("Day 5");

Day5Part1();
Day5Part2();
return;

static void Day5Part1()
{
    // load the data
    var lines = File
        .ReadLines("src/day5/inputFull.txt")
        .ToList();
    
    
    // page ordering rules
    var (pageOrdering, updates) = Divide(lines);

    // Console.WriteLine(string.Join(",\n", pageOrdering));
    // Console.WriteLine(string.Join(",\n", updates));

    var result = 0;
    // updates
    foreach (var u in updates)
    {
        // Console.WriteLine("{0}:", string.Join(",", u));
        var correct = IsUpdateCorrect(u, pageOrdering);
        // Console.WriteLine("{0}: {1}", string.Join(",", u), correct);

        if (correct)
        {
            result += MiddleValue(u);
        }
    }

    Console.WriteLine(result);
}

static void Day5Part2()
{
    // load the data
    var lines = File
        .ReadLines("src/day5/inputFull.txt")
        .ToList();
    
    
    // page ordering rules
    var (pageOrdering, updates) = Divide(lines);

    // Console.WriteLine(string.Join(",\n", pageOrdering));
    // Console.WriteLine(string.Join(",\n", updates));

    var result = 0;
    // updates
    foreach (var u in updates)
    {
        var correct = IsUpdateCorrect(u, pageOrdering);
        
        if (!correct)
        {
            var updatedOrder = UpdateOrder(u, pageOrdering);
            result += MiddleValue(updatedOrder);
        }
    }

    Console.WriteLine(result);
}

static List<int> UpdateOrder(List<int> originalOrder, List<(int, int)> pageOrdering)
{
    List<int> updatedOrder = [..originalOrder.ToList()];
    for (var i = 0; i < updatedOrder.Count; i++)
    {
        var n = updatedOrder[i];
        var rules1 = pageOrdering.Where(p => p.Item1 == n).ToList();
        var rules2 = pageOrdering.Where(p => p.Item2 == n).ToList();
        // 4998 to high
        // 4849 to low
        
        // check backwards rules?
        foreach (var r in rules2)
        {
            var x = updatedOrder.IndexOf(r.Item1);
            if (updatedOrder.Contains(r.Item1) && x > i)
            {
                // move r.Item1 to before current index.
                updatedOrder.RemoveAt(x);
                updatedOrder.Insert(i, r.Item1);
            }
        }
    }
    return updatedOrder;
}

static int MiddleValue(List<int> update)
{
    var updateMiddleIndex = update.Count / 2;
    // 10 / 2 = 5 - 1 = 4
    return update[updateMiddleIndex];
}

static bool IsUpdateCorrect(List<int> update, List<(int, int)> pageOrdering)
{
    foreach (var n in update)
    {
        var currentIndex = update.IndexOf(n);
        // find rules for ordering.
        var rules1 = pageOrdering.Where(p => p.Item1 == n).ToList();
        var rules2 = pageOrdering.Where(p => p.Item2 == n).ToList();
        
        // Console.WriteLine("- {0} __  {1}", string.Join(",", rules1), string.Join(",", rules2));
        
        // check forward rules
        foreach (var r in rules1)
        {
            if (update.Contains(r.Item2) && update.IndexOf(r.Item2) > currentIndex)
            {
                // ok
            }
            // ?
        }
        // check backwards rules?
        foreach (var r in rules2)
        {
            if (update.Contains(r.Item1) && update.IndexOf(r.Item1) < currentIndex)
            {
                // ok
            }
            
            if (update.Contains(r.Item1) && update.IndexOf(r.Item1) >= currentIndex)
            {
                return false;
            }
            // ?
        }
    }

    return true;
}

static (List<(int,int)> pageOrdering,List<List<int>> updates) Divide(List<string> input)
{
    var divideIndex = input.FindIndex(string.IsNullOrWhiteSpace);
    return (
        ConvertToPageOrdering(input.Slice(0, divideIndex)),
        input
            .Slice(divideIndex + 1, input.Count - divideIndex - 1)
            .Select(s => s
                .Split(",")
                .Select(i => int.Parse(i))
                .ToList()
            )
            .ToList()
        );
}

static (int, int) ConvertToPageOrder(string input, char delim = '|')
{
    var result = input
        .Split(delim)
        .Select((i) => int.Parse(i))
        .ToList();
    return (result[0], result[1]);
}

static List<(int, int)> ConvertToPageOrdering(List<string> input)
{
    return input
        .Select(i => ConvertToPageOrder(i))
        .ToList();
}
