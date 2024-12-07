// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

Day4Part1();
Day4Part2();
return;

static void Day4Part1()
{
    Console.WriteLine("Day 4");
    // load the data
    var lines = File
        .ReadLines("src/day4/inputFull.txt")
        .ToList();
    
    Console.WriteLine(string.Join(",\n", lines));
    
    Console.WriteLine(FindString(lines, "XMAS"));

}
static void Day4Part2()
{
    Console.WriteLine("Day 3; Part 2");
    // load the data
    var lines = File
        .ReadLines("src/day4/inputFull.txt")
        .ToList();
    
    Console.WriteLine(string.Join(",\n", lines));
    
    Console.WriteLine(FindXShape(lines, "MAS"));

}

static int FindString(List<string> lines, string letters)
{
    var result = 0;
    var countX = lines[0].Length; // 10
    var countY = lines.Count; // 10
    
    for (var i = 0; i < lines.Count; i++)
    {
        for (var j = 0; j < lines[i].Length; j++)
        {
            // detect letters[0].
            if (lines[i][j] == letters[0])
            {
                
                var r = GetStringDirection(lines, j, i,  1, 0, letters.Length);

                var l = GetStringDirection(lines, j, i,  -1, 0, letters.Length);

                
                // diagonal /
                var tr = GetStringDirection(lines, j, i,  1, -1, letters.Length);
                
                var tl = GetStringDirection(lines, j, i, -1, -1, letters.Length);
                
                var br = GetStringDirection(lines, j, i,  1,  1, letters.Length);
                
                var bl = GetStringDirection(lines, j, i,  -1, 1, letters.Length);
                
                var b = GetStringDirection(lines, j, i,  0, 1, letters.Length);
                var t = GetStringDirection(lines, j, i,  0, -1, letters.Length);

                
                if (tr.StartsWith(letters))
                {
                    result++;
                }
                if (tl.StartsWith(letters))
                {
                    result++;
                }
                if (br.StartsWith(letters))
                {
                    result++;
                }
                if (bl.StartsWith(letters))
                {
                    result++;
                }
                if (b.StartsWith(letters))
                {
                    result++;
                }
                if (t.StartsWith(letters))
                {
                    result++;
                }
                
                if (r.StartsWith(letters))
                {
                    result++;
                }
                if (l.StartsWith(letters))
                {
                    result++;
                }
            }
        }   
    }

    return result;
}


static int FindXShape(List<string> lines, string letters)
{
    var result = 0;
    var countX = lines[0].Length; // 10
    var countY = lines.Count; // 10
    
    for (var i = 0; i < lines.Count; i++)
    {
        for (var j = 0; j < lines[i].Length; j++)
        {
            // detect letters[1].
            if (lines[i][j] == letters[1])
            {
                // center "MAS".
                
                // var r = GetStringDirection(lines, j, i,  1, 0, letters.Length);

                // var l = GetStringDirection(lines, j, i,  -1, 0, letters.Length);

                
                // diagonal /
                var tr = GetStringDirection(lines, j - 1, i + 1,  1, -1, letters.Length);
                
                var tl = GetStringDirection(lines, j + 1, i + 1, -1, -1, letters.Length);
                
                var br = GetStringDirection(lines, j - 1, i - 1,  1,  1, letters.Length);
                
                var bl = GetStringDirection(lines, j + 1, i - 1,  -1, 1, letters.Length);
                
                // var b = GetStringDirection(lines, j, i,  0, 1, letters.Length);
                // var t = GetStringDirection(lines, j, i,  0, -1, letters.Length);

                
                if (tr.StartsWith(letters) && br.StartsWith(letters))
                {
                    result++;
                }
                if (tl.StartsWith(letters) && bl.StartsWith(letters))
                {
                    result++;
                }
                
                if (bl.StartsWith(letters) && br.StartsWith(letters))
                {
                    result++;
                }
                
                if (tl.StartsWith(letters) && tr.StartsWith(letters))
                {
                    result++;
                }
                // if (br.StartsWith(letters))
                // {
                //     result++;
                // }
                // if (bl.StartsWith(letters))
                // {
                //     result++;
                // }
                // if (b.StartsWith(letters))
                // {
                //     result++;
                // }
                // if (t.StartsWith(letters))
                // {
                //     result++;
                // }
                //
                // if (r.StartsWith(letters))
                // {
                //     result++;
                // }
                // if (l.StartsWith(letters))
                // {
                //     result++;
                // }
            }
        }   
    }

    return result;
}

static string GetStringDirection(List<string> lines, 
    int x, int y, 
    int deltaX, int deltaY,
    int length)
{
    var result = "";
    // Console.WriteLine("{0}: {1}x{2}, {3}x{4}", result, x, y, deltaX, deltaY);
    foreach (var i in Enumerable.Range(0, length))
    {
        var nextY = y + (deltaY * i);
        var nextX = x + (deltaX * i);
        if (nextY >= 0 && nextY < lines.Count && nextX >= 0 && nextX < lines[0].Length)
        {
            result += lines[nextY][nextX];
            // Console.WriteLine(" - {0}: {1}x{2}, {3}x{4} {5}x{6}", result, x, y, deltaX, deltaY, nextX, nextY);
        }
    }

    // if (result == "XMAS")
    // {
    //     Console.WriteLine("{0}: {1}x{2}, {3}x{4}", result, x, y, deltaX, deltaY);
    // } 

    return result;
    // return lines[y][x] +
    //        lines[y - 1][x - 1] +
    //        lines[y - 2][x - 2] +
    //        lines[y - 3][x - 3];
}