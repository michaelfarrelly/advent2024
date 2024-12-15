using System.Collections.Immutable;
using Microsoft.VisualBasic.FileIO;

Day11Part1(args[0]);
// Day7Part2(args[0]);
return;

static void Day11Part1(string filename = "src/day11/input.txt")
{
    // load the data
    var lines = File
        .ReadLines(filename)
        .ToList();
    
    // Console.WriteLine(string.Join("\n", lines));

    foreach (var l in lines)
    {
        var validObject = l.Parse();
        Console.WriteLine("0: {0}", string.Join(",", validObject));

        var b = new Blinker(validObject);
        
        foreach (var n in Enumerable.Range(0, 75))
        {
         
            
            b = b.Blink();

            // Console.WriteLine("{1} Blink: {0}", string.Join(",", b.stones), n);
            // Console.WriteLine("Blink-Count: {0}", b.stones.Count);
        }
        
        Console.WriteLine("----");
        Console.WriteLine("Blink: {0}", string.Join(",", b.stones));
        Console.WriteLine("Blink-Count: {0}", b.stones.Count);
    }
}


public static class Day11Extensions
{
    public static ImmutableList<Item> Parse(this string input)
    {
        return input
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(l => new Item(value: long.Parse(l), raw: l))
            .ToImmutableList();
    }
}

public class Item
{
    public Item(long value, string raw)
    {
        this.value = value;
        this.raw = raw;
    }

    public override string ToString()
    {
        return $"{this.value}";
    }

    public string raw;
    public long value;
}

public class Blinker
{
    public Blinker(ImmutableList<Item> stones)
    {
        this.stones = stones;
    }

    public Blinker Blink()
    {
        
        var result = new List<Item>();
        foreach (var stone in this.stones)
        {
            // If the stone is engraved with the number 0, it is replaced by a
            // stone engraved with the number 1.

            if (stone.value == 0)
            {
                result.Add(new Item(1, "1"));
            }
            else if (stone.raw.Length % 2 == 0)
            {

                // If the stone is engraved with a number that has an even number of digits,
                // it is replaced by two stones. The left half of the digits are engraved on
                // the new left stone, and the right half of the digits are engraved on
                // the new right stone. (The new numbers don't keep extra leading
                // zeroes: 1000 would become stones 10 and 0.)
                var l = stone.raw.Length / 2;
                var v = long.Parse(stone.raw.Substring(0, l));
                var v2 = long.Parse(stone.raw.Substring(l));
                result.Add(new Item(v, raw: v.ToString()));
                result.Add(new Item(v2, raw: v2.ToString()));
            }
            else
            {
            
                // If none of the other rules apply, the stone is replaced by a new stone;
                // the old stone's number multiplied by 2024 is engraved on the new stone.

                var v = stone.value * 2024;
                var s = v.ToString();
                
                result.Add(new Item(v, raw: s));
            }
            
            
        }
        return new Blinker(result.ToImmutableList());
    }

    public ImmutableList<Item> stones;
}