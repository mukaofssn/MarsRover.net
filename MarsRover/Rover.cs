using System;
using System.Collections.Generic;
using System.Linq;

namespace MarsRover
{
    public class Rover
    {
        private int _currentX = 1;
        private int _currentY;

        public int CurrentPosition => _currentY * (Constants.YMaxLimit + 1) + _currentX;
        public Direction FacingDirection { get; private set; } = Direction.South;


        private readonly List<Func<bool>> _commands = new List<Func<bool>>();

        public void Process()
        {
            var index = 1;
            foreach (var result in _commands.Select(command => command.Invoke()))
            {
                if (!result)
                {
                    Console.WriteLine($"Halted at step {index}. Rover at {CurrentPosition} facing {FacingDirection.ToString()}.");
                    return;
                }

                Console.WriteLine($"Rover at {CurrentPosition} facing {FacingDirection.ToString()}.");
                index++;
            }

            Console.WriteLine($"Commands completed. Final position at {CurrentPosition} facing {FacingDirection.ToString()}.");
        }

        public bool AddCommands(IEnumerable<Func<bool>> commands)
        {
            if (_commands.Count == 5) return false;

            _commands.AddRange(commands);
           return true;
        }

        public bool TurnLeft()
        {
            Console.WriteLine($"Turning left.");
            return PerformTurn(TurnTo.Left);
        }

        public bool TurnRight()
        {
            Console.WriteLine($"Turning right.");
            return PerformTurn(TurnTo.Right);
        }

        private bool PerformTurn(TurnTo turnTo)
        {
            FacingDirection = GetNewDirection(turnTo);
            return true;
        }

        public bool Proceed(int unitsToMove)
        {
            int stepsMoved;

            // calculate steps to move based on input
            (_currentX, _currentY, stepsMoved) = CalculateMove(unitsToMove);
            Console.WriteLine($"Forward {stepsMoved} steps/{unitsToMove} steps");
            return !ShouldRoverHalt();
        }

        // Get new direction from current direction and turn instruction
        private Direction GetNewDirection(TurnTo turnTo)
        {
            return FacingDirection switch
            {
                Direction.South => turnTo == TurnTo.Left ? Direction.East : Direction.West,
                Direction.East => turnTo == TurnTo.Left ? Direction.North : Direction.South,
                Direction.West => turnTo == TurnTo.Left ? Direction.South : Direction.North,
                Direction.North => turnTo == TurnTo.Left ? Direction.West : Direction.East,
                _ => Direction.South
            };
        }

        // Check and return new coordinates to move to taking the bounds into consideration
        private (int, int, int) CalculateMove(int unitsToMove)
        {
            return FacingDirection switch
            {
                Direction.South => _currentY + unitsToMove > Constants.YMaxLimit ? (_currentX, Constants.YMaxLimit, Constants.YMaxLimit - _currentY) : (_currentX, _currentY + unitsToMove, unitsToMove),
                Direction.East => _currentX + unitsToMove > Constants.XMaxLimit ? (Constants.XMaxLimit, _currentY, Constants.XMaxLimit - _currentX) : (_currentX + unitsToMove, _currentY, unitsToMove),
                Direction.West => _currentX - unitsToMove < Constants.XMinLimit ? (Constants.XMinLimit, _currentY, _currentX - Constants.XMinLimit) : (_currentX - unitsToMove, _currentY, unitsToMove),
                Direction.North => _currentY - unitsToMove < Constants.YMinLimit ? (_currentX, Constants.YMinLimit, _currentY - Constants.YMinLimit) : (_currentX, _currentY - unitsToMove, unitsToMove),
                _ => (_currentX, _currentY, 0)
            };
        }

        // Bounds check
        private bool ShouldRoverHalt()
        {
            return FacingDirection switch
            {
                Direction.South => _currentY == Constants.YMaxLimit,
                Direction.East => _currentX == Constants.XMaxLimit,
                Direction.West => _currentX == Constants.XMinLimit,
                Direction.North => _currentY == Constants.YMinLimit,
                _ => false
            };
        }
    }
}