using System;
using System.Collections.Generic;
using System.Text;
using MarsRover.Domain;
using MarsRover.Domain.Classes;
using MarsRover.Domain.Enums;
using Xunit;

namespace MarsRover.Tests
{
    public class MarsRoverTests
    {
        public MarsRoverWrapper newMarsRoverWrapper;

        [Theory]
        [InlineData("5 5", "1 2 N", "LMLMLMLMM")]
        public void ApplySimulation_ShouldPass(string plaetauSize, string roverPosition, string roverCommand)
        {
            newMarsRoverWrapper = new MarsRoverWrapper(plaetauSize, roverPosition, roverCommand);

            var result = newMarsRoverWrapper.ApplySimulation();

            Assert.Equal(Directions.North, result.RoverDirection);
            Assert.Equal(1, result.XCoord);
            Assert.Equal(3, result.YCoord);
        }

        [Theory]
        [InlineData("3 3", "1 1 S", "MRMRMLM")]
        public void ApplySimulation_ShouldFail(string plaetauSize, string roverPosition, string roverCommand)
        {
            newMarsRoverWrapper = new MarsRoverWrapper(plaetauSize, roverPosition, roverCommand);

            Assert.Throws<Exception>(()=> newMarsRoverWrapper.ApplySimulation());
        }

        [Theory]
        [InlineData("5 6")]
        public void SetPlaetau_ShouldPass(string plaetauSize)
        {
            newMarsRoverWrapper = new MarsRoverWrapper();
            var plaetau = newMarsRoverWrapper.SetPlaetau(plaetauSize);

            Assert.IsType<Plaetau>(plaetau);
            Assert.NotNull(plaetau);
            Assert.Equal(5, plaetau.PlaetauSize.MaxXCoord);
            Assert.Equal(6, plaetau.PlaetauSize.MaxYCoord);
            Assert.NotNull(plaetau.PlaetauId);
        }

        [Theory]
        [InlineData("")]
        [InlineData("A B")]
        [InlineData("5 6 7")]
        public void SetPlaetau_ShouldFail(string plaetauSize)
        {
            newMarsRoverWrapper = new MarsRoverWrapper();

            Assert.Throws<Exception>(() => newMarsRoverWrapper.SetPlaetau(plaetauSize));
        }

        [Theory]
        [InlineData("2 3 w")]
        public void CreateRover_ShouldPass(string roverPosition)
        {
            newMarsRoverWrapper = new MarsRoverWrapper();
            var rover = newMarsRoverWrapper.CreateRover(roverPosition);

            Assert.IsType<Rover>(rover);
            Assert.NotNull(rover);
            Assert.Equal(2, rover.Position.XCoord);
            Assert.Equal(3, rover.Position.YCoord);
            Assert.Equal(Directions.West, rover.Position.RoverDirection);
            Assert.NotNull(rover.RoverId);
        }

        [Theory]
        [InlineData("A B E")]
        [InlineData(" ")]
        [InlineData("1 2 s W")]
        [InlineData("1 2 K")]
        public void CreateRover_ShouldFail(string roverPosition)
        {
            newMarsRoverWrapper = new MarsRoverWrapper();

            Assert.Throws<Exception>(() => newMarsRoverWrapper.CreateRover(roverPosition));
        }

        [Fact]
        public void TurnRoverLeft_ShouldPass()
        {
            newMarsRoverWrapper = new MarsRoverWrapper();

            var newRoverPositionStartFromNorth = newMarsRoverWrapper.TurnRoverLeft(new RoverPosition(1, 2, Directions.North));
            var newRoverPositionStartFromSouth = newMarsRoverWrapper.TurnRoverLeft(new RoverPosition(1, 2, Directions.South));
            var newRoverPositionStartFromWest = newMarsRoverWrapper.TurnRoverLeft(new RoverPosition(1, 2, Directions.West));
            var newRoverPositionStartFromEast = newMarsRoverWrapper.TurnRoverLeft(new RoverPosition(1, 2, Directions.East));

            Assert.Equal(Directions.West, newRoverPositionStartFromNorth.RoverDirection);
            Assert.Equal(Directions.East, newRoverPositionStartFromSouth.RoverDirection);
            Assert.Equal(Directions.South, newRoverPositionStartFromWest.RoverDirection);
            Assert.Equal(Directions.North, newRoverPositionStartFromEast.RoverDirection);

        }

