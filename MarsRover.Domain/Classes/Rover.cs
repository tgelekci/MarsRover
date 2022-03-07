using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRover.Domain.Classes
{
    public class Rover
    {
        public string RoverId;
        public RoverPosition Position;

        public Rover()
        {
            //this.RoverId = "rover" + Guid.NewGuid().ToString("N").Substring(0, 5);
        }

        public Rover(RoverPosition position)
        {
            this.Position = position;
            this.RoverId = "rover" + Guid.NewGuid().ToString("N").Substring(0, 5);
        }
    }
}
