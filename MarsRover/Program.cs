using MarsRover.Domain.Classes;
using MarsRover.Domain.Enums;
using System;

namespace MarsRover
{
    class Program
    {
        static void Main(string[] args)
        {

            //// Plaetau:
            
            Console.Write("Plateau upper-right coordinates:");

            var plaetauSizeInput = Console.ReadLine();

            //// First Rover:

            Console.Write("First Rover's Initial Position:");

            var firstRoverPosInput = Console.ReadLine();

            Console.Write("First Rover's Exploration Command:");

            var firstRoverCommandInput = Console.ReadLine();

            MarsRoverWrapper marsRoverWrapper = new MarsRoverWrapper(plaetauSizeInput, firstRoverPosInput, firstRoverCommandInput);

            var resultForFirstRover = marsRoverWrapper.ApplySimulation();

            Console.WriteLine("First Rover's Result: " + resultForFirstRover.XCoord + " " + resultForFirstRover.YCoord + " " + resultForFirstRover.RoverDirection);

            //// Second Rover:

            Console.Write("Second Rover's Initial Position:");

            var secondRoverPosInput = Console.ReadLine();

            Console.Write("Second Rover's Exploration Command:");

            var secondRoverCommandInput = Console.ReadLine();

            marsRoverWrapper = new MarsRoverWrapper(plaetauSizeInput, secondRoverPosInput, secondRoverCommandInput);

            var resultForSecondRover = marsRoverWrapper.ApplySimulation();

            Console.WriteLine("Second Rover's Result: " + resultForSecondRover.XCoord + " " + resultForSecondRover.YCoord + " " + resultForSecondRover.RoverDirection);

            Console.ReadKey();


        }
    }
}
