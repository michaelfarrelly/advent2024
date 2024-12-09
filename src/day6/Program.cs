// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");

// Day6Part1(args[0]);
Day6Part2(args[0]);
return;

static void Day6Part1(string filename = "src/day6/input.txt")
{
    // load the data
    var lines = File
        .ReadLines(filename)
        .ToList();
    
    // Console.WriteLine(string.Join("\n", lines));

    var m = new Map(lines);
    
    m.Run();
    
    Console.WriteLine(m.ShowMap());
    
    Console.WriteLine("Moves {0}", m.CountMoves());
}


static void Day6Part2(string filename = "src/day6/input.txt")
{
    // load the data
    var lines = File
        .ReadLines(filename)
        .ToList();

    var loopResult = 0;

    var alternateMaps = lines.Count() * lines[0].Length - 1;
    var nextPosition = new Position(0, 0);
    for (var a = 0; a < alternateMaps; a++)
    {
        var m = new Map(lines);
        m.AddObstruction(nextPosition);
        nextPosition += new Position(1, 0);
        if (nextPosition.x >= lines[0].Length)
        {
            nextPosition.y++;
            nextPosition.x = 0;
        }

        var state = m.Run();
        
        // Console.WriteLine(m.ShowMap());

        if (state == MapState.Looped)
        {
            loopResult++;
        }
    }

    Console.WriteLine(alternateMaps);
    
    // 16829 too high;
    Console.WriteLine(loopResult);

    // Console.WriteLine(m.ShowMap());

    // Console.WriteLine("Moves {0}", m.CountMoves());
}

internal class Map
{
    private Position GuardPosition;
    private Direction GuardDirection;
    private char[][] map;

    internal Map(List<string> map)
    {
        this.map = map
            .Select(s => s.ToArray())
            .ToArray();

        
        // find guard position.
        this.GuardPosition = this.FindGuardPosition();
    }

    public void AddObstruction(Position pos)
    {
        if (ValidPosition(pos) && this.map[pos.y][pos.x] != '^')
        {
            this.map[pos.y][pos.x] = '#';
        }
    }

    public string ShowMap()
    {
        return string.Join(Environment.NewLine, this.map.Select(s => string.Join("", s)));
    }

    public MapState Run()
    {
        var running = true;
        var i = 0;
        while (running)
        {
            var oldPosition = this.GuardPosition.Clone();
            var state = this.UpdateGuardPosition();
            this.MarkGuardPosition(oldPosition);
            
            // debug:
            i++;
            if (i > 50000)
            {
                return MapState.Looped;
            }
            if (state == GuardState.Done)
            {
                running = false;
            }
        }
        
        // Console.WriteLine("Moves {0}", CountMoves());
        return MapState.Complete;
    }

    public int CountMoves()
    {
        return ShowMap().Count(c =>
        {
            return c == 'X';
        });
    }

    private void MarkGuardPosition(Position oldPosition)
    {
        this.map[oldPosition.y][oldPosition.x] = 'X';
    }

    private GuardState UpdateGuardPosition()
    {
        // move by direction
        var dDelta = this.ToPosition(this.GuardDirection);
        
        var nextPosition = this.GuardPosition + dDelta;
        // check if guard is on map
        
        if (!ValidPosition(nextPosition))
        {
            return GuardState.Done;
        }
        
        // check if position is a #,
        var blocked = this.FindObjectAt(nextPosition);
        // if #, rotate
        if (blocked)
        {
            this.GuardDirection = Rotate(this.GuardDirection);
            return GuardState.Rotate;
        }

        // otherwise move direction.
        this.GuardPosition += dDelta;
        return GuardState.Move;
    }

    private bool ValidPosition(Position nextPosition)
    {
        return nextPosition.x >= 0 && nextPosition.y >= 0
            && nextPosition.x < this.map[0].Length
            && nextPosition.y < this.map.Length;
    }

    private bool FindObjectAt(Position nextPosition)
    {
        return this.map[nextPosition.y][nextPosition.x] == '#';
    }


    private Position FindGuardPosition()
    {
        for (var y = 0; y < this.map.Length; y++)
        {
            if (this.map[y].Contains('^'))
            {
                for (var x = 0; x < this.map[y].Length; x++)
                {
                    if (this.map[y][x].Equals('^'))
                    {
                        return new Position(x, y);
                    }
                }
            }
        }
        // var y = this.ma.FindIndex((s) => s.Contains('^', StringComparison.InvariantCulture));
        // var x = this.map[y].IndexOf('^', StringComparison.InvariantCulture);

         return new Position(-1, -1);
    }

    Direction Rotate(Direction d)
    {
        return d switch
        {
            Direction.N => Direction.E,
            Direction.S => Direction.W,
            Direction.E => Direction.S,
            Direction.W => Direction.N,
            _ => throw new ArgumentOutOfRangeException(nameof(d), d, null)
        };
   
    }
    
    Position ToPosition(Direction d)
    {
        return d switch
        {
            Direction.N => new Position(0, -1),
            Direction.S => new Position(0, 1),
            Direction.E => new Position(1, 0),
            Direction.W => new Position(-1, 0),
            _ => throw new ArgumentOutOfRangeException(nameof(d), d, null)
        };
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

enum Direction
{
    N = 0,
    S,
    E,
    W,
}

enum GuardState
{
    Move = 0,
    Rotate = 1,
    Done = 2
}

enum MapState
{
    Complete = 0,
    Looped,
}