using MarsRover.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRover.Domain.Classes
{
    public class MarsRoverWrapper
    {
        public string plaetauSizeInput;
        public string roverPositionInput;
        public string roverCommandInput;

        public MarsRoverWrapper()
        {
        }

        public MarsRoverWrapper(string plaetauSizeInput, string roverPositionInput, string roverCommandInput)
        {
            this.plaetauSizeInput = plaetauSizeInput;
            this.roverPositionInput = roverPositionInput;
            this.roverCommandInput = roverCommandInput;
        }

        public Plaetau SetPlaetau(string plaetauSizeInput)
        {
            Plaetau plaetau = new Plaetau();

            if (!string.IsNullOrWhiteSpace(plaetauSizeInput))
            {
                var gridSizeArr = plaetauSizeInput.Split(' ');

                if (gridSizeArr.Length == 2)                  
                {
                    if (int.TryParse(gridSizeArr[0], out int x) && int.TryParse(gridSizeArr[1], out int y))
                        plaetau = new Plaetau(x, y);
                    else
                        throw new Exception("At least one plaetau size coordinate data is not a number");
                }
                else
                {
                    throw new Exception("Plaetau size input length is invalid");
                }
            }
            else
            {
                throw new Exception("Plaetau size input is invalid");
            }

            return plaetau;

        }

        public Rover CreateRover(string roverPositionInput)
        {
            Rover rover = new Rover();

            if (!string.IsNullOrWhiteSpace(roverPositionInput))
            {
                var roverPositionArr = roverPositionInput.Split(' ');
                RoverPosition roverPosition = new RoverPosition();

                if (roverPositionArr.Length == 3)
                {
                    switch (roverPositionArr[2].ToUpper())
                    {
                        case "N":
                            roverPosition.RoverDirection = Directions.North;
                            break;
                        case "S":
                            roverPosition.RoverDirection = Directions.South;
                            break;
                        case "W":
                            roverPosition.RoverDirection = Directions.West;
                            break;
                        case "E":
                            roverPosition.RoverDirection = Directions.East;
                            break;
                        default:
                            throw new Exception("Invalid rover direction letter");
                    }

                    if (int.TryParse(roverPositionArr[0], out int x) && int.TryParse(roverPositionArr[1], out int y))
                    {
                        roverPosition.XCoord = x;
                        roverPosition.YCoord = y;
                    }
                    else
                        throw new Exception("Invalid rover coordinate numbers");

                    rover = new Rover(roverPosition);

                }
                else
                    throw new Exception("Rover position input length is invalid");

            }
            else
            {
                throw new Exception("Rover position input is invalid");
            }

            return rover;
        }

        public RoverPosition TurnRoverLeft(RoverPosition roverPosition)
        {
            switch (roverPosition.RoverDirection)
            {
                case Directions.West:
                    roverPosition.RoverDirection = Directions.South;
                    break;
                case Directions.South:
                    roverPosition.RoverDirection = Directions.East;
                    break;
                case Directions.North:
                    roverPosition.RoverDirection = Directions.West;
                    break;
                case Directions.East:
                    roverPosition.RoverDirection = Directions.North;
                    break;
                default:
                    throw new Exception("Invalid direction data");
            }

            return roverPosition;
        }

        public RoverPosition TurnRoverRight(RoverPosition roverPosition)
        {
            switch (roverPosition.RoverDirection)
            {
                case Directions.West:
                    roverPosition.RoverDirection = Directions.North;
                    break;
                case Directions.South:
                    roverPosition.RoverDirection = Directions.West;
                    break;
                case Directions.North:
                    roverPosition.RoverDirection = Directions.East;
                    break;
                case Directions.East:
                    roverPosition.RoverDirection = Directions.South;
                    break;
                default:
                    throw new Exception("Invalid direction data");
            }

            return roverPosition;
        }

        public RoverPosition MoveRoverForward(RoverPosition roverPosition)
        {
            switch (roverPosition.RoverDirection)
            {
                case Directions.West:
                    roverPosition.XCoord--;
                    break;
                case Directions.South:
                    roverPosition.YCoord--;
                    break;
                case Directions.North:
                    roverPosition.YCoord++;
                    break;
                case Directions.East:
                    roverPosition.XCoord++;
                    break;
                default:
                    throw new Exception("Invalid direction data");
            }

            return roverPosition;
        }

        public RoverPosition ApplyCommandsToRover(RoverPosition roverPosition, string roverCommandInput)
        {
            if (!string.IsNullOrWhiteSpace(roverCommandInput))
            {
                char[] roverCommandInputArr = roverCommandInput.ToCharArray();

                foreach (var command in roverCommandInputArr)
                {
                    {
                        switch (char.ToUpper(command))
                        {
                            case 'L':
                                TurnRoverLeft(roverPosition);
                                break;

                            case 'R':
                                TurnRoverRight(roverPosition);
                                break;

                            case 'M':
                                MoveRoverForward(roverPosition);
                                break;

                            default:
                                throw new Exception("Invalid command data");
                        }
                    }
                }
            }
            else
                throw new Exception("Invalid command input");

            return roverPosition;
        }

        public bool ControlIfRoverIsInsidePlaetau(Rover rover, Plaetau plaetau)
        {

            if (rover.Position.XCoord > plaetau.PlaetauSize.MaxXCoord || rover.Position.XCoord < 0  || rover.Position.YCoord > plaetau.PlaetauSize.MaxYCoord || rover.Position.YCoord < 0)
                return false;

            return true;
        }

        public RoverPosition ApplySimulation()
        {
            Plaetau currentPlaetau = SetPlaetau(this.plaetauSizeInput);
            Rover currentRover = CreateRover(this.roverPositionInput);

            ApplyCommandsToRover(currentRover.Position, this.roverCommandInput);

            if (!ControlIfRoverIsInsidePlaetau(currentRover, currentPlaetau))
                throw new Exception("Rover moved outside of the plaetau");

            return currentRover.Position;
        }
    }
}
