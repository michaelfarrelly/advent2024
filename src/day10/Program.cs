// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");

Day10Part1(args[0]);
// Day7Part2(args[0]);
return;

static void Day10Part1(string filename = "src/day10/input.txt")
{
    // load the data
    var lines = File
        .ReadLines(filename)
        .ToList();
    
    // Console.WriteLine(string.Join("\n", lines));

    var t = new TrailHeads(lines);

    Console.WriteLine("th {0}", t.GetScore());
}

class TrailHeads
{
    private readonly int[][] map;

    public TrailHeads(List<string> map)
    {
        this.map = map
            .Select(s => s
                    .Select(d => int.Parse(d.ToString()))
                    .ToArray()
            )
            .ToArray();
        
    }

    public int GetScore()
    {
        var trailHeads = GetPositionsOf(0);
        var trailEnds = GetPositionsOf(9);

        var thScore = 0;
        foreach (var h in trailHeads)
        {
            foreach (var t in trailEnds)
            {
                Console.WriteLine("{0}x{1}", h.x, h.y);
                Console.WriteLine("{0}x{1}", t.x, t.y);
                var r = FindRoute(h, t);
                // Console.WriteLine(r.Count);
                if (r.Count > 0)
                {
                    thScore++;
                }
            }
        }
        
        return thScore;
    }

    public List<Position> GetPositionsOf(int value)
    {
        var result = new List<Position>();
        for (var y = 0; y < map.Length; y++)
        {

            for (var x = 0; x < map[0].Length; x++)
            {
                if (this.map[y][x] == value)
                {
                    result.Add(new Position(x,y));
                }
            }
        }
        return result;
    }

    public List<Position> FindRoute(Position head, Position end)
    {
        // using the map, find a route from head to end.
        // if unable, return empty list.

        var result = new List<Position>() { };
        var running = true;
        var i = 0;
        var current = head.Clone();
   
        // only moves are N,S,E,W
        
        // move north?
        var n = current.Add(new Position(0, -1));
        var s = current.Add(new Position(0, 1));
        var e = current.Add(new Position(1, 0));
        var w = current.Add(new Position(-1, 0));
        
        // check if position is valid, and "easy"
        var possibles = new List<Position>()
        {
            n, s, e, w
        };
        foreach (var p in possibles)
        {
            if (p.x >= 0 && p.y >= 0 && p.x < this.map[0].Length && p.y < this.map.Length)
            {
                // Console.WriteLine("- possible {0} {1}: {2}", p.x, p.y, this.map[p.y][p.x]);

                // valid position, but is it valid "easy" hike move
                var c = this.map[current.y][current.x];
                var next = this.map[p.y][p.x];
                if (c + 1 == next && next < 9)
                {
                    // Console.WriteLine("- possible {0} {1}: {2}", p.x, p.y, this.map[p.y][p.x]);
                    result.AddRange( FindRoute(p, end));
                }
                else if (c + 1 == next && next == 9)
                {
                    // Console.WriteLine(" - Found route to 9");
                    if (p.x == end.x && p.y == end.y)
                    {
                        result.Add(p);
                    }
                }
            }
        }

        if (result.Count > 1)
        {
            var l = result.Last();
            if (this.map[l.y][l.x] != 9 || (l.x != end.x && l.y != end.y))
            {
                // Console.WriteLine("rubish");
                return new List<Position>();
            }
        }

        return result;
    }
}


class Position(int x, int y)
{
    public int x = x;
    public int y = y;

    public Position Add(Position pos)
    {
        return new Position(this.x + pos.x, this.y + pos.y);
    }

    public Position Clone()
    {
        return new Position(this.x, this.y);
    }
    
    public static Position operator +(Position a, Position b) 
        => new Position(a.x + b.x, a.y + b.y);
}