using System.Linq;
using TelstraPurple.Robot.Models;
using TelstraPurple.Robot.Services;
using Xunit;

namespace TelstraPurple.Robot.UnitTests
{
    public class RobotServiceTests
    {
        [Fact]
        public void Commands_Parse_Success()
        {
            var robotService = new RobotService();

            var strCommands = @"
                PLACE 1,2,EAST
                MOVE
                MOVE
                LEFT
                MOVE
            ";

            var commands = robotService.ParseCommands(strCommands);

            Assert.Equal(CommandType.PLACE, commands.ElementAt(0).Name);
            Assert.Equal(CommandType.MOVE, commands.ElementAt(1).Name);
            Assert.Equal(CommandType.MOVE, commands.ElementAt(2).Name);
            Assert.Equal(CommandType.LEFT, commands.ElementAt(3).Name);
            Assert.Equal(CommandType.MOVE, commands.ElementAt(4).Name);

            strCommands = @"
                PLACE 1,2,EAST
                MOVE
                LEFT
                MOVE
                PLACE 3,1
                MOVE
            ";

            commands = robotService.ParseCommands(strCommands);

            Assert.Equal(CommandType.PLACE, commands.ElementAt(0).Name);
            Assert.Equal(CommandType.MOVE, commands.ElementAt(1).Name);
            Assert.Equal(CommandType.LEFT, commands.ElementAt(2).Name);
            Assert.Equal(CommandType.MOVE, commands.ElementAt(3).Name);
            Assert.Equal(CommandType.PLACE, commands.ElementAt(4).Name);
            Assert.Equal(CommandType.MOVE, commands.ElementAt(5).Name);
        }
    }
}
