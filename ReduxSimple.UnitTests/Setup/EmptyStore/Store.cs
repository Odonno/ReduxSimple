namespace ReduxSimple.UnitTests.Setup.EmptyStore
{
    public class StoreWithEmptyState : ReduxStore<EmptyState>
    {
    }

    public class HistoryStoreWithEmptyState : ReduxStoreWithHistory<EmptyState>
    {
    }
}
