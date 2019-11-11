The TODO list example is a different kind of example than the Tic Tac Toe example. The TODO list example also needs a rich UI and has a lot more user actions (add an item, complete it, remove it, etc..).

## The UI

The UI is divided in 3 blocks:

* A list of filter (All, Todo, Completed) at the top
* The list of todo items (that can be filtered)
* Action to add new items at the bottom

## The logic

Again, the logic is maintained in the Redux Store but this time user can interact with both the Main page and a user control (a single todo item). So you will see that we can dispatch action anywhere in an application.