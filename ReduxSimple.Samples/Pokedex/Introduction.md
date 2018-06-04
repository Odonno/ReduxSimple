The Pokedex example is another complex with a relatively simple UI and logic to focus on side effects. Side effects can be be of different form but the more usual in any kind of application is when the app calls a web server like an HTTP request.

## The UI

The UI is divided in 3 parts:

* A search bar so you can search a pokemon by its name or its id
* A card with information on the pokemon you searched
* And some notification when error occured (if you cannot reach the server for example)

Of course, because we can listen to the UI changes with observable, we can call the API as soon as you type something in the search bar.

## The logic

The logic is basically the same as you seen previously. The main difference is that we have more actions to treat side effect:

* `XAction` is the typical action triggered to start an asynchronous task (like an API call)
* `XFulfilledAction` is the action triggered when there is a success (JSON response)
* `XFailedAction` is the action triggered when something went wrong during the asynchronous task