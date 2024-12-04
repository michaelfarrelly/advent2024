// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;
using System.Linq;

// Day1();

Day2();

void Day1() {
// Console.WriteLine("Hello, World!");

    string input = """
                   3   4
                   4   3
                   2   5
                   1   3
                   3   9
                   3   3
                   """;

// parse the input into lists
    string[] lines = input.Split(Environment.NewLine);

    List<List<int>> buckets = new List<List<int>>();
    buckets.Add(new List<int>());
    buckets.Add(new List<int>());

    foreach (var line in lines)
    {
        var parts = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);
        for (var index = 0; index < parts.Length; index++)
        {
            var part = parts[index];
            buckets[index].Add(int.Parse(part));
        }
    }

    buckets[0].Sort();
    buckets[1].Sort();

// compare
    var deltas = new List<int>();
    foreach (var i in Enumerable.Range(0, buckets[0].Count)) {
        deltas.Add(Math.Abs(buckets[0][i] - buckets[1][i]));
    }

    Console.WriteLine(string.Join(",", lines));

    foreach (var b in buckets)
    {
        Console.WriteLine(string.Join(",", b));
    }

    Console.WriteLine(string.Join(",", deltas));
    Console.WriteLine(deltas.Sum());
}

void Day2() {
// Console.WriteLine("Hello, World!");

    string input = """
                   3   4
                   4   3
                   2   5
                   1   3
                   3   9
                   3   3
                   """;

// parse the input into lists
    string[] lines = input.Split(Environment.NewLine);

    List<List<int>> buckets = new List<List<int>>();
    buckets.Add(new List<int>());
    buckets.Add(new List<int>());

    foreach (var line in lines)
    {
        var parts = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);
        for (var index = 0; index < parts.Length; index++)
        {
            var part = parts[index];
            buckets[index].Add(int.Parse(part));
        }
    }

    // buckets[0].Sort();
    // buckets[1].Sort();

// compare
    var similarity = new List<int>();
    foreach (var i in Enumerable.Range(0, buckets[0].Count))
    {
        var value = buckets[0][i];
        var cnt = buckets[1].Count(n => n == value);
        similarity.Add(value * cnt);
    }

    Console.WriteLine(string.Join(",", lines));

    foreach (var b in buckets)
    {
        Console.WriteLine(string.Join(",", b));
    }

    Console.WriteLine(string.Join(",", similarity));
    Console.WriteLine(similarity.Sum());

}