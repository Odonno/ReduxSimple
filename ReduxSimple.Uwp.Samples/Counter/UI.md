```xaml
<StackPanel VerticalAlignment="Center" HorizontalAlignment="Center">
    <TextBlock x:Name="CounterValueTextBlock" 
               FontSize="42"
               TextAlignment="Center" />

    <StackPanel Orientation="Horizontal"
                Margin="0 10 0 0">
        <Button x:Name="IncrementButton"
                Margin="0 0 15 0"
                Padding="0 -8 0 0"
                Width="50" Height="50"
                FontSize="38">
            +
        </Button>

        <Button x:Name="DecrementButton"
                Margin="15 0 0 0"
                Padding="0 -8 0 0"
                Width="50" Height="50"
                FontSize="38">
            -
        </Button>
    </StackPanel>
</StackPanel>
```