```xaml
<ProgressRing x:Name="GlobalLoadingProgressRing" 
                Grid.Row="1" 
                Width="50" Height="50"
                IsActive="True" />

<Grid x:Name="RootStackPanel" 
      Grid.Row="1"
      Visibility="Collapsed"
      Margin="40">
    <Grid.RowDefinitions>
        <RowDefinition Height="Auto" />
        <RowDefinition />
    </Grid.RowDefinitions>
    
    <AutoSuggestBox x:Name="AutoSuggestBox"
                    Grid.Row="0"
                    MaxWidth="300" MaxSuggestionListHeight="150">
        <AutoSuggestBox.ItemTemplate>
            <DataTemplate>
                <StackPanel Orientation="Horizontal">
                    <TextBlock FontWeight="Bold">
                        <Run Text="#" /><Run Text="{Binding Id}" />
                    </TextBlock>
                    <TextBlock Text="{Binding Name}" Margin="10 0 0 0" />
                </StackPanel>
            </DataTemplate>
        </AutoSuggestBox.ItemTemplate>
    </AutoSuggestBox>

    <controls:DropShadowPanel x:Name="PokemonPanel" 
                              Grid.Row="1" 
                              BlurRadius="20"
                              ShadowOpacity="0.6"
                              Color="Black"
                              VerticalAlignment="Center" HorizontalAlignment="Center"
                              Margin="0 40 0 0">
        <Grid Background="White"
              VerticalAlignment="Center" HorizontalAlignment="Center"
              Width="200" Height="250">
            <Grid.RowDefinitions>
                <RowDefinition Height="Auto" />
                <RowDefinition Height="Auto" />
                <RowDefinition />
            </Grid.RowDefinitions>
            
            <TextBlock x:Name="PokemonIdTextBlock"
                       Grid.Row="0"
                       Margin="0 20 0 0"
                       FontSize="18" FontWeight="Bold"
                       VerticalAlignment="Center" HorizontalAlignment="Center" />

            <TextBlock x:Name="PokemonNameTextBlock"
                       Grid.Row="1"
                       Margin="0 5 0 0"
                       VerticalAlignment="Center" HorizontalAlignment="Center" />

            <Image x:Name="PokemonImage"
                   Grid.Row="2"
                   Margin="0 10 0 0"
                   Stretch="Uniform"
                   VerticalAlignment="Center" HorizontalAlignment="Center" />
        </Grid>
    </controls:DropShadowPanel>
</Grid>
```