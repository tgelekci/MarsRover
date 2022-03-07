using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRover.Domain.Classes
{
    public class PlaetauGridSize
    {
        public int MaxXCoord;
        public int MaxYCoord;
        public PlaetauGridSize()
        {
        }

        public PlaetauGridSize(int maxX, int maxY)
        {
            this.MaxXCoord = maxX;
            this.MaxYCoord = maxY;
        }
    }
}