        [Fact]
        public void TurnRoverRight_ShouldPass()
        {
            newMarsRoverWrapper = new MarsRoverWrapper();

            var newRoverPositionStartFromNorth = newMarsRoverWrapper.TurnRoverRight(new RoverPosition(1, 2, Directions.North));
            var newRoverPositionStartFromSouth = newMarsRoverWrapper.TurnRoverRight(new RoverPosition(1, 2, Directions.South));
            var newRoverPositionStartFromWest = newMarsRoverWrapper.TurnRoverRight(new RoverPosition(1, 2, Directions.West));
            var newRoverPositionStartFromEast = newMarsRoverWrapper.TurnRoverRight(new RoverPosition(1, 2, Directions.East));

            Assert.Equal(Directions.East, newRoverPositionStartFromNorth.RoverDirection);
            Assert.Equal(Directions.West, newRoverPositionStartFromSouth.RoverDirection);
            Assert.Equal(Directions.North, newRoverPositionStartFromWest.RoverDirection);
            Assert.Equal(Directions.South, newRoverPositionStartFromEast.RoverDirection);

        }

        [Fact]
        public void MoveForward_ShouldPass()
        {
            newMarsRoverWrapper = new MarsRoverWrapper();

            var newRoverPositionStartFromNorth = newMarsRoverWrapper.MoveRoverForward(new RoverPosition(1, 2, Directions.North));
            var newRoverPositionStartFromSouth = newMarsRoverWrapper.MoveRoverForward(new RoverPosition(1, 2, Directions.South));
            var newRoverPositionStartFromWest = newMarsRoverWrapper.MoveRoverForward(new RoverPosition(1, 2, Directions.West));
            var newRoverPositionStartFromEast = newMarsRoverWrapper.MoveRoverForward(new RoverPosition(1, 2, Directions.East));

            Assert.Equal(3, newRoverPositionStartFromNorth.YCoord);
            Assert.Equal(1, newRoverPositionStartFromNorth.XCoord);

            Assert.Equal(1, newRoverPositionStartFromSouth.YCoord);
            Assert.Equal(1, newRoverPositionStartFromSouth.XCoord);

            Assert.Equal(2, newRoverPositionStartFromWest.YCoord);
            Assert.Equal(0, newRoverPositionStartFromWest.XCoord);

            Assert.Equal(2, newRoverPositionStartFromEast.YCoord);
            Assert.Equal(2, newRoverPositionStartFromEast.XCoord);
        }

        [Theory]
        [InlineData("lmRM")]
        public void ApplyCommandsToRover_ShouldPass(string roverCommand)
        {
            newMarsRoverWrapper = new MarsRoverWrapper();

            var newPosition = newMarsRoverWrapper.ApplyCommandsToRover(new RoverPosition(1, 2, Directions.North), roverCommand);

            Assert.IsType<RoverPosition>(newPosition);
            Assert.NotNull(newPosition);
            Assert.Equal(0, newPosition.XCoord);
            Assert.Equal(3, newPosition.YCoord);
            Assert.Equal(Directions.North, newPosition.RoverDirection);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("LrMj")]
        public void ApplyCommandsToRover_ShouldFail(string roverCommand)
        {
            newMarsRoverWrapper = new MarsRoverWrapper();

            Assert.Throws<Exception>(() => newMarsRoverWrapper.ApplyCommandsToRover(new RoverPosition(1, 2, Directions.North), roverCommand));
        }


        [Theory]
        [InlineData("5 5", "3 3 E")]
        [InlineData("5 5", "1 2 N")]
        public void ControlIfRoverIsInsidePlaetau_ShouldPass(string plaetauSize, string roverPosition)
        {
            newMarsRoverWrapper = new MarsRoverWrapper();

            var rover = newMarsRoverWrapper.CreateRover(roverPosition);
            var plaetau = newMarsRoverWrapper.SetPlaetau(plaetauSize);

            var result = newMarsRoverWrapper.ControlIfRoverIsInsidePlaetau(rover, plaetau);

            Assert.True(result);
        }

        [Theory]
        [InlineData("3 3", "1 4 S")]
        public void ControlIfRoverIsInsidePlaetau_ShouldFail(string plaetauSize, string roverPosition)
        {
            newMarsRoverWrapper = new MarsRoverWrapper();

            var rover = newMarsRoverWrapper.CreateRover(roverPosition);
            var plaetau = newMarsRoverWrapper.SetPlaetau(plaetauSize);

            var result = newMarsRoverWrapper.ControlIfRoverIsInsidePlaetau(rover, plaetau);

            Assert.False(result);
        }

    }
}
