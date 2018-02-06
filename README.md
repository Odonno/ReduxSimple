# Redux Simple

[![NuGet](https://img.shields.io/nuget/v/ReduxSimple.svg)](https://www.nuget.org/packages/ReduxSimple/)

> Simple Stupid Redux Store using Reactive Extensions

Redux Simple is a .NET library based on [Redux](https://redux.js.org/) principle. Redux Simple is written with Rx.NET and built with the minimum of code you need to scale your whatever .NET application you want to design.

## Getting started

Like the original Redux library, you will have to initialize a new `State` when creating a `Store` + you will create `Reduce` functions each linked to an `Action` which will possibly update this `State`. 

In your app, you can:

* `Dispatch` new `Action` to change the `State` 
* and listen to events/changes using the `Subscribe` method

We will go through using an example.

### The default State

Each State should immutable. That's why we prefer to use immutable types for each property of the State.

```csharp
public class AppState
{
    public string CurrentPage { get; set; } = string.Empty;
    public ImmutableArray<string> Pages { get; set; } = ImmutableArray<string>.Empty;
}
```

### A simple Store

You will need 3 steps to create your own Redux Store:

1. Create `Actions` used in the Reduce function

```csharp
public class NavigateAction
{
    public string PageName { get; set; }
}

public class GoBackAction { }

public class ResetAction { }
```

2. Create a new Store class inherited from `ReduxStore`

```csharp
public sealed class MyAppStore : ReduxStore<AppState>
{
    public override AppState Reduce(AppState state, object action)
    {
        switch (action)
        {
            case NavigateAction navigateAction:
                return Reduce(state, navigateAction);
            case GoBackAction goBackAction:
                return Reduce(state, goBackAction);
            case ResetAction resetAction:
                return Reduce(state, resetAction);
        }

        return base.Reduce(state, action);
    }

    private static AppState Reduce(AppState state, NavigateAction action)
    {
        return new AppState
        {
            CurrentPage = action.PageName,
            Pages = state.Pages.Add(action.PageName)
        };
    }
    private static AppState Reduce(AppState state, GoBackAction action)
    {
        var newPages = state.Pages.RemoveAt(state.Pages.Length - 1);

        return new AppState
        {
            CurrentPage = newPages.LastOrDefault(),
            Pages = newPages
        };
    }
    private static AppState Reduce(AppState state, ResetAction action)
    {
        return new AppState
        {
            CurrentPage = string.Empty,
            Pages = ImmutableArray<string>.Empty
        };
    }
}
```

3. Create a new instance of your Store

```csharp
sealed partial class App
{
    public static readonly MyAppStore Store;

    static App()
    {
        Store = new MyAppStore();
    }
}
```

### Dispatch & Subscribe

You can now dispatch new actions using your globally accessible `Store`.

```csharp
using static MyApp.App; // static reference on top of your file

Store.Dispatch(new NavigateAction { PageName = "Page1" });
Store.Dispatch(new NavigateAction { PageName = "Page2" });
Store.Dispatch(new GoBackAction());
```

And subscribe to either state changes or actions raised.

```csharp
using static MyApp.App; // static reference on top of your file

Store.ObserveAction<NavigateAction>().Subscribe(_ =>
{
    // TODO : Handle navigation
});

Store.ObserveState()
    .Where(state => state.CurrentPage == nameof(Page1))
    .Subscribe(_ =>
    {
        // TODO : Handle event when the current page is now "Page1"
    });
```

### Asynchronous Actions

When you work with asynchronous tasks (side effects), you can follow the following rule:

* Create 3 actions - a normal/start action, a `fulfilled` action and a `failed` action
* Reduce/Handle response on `fulfilled` action
* Reduce/Handle error on `failed` action

Here is a concrete example.

#### List of actions

```csharp
public class GetTodosAction { }
public class GetTodosFulfilledAction
{
    public ImmutableArray<Todo> Todos { get; set; }
}
public class GetTodosFailedAction
{
    public int StatusCode { get; set; }
    public string Reason { get; set; }
}
```

```csharp
Store.Dispatch(new GetTodosAction());
```

#### Reduce functions

```csharp
private static AppState Reduce(AppState state, GetTodosAction action)
{
    return new AppState
    {
        Loading = true,
        Todos = state.Todos
    };
}
private static AppState Reduce(AppState state, GetTodosFulfilledAction action)
{
    return new AppState
    {
        Loading = false,
        Todos = action.Todos.ToImmutableArray()
    };
}
private static AppState Reduce(AppState state, GetTodosFailedAction action)
{
    return new AppState
    {
        Loading = false,
        Todos = ImmutableArray<Todo>.Empty
    };
}
```
