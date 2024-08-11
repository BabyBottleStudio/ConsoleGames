ConsoleGame_2048

This is my interpretation of the game "2048". It is made to be played in console.
Rules for playing the game:

2048 is a single-player sliding tile puzzle video game written by Italian web developer Gabriele Cirulli and published on GitHub.
(description taken from https://en.wikipedia.org/wiki/2048_(video_game) )

Game is played on a plain 4×4 grid, with numbered tiles that slide when a player moves them using the four arrow keys.
The game begins with two tiles already in the grid, having a value of either 2 or 4, and another such tile appears in a random empty space after each turn.
Tiles slide as far as possible in the chosen direction until they are stopped by either another tile or the edge of the grid.
If two tiles of the same number collide while moving, they will merge into a tile with the total value of the two tiles that collided.
The resulting tile cannot merge with another tile again in the same move.

If a move causes three consecutive tiles of the same value to slide together, only the two tiles farthest along the direction of motion will combine.
If all four spaces in a row or column are filled with tiles of the same value, a move parallel to that row/column will combine the first two and last two.
A scoreboard on the upper-right keeps track of the user's score.
The user's score starts at zero, and is increased whenever two tiles combine.

The game is won when a tile with a value of 2048 appears on the board.
Players can continue beyond that to reach higher scores.
When the player has no legal moves (there are no empty spaces and no adjacent tiles with the same value), the game ends.
