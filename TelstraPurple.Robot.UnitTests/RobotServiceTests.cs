using System;
using System.Linq;
using TelstraPurple.Robot.Models;
using TelstraPurple.Robot.Services;
using Xunit;

namespace TelstraPurple.Robot.UnitTests
{
    public class RobotServiceTests
    {
        [Fact]
        public void ParseCommands_Success()
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

            Assert.Equal(5, commands.Count());
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

            Assert.Equal(6, commands.Count());
            Assert.Equal(CommandType.PLACE, commands.ElementAt(0).Name);
            Assert.Equal(CommandType.MOVE, commands.ElementAt(1).Name);
            Assert.Equal(CommandType.LEFT, commands.ElementAt(2).Name);
            Assert.Equal(CommandType.MOVE, commands.ElementAt(3).Name);
            Assert.Equal(CommandType.PLACE, commands.ElementAt(4).Name);
            Assert.Equal(CommandType.MOVE, commands.ElementAt(5).Name);
        }

        [Fact]
        public void ParseCommands_Ignore_InvalidCommand_Success()
        {
            var robotService = new RobotService();

            var strCommands = @"
                PLACE 1,2,EAST
                MOVE
                ABC - Invalid
                MOVE
                LEFT
                MOVE
            ";

            var commands = robotService.ParseCommands(strCommands);

            Assert.Equal(5, commands.Count());
            Assert.Equal(CommandType.PLACE, commands.ElementAt(0).Name);
            Assert.Equal(CommandType.MOVE, commands.ElementAt(1).Name);
            Assert.Equal(CommandType.MOVE, commands.ElementAt(2).Name);
            Assert.Equal(CommandType.LEFT, commands.ElementAt(3).Name);
            Assert.Equal(CommandType.MOVE, commands.ElementAt(4).Name);            
        }

        [Fact]
        public void ParseCommands_Place_Command_Not_First_Command_Failure()
        {
            var robotService = new RobotService();

            var strCommands = @"
                MOVE
                MOVE
                LEFT
                MOVE
            ";

            try
            {
                var commands = robotService.ParseCommands(strCommands);
            }
            catch(Exception ex)
            {
                Assert.Equal("PLACE x, y NORTH|SOUTH|EAST|WEST should be the first command.", ex.Message);
            }            
        }

    }
}
