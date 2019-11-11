```xaml
<Grid>
    <Grid.ColumnDefinitions>
        <ColumnDefinition Width="*" />
        <ColumnDefinition Width="2*" />
    </Grid.ColumnDefinitions>

    <StackPanel Grid.Column="0" 
                Width="200"
                VerticalAlignment="Center" HorizontalAlignment="Right">
        <TextBlock x:Name="YourTurnTextBlock" Margin="0 10 0 0" TextAlignment="Center">
            Your turn!
        </TextBlock>

        <Button x:Name="StartNewGameButton"
                HorizontalAlignment="Center"
                Margin="0 10 0 10">
            <StackPanel Orientation="Horizontal">
                <TextBlock FontFamily="Segoe MDL2 Assets" Text="&#xE72C;"
                            VerticalAlignment="Center" />
                <TextBlock Text="Start a new game" Margin="10 0 0 0" />
            </StackPanel>
        </Button>

        <TextBlock x:Name="EndGameTextBlock" TextAlignment="Center">
            :) You won!
        </TextBlock>
    </StackPanel>
    
    <Grid x:Name="CellsRootGrid" 
            Grid.Column="1"
            Height="300" Width="300"
            VerticalAlignment="Center" HorizontalAlignment="Center">
        <Grid.RowDefinitions>
            <RowDefinition MinHeight="100" />
            <RowDefinition MinHeight="100" />
            <RowDefinition MinHeight="100" />
        </Grid.RowDefinitions>

        <Grid.ColumnDefinitions>
            <ColumnDefinition MinWidth="100" />
            <ColumnDefinition MinWidth="100" />
            <ColumnDefinition MinWidth="100" />
        </Grid.ColumnDefinitions>

        <Grid Grid.Row="0" Grid.Column="0" 
                Padding="5"
                Background="Transparent"
                BorderBrush="Gray" BorderThickness="0 0 0 5">
            <TextBlock Text="" FontSize="66"
                        VerticalAlignment="Center" HorizontalAlignment="Center" />
        </Grid>

        <Grid Grid.Row="0" Grid.Column="1"
                Padding="5"
                Background="Transparent"
                BorderBrush="Gray" BorderThickness="5 0 5 5">
            <TextBlock Text="" FontSize="66"
                        VerticalAlignment="Center" HorizontalAlignment="Center" />
        </Grid>

        <Grid Grid.Row="0" Grid.Column="2"
                Padding="5"
                Background="Transparent"
                BorderBrush="Gray" BorderThickness="0 0 0 5">
            <TextBlock Text="" FontSize="66"
                        VerticalAlignment="Center" HorizontalAlignment="Center" />
        </Grid>

        <Grid Grid.Row="1" Grid.Column="0"
                Padding="5"
                Background="Transparent"
                BorderBrush="Gray" BorderThickness="0 0 0 5">
            <TextBlock Text="" FontSize="66"
                        VerticalAlignment="Center" HorizontalAlignment="Center" />
        </Grid>

        <Grid Grid.Row="1" Grid.Column="1"
                Padding="5"
                Background="Transparent"
                BorderBrush="Gray" BorderThickness="5 0 5 5">
            <TextBlock Text="" FontSize="66"
                        VerticalAlignment="Center" HorizontalAlignment="Center" />
        </Grid>

        <Grid Grid.Row="1" Grid.Column="2"
                Padding="5"
                Background="Transparent"
                BorderBrush="Gray" BorderThickness="0 0 0 5">
            <TextBlock Text="" FontSize="66"
                        VerticalAlignment="Center" HorizontalAlignment="Center" />
        </Grid>

        <Grid Grid.Row="2" Grid.Column="0"
                Padding="5"
                Background="Transparent"
                BorderBrush="Gray" BorderThickness="0 0 0 0">
            <TextBlock Text="" FontSize="66"
                        VerticalAlignment="Center" HorizontalAlignment="Center" />
        </Grid>

        <Grid Grid.Row="2" Grid.Column="1"
                Padding="5"
                Background="Transparent"
                BorderBrush="Gray" BorderThickness="5 0 5 0">
            <TextBlock Text="" FontSize="66"
                        VerticalAlignment="Center" HorizontalAlignment="Center" />
        </Grid>

        <Grid Grid.Row="2" Grid.Column="2"
                Padding="5"
                Background="Transparent"
                BorderBrush="Gray" BorderThickness="0 0 0 0">
            <TextBlock Text="" FontSize="66"
                        VerticalAlignment="Center" HorizontalAlignment="Center" />
        </Grid>
    </Grid>
</Grid>
```