using System;
using System.Collections.Generic;
using System.Linq;

namespace MarsRover
{
    class Program
    {
        static void Main()
        {
            var rover = new Rover();
            var instructions = new List<string>();
            var commands = new List<Func<bool>>();

            Console.WriteLine("Hello Mars! Provide 5 commands one by one. Choose from left/right or <x>m e.g. 50m.");

            // Capture 5 instructions
            for (var i = 0; i < 5; i++)
            {
                instructions.Add(Console.ReadLine());
            }

            // filter for invalid commands
            if (!instructions.All(instruction => !string.IsNullOrWhiteSpace(instruction) &&
                                                (instruction.Trim().ToLower().Equals("left") ||
                                                instruction.Trim().ToLower().Equals("right") ||
                                                instruction.Trim().ToLower().EndsWith('m') &&
                                                int.TryParse(instruction.Trim().Remove(instruction.Length - 1), out _))))
            {
                Console.WriteLine("Invalid command present.");
            }
            else
            {
                // Construct the command list
                foreach (var instruction in instructions)
                {
                    switch(instruction.ToLower())
                    {
                        case "left" :
                            commands.Add(() => rover.TurnLeft());
                            break;
                        case "right":
                            commands.Add(() => rover.TurnRight());
                            break;
                        default:
                            if (instruction.Trim().ToLower().EndsWith('m') &&
                                int.TryParse(instruction.Trim().Remove(instruction.Length - 1), out var units))
                            {
                                var result = units;
                                commands.Add(() => rover.Proceed(result));
                            }
                            break;
                    }
                }

                if (!rover.AddCommands(commands)) return;

                Console.WriteLine($"Performing commands. Rover present at {rover.CurrentPosition} facing {rover.FacingDirection.ToString()}");

                // process command list
                rover.Process();
            }
           
        }
    }
}
