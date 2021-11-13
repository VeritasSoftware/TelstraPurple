using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TelstraPurple.Robot.Models;

namespace TelstraPurple.Robot.Services
{
    public class RobotService : IRobotService
    {
        public IEnumerable<Command> ParseCommands(string commands)
        {
            //get command lines
            var lines = Regex.Match(commands, "^((?<lines>.+?)((\r)?\n|$))+$")
                                .Groups["lines"]
                                .Captures.Cast<Capture>()
                                .Select(x => x.Value);

            //parse each command line
            var commandList = lines.Select(x => this.ParseLine(x)).Where(x => x != null);

            return commandList;
        }

        public Command ParseLine(string line)
        {
            //PLACE
            var match = Regex.Match(line, @"^(\t|\s)*PLACE\s+(\s*(\d)\s*\,\s*(\d)(,\s*)?(NORTH|SOUTH|EAST|WEST)?\s*)(\r\n)*$", RegexOptions.IgnoreCase | RegexOptions.Compiled);

            if (match.Success)
            {
                return new Command { Name = CommandType.PLACE, Arguments = match.Groups[2].Captures[0].Value };
            }

            //MOVE
            match = Regex.Match(line, @"^(\t|\s)*MOVE\s*(\r\n)*$", RegexOptions.IgnoreCase | RegexOptions.Compiled);

            if (match.Success)
            {
                return new Command { Name = CommandType.MOVE, Arguments = null };
            }

            //LEFT
            match = Regex.Match(line, @"^(\t|\s)*LEFT\s*(\r\n)*$", RegexOptions.IgnoreCase | RegexOptions.Compiled);

            if (match.Success)
            {
                return new Command { Name = CommandType.LEFT, Arguments = null };
            }

            //RIGHT
            match = Regex.Match(line, @"^(\t|\s)*RIGHT\s*(\r\n)*$", RegexOptions.IgnoreCase | RegexOptions.Compiled);

            if (match.Success)
            {
                return new Command { Name = CommandType.RIGHT, Arguments = null };
            }

            return null;
        }

        public Location ParsePlaceArgs(string args)
        {
            var coords = Regex.Match(args, @"^\s*(\d)\s*\,\s*(\d)(,\s*)?(NORTH|SOUTH|EAST|WEST)?\s*$", RegexOptions.IgnoreCase | RegexOptions.Compiled);

            if (coords.Success)
            {
                var d = (Direction)Enum.Parse(typeof(Direction), coords.Groups[4].Captures[0].Value);

                return new Location
                {
                    X = int.Parse(coords.Groups[1].Captures[0].Value),
                    Y = int.Parse(coords.Groups[2].Captures[0].Value),
                    Direction = d
                };
            }

            return null;
        }

        public Location ParsePlaceArgs(string args, Direction direction)
        {
            var coords = Regex.Match(args, @"^\s*(\d)\s*\,\s*(\d)\s*$", RegexOptions.IgnoreCase | RegexOptions.Compiled);

            if (coords.Success)
            {
                return new Location
                {
                    X = int.Parse(coords.Groups[1].Captures[0].Value),
                    Y = int.Parse(coords.Groups[2].Captures[0].Value),
                    Direction = direction
                };
            }

            return null;
        }

        public Location ParseMove(Location presentLocation)
        {
            switch (presentLocation.Direction)
            {
                case Direction.NORTH:
                    return new Location { X = presentLocation.X, Y = presentLocation.Y + 1, Direction = presentLocation.Direction };
                case Direction.EAST:
                    return new Location { X = presentLocation.X + 1, Y = presentLocation.Y, Direction = presentLocation.Direction };
                case Direction.SOUTH:
                    return new Location { X = presentLocation.X, Y = presentLocation.Y - 1, Direction = presentLocation.Direction };
                case Direction.WEST:
                    return new Location { X = presentLocation.X - 1, Y = presentLocation.Y, Direction = presentLocation.Direction };
                default:
                    return null;
            }
        }

        public Location ParseDirection(Location presentLocation, Movement movement)
        {
            switch(movement)
            {
                case Movement.LEFT:
                    switch (presentLocation.Direction)
                    {
                        case Direction.NORTH:
                            presentLocation.Direction = Direction.WEST;
                            break;
                        case Direction.EAST:
                            presentLocation.Direction = Direction.NORTH;
                            break;
                        case Direction.SOUTH:
                            presentLocation.Direction = Direction.EAST;
                            break;
                        case Direction.WEST:
                            presentLocation.Direction = Direction.SOUTH;
                            break;
                        default:
                            return null;
                    }
                    break;
                case Movement.RIGHT:
                    switch (presentLocation.Direction)
                    {
                        case Direction.NORTH:
                            presentLocation.Direction = Direction.EAST;
                            break;
                        case Direction.EAST:
                            presentLocation.Direction = Direction.SOUTH;
                            break;
                        case Direction.SOUTH:
                            presentLocation.Direction = Direction.WEST;
                            break;
                        case Direction.WEST:
                            presentLocation.Direction = Direction.NORTH;
                            break;
                        default:
                            return null;
                    }
                    break;
            }            

            return presentLocation;
        }
    }
}
