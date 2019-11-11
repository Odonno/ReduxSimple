namespace ReduxSimple.Uwp.Samples.Counter
{
    public class CounterState
    {
        public int Count { get; set; }

        public static CounterState InitialState => 
            new CounterState
            {
                Count = 0
            };
    }
}
