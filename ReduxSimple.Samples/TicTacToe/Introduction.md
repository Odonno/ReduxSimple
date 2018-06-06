The Tic Tac Toe example is more complex example than the Counter example. This game needs a  richer UI and a deeper logic.

## The UI

THe UI is divided in 2 parts:

* the left part - game information
* the right part - game board with a 3x3 cells

## The logic

Each time a user select a cell, we check if the player won then if the bot won. Or if no cell is available, the game end with a tie.

The logic is contained in the Store when the `PlayAction` is reduced.