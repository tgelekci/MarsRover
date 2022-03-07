using MarsRover.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRover.Domain.Classes
{
    public class RoverPosition
    {
        public int XCoord;
        public int YCoord;
        public Directions RoverDirection;

        public RoverPosition()
        {
        }

        public RoverPosition(int x, int y, Directions direction)
        {
            this.XCoord = x;
            this.YCoord = y;
            this.RoverDirection = direction;
        }
    }
}
