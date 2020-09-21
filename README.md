# MarsRover.net
An application to control the movements of a Mars rover. Surface considered is 100m x 100m, movable areas are numbered 1 through to 100. The rover:

1. starts its journey facing south.
2. can turn in the directions of left and right moving in metres
3. can take a maximum of 5 commands at any time. 
4. starts in number 1
5. after each set of commands reports back its current position and direction it is facing.


e.g
1. 50m
2. Left
3. 23m
4. Left
5. 4m

The above set of commands would cause the rover to report back position 4624 north. The next set of commands should then continue from this square. 
Please note that the rover cannot go out of this area so will halt all commands when it has reached its perimeter.

Diagram1….
1 2 3 …
101 102 103 …
201 202 203 …
… … … …”
