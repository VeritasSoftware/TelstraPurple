using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TelstraPurple.Robot.UI.Models
{
    public class Cell
    {
        public int X { get; set; }
        public int Y { get; set; }
    }

    public class Command
    {
        public CommandType Name { get; set; }
        public string Arguments { get; set; }
    }

    public enum CommandType
    {
        PLACE,
        MOVE,
        LEFT,
        RIGHT,
        REPORT
    }

    public enum Direction
    {
        NORTH,
        SOUTH,
        EAST,
        WEST
    }

    public class Location
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Direction Direction { get; set; }
    }

    public enum Movement
    {
        LEFT,
        RIGHT
    }
}
