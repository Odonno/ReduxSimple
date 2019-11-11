This example contains 2 elements.

## The main page

```xaml
<ScrollViewer Grid.Row="1" Width="500" Margin="10"
              VerticalScrollBarVisibility="Auto">
    <StackPanel>
        <Grid>
            <Grid.ColumnDefinitions>
                <ColumnDefinition />
                <ColumnDefinition />
                <ColumnDefinition />
            </Grid.ColumnDefinitions>

            <Button x:Name="FilterAllButton" Grid.Column="0" 
                    VerticalAlignment="Stretch" HorizontalAlignment="Stretch">All</Button>
            <Button x:Name="FilterTodoButton" Grid.Column="1"
                    VerticalAlignment="Stretch" HorizontalAlignment="Stretch">Todo</Button>
            <Button x:Name="FilterCompletedButton" Grid.Column="2"
                    VerticalAlignment="Stretch" HorizontalAlignment="Stretch">Completed</Button>
        </Grid>

        <ListView x:Name="TodoItemsListView" 
                  SelectionMode="None">
            <ListView.ItemTemplate>
                <DataTemplate>
                    <local:TodoItemComponent TodoItem="{Binding}" />
                </DataTemplate>
            </ListView.ItemTemplate>

            <ListView.ItemContainerStyle>
                <Style TargetType="ListViewItem">
                    <Setter Property="HorizontalContentAlignment" Value="Stretch"/>
                </Style>
            </ListView.ItemContainerStyle>
        </ListView>

        <Button x:Name="AddNewItemButton" Grid.Column="2"
                VerticalAlignment="Stretch" HorizontalAlignment="Stretch">
            <StackPanel Orientation="Horizontal">
                <TextBlock FontFamily="Segoe MDL2 Assets"
                           Padding="0 2 0 0">
                    &#xE710;
                </TextBlock>
                <TextBlock Margin="8 0 0 0">
                    Add new item
                </TextBlock>
            </StackPanel>
        </Button>
    </StackPanel>
</ScrollViewer>
```

## The Todo Item user control

```xaml
<Grid>
    <Grid.ColumnDefinitions>
        <ColumnDefinition />
        <ColumnDefinition Width="Auto" />
        <ColumnDefinition Width="Auto" />
    </Grid.ColumnDefinitions>

    <TextBox x:Name="TextBox" Grid.Column="0" 
                VerticalAlignment="Stretch" HorizontalAlignment="Stretch"
                FontSize="11"
                Padding="10 7" />

    <Button x:Name="CompleteButton" Grid.Column="1"
            Margin="10 0 0 0"
            VerticalAlignment="Stretch" HorizontalAlignment="Stretch"
            Background="{ThemeResource SystemControlBackgroundChromeMediumBrush}" Foreground="Green"
            Style="{ThemeResource ButtonMainMenuStyle}"
            FontWeight="Bold"
            FontFamily="Segoe MDL2 Assets">
        &#xE8FB;
    </Button>

    <Button x:Name="RevertCompleteButton" Grid.Column="1"
            Visibility="Collapsed"
            Margin="10 0 0 0"
            VerticalAlignment="Stretch" HorizontalAlignment="Stretch"
            Background="{ThemeResource SystemControlBackgroundChromeMediumBrush}" Foreground="Orange"
            Style="{ThemeResource ButtonMainMenuStyle}"
            FontWeight="Bold"
            FontFamily="Segoe MDL2 Assets">
        &#xE7A7;
    </Button>

    <Button x:Name="RemoveButton" Grid.Column="2"
            VerticalAlignment="Stretch" HorizontalAlignment="Stretch"
            Background="{ThemeResource SystemControlBackgroundChromeMediumBrush}" Foreground="Red"
            Style="{ThemeResource ButtonMainMenuStyle}"
            FontWeight="Bold"
            FontFamily="Segoe MDL2 Assets">
        &#xE711;
    </Button>
</Grid>
```