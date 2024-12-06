// See https://aka.ms/new-console-template for more information

using System.Text.RegularExpressions;

Console.WriteLine("Hello, World!");

Day3Part1();
return;

static void Day3Part1()
{
    Console.WriteLine("Day 3");
    // load the data
    var lines = File.ReadLines("src/day3/inputFull.txt");
    
    Console.WriteLine(string.Join(",", lines));
    
    // process the data
    var result = 0;
    foreach (var line in lines)
    {
        // get mul([0-9],[0-9]) matches
        var reg = new Regex(@"mul\(([0-9]+),([0-9]+)\)");
        var matches = reg.Matches(line);
        foreach (var m in matches)
        {
            var values = m
                .ToString()
                .Replace("mul(", "")
                .TrimEnd(')')
                .Split(",")
                .Select(s => int.Parse(s))
                .ToList();
            
            var resultInner = values.Aggregate(1, (current, v) => current * v);
            result += resultInner;
        }

    }
    Console.WriteLine(result);

}