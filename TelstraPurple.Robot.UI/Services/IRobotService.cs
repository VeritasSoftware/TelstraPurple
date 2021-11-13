using System.Collections.Generic;
using TelstraPurple.Robot.UI.Models;

namespace TelstraPurple.Robot.UI.Services
{
    public interface IRobotService
    {
        IEnumerable<Command> ParseCommands(string commands);
        Command ParseLine(string line);
        Location ParseMove(Location presentLocation);
        Location ParseDirection(Location presentLocation, Movement movement);
        Location ParsePlaceArgs(string args);
        Location ParsePlaceArgs(string args, Direction direction);
    }
}