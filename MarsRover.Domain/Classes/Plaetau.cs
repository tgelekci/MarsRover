using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRover.Domain.Classes
{
    public class Plaetau
    {
        public string PlaetauId;
        public PlaetauGridSize PlaetauSize;

        public Plaetau()
        {
            //this.PlaetauId = "plaetau" + Guid.NewGuid().ToString("N").Substring(0, 5);
        }

        public Plaetau(int width, int height)
        {
            this.PlaetauSize = new PlaetauGridSize(width, height);
            this.PlaetauId = "plaetau"+Guid.NewGuid().ToString("N").Substring(0, 5);
        }

    }
}
