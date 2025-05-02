using System.Windows;
using System.Windows.Controls;
using Magicboard.Common;
using Magicboard.Tikitaki;

namespace TikiTaki;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    Game game;  

    public MainWindow()
    {
        InitializeComponent();

        game = new Game();
       
        int size = 3;      
        int cellSize = 50;

        Grid myGrid = new Grid();
        myGrid.Width = size * cellSize;
        myGrid.Height = size * cellSize;
        myGrid.ShowGridLines = true;
        myGrid.Name = "windowGrid";
        RegisterName("windowGrid", myGrid);
        myGrid.HorizontalAlignment = HorizontalAlignment.Stretch;
        myGrid.VerticalAlignment= VerticalAlignment.Stretch;


        for(int i = 0; i < size; i++)
        {
            var newColDef = new ColumnDefinition();
            newColDef.Width = new GridLength(cellSize);
            newColDef.MinWidth = cellSize;
            newColDef.MaxWidth = cellSize;
            myGrid.ColumnDefinitions.Add(newColDef);
        }

        for(int i = 0; i < size; i++)
        {
            var newRowDef = new RowDefinition();
            newRowDef.Height = new GridLength(cellSize);
            newRowDef.MinHeight = cellSize;
            newRowDef.MaxHeight = cellSize;
            myGrid.RowDefinitions.Add(newRowDef);
        }

        for(int i = 0; i < size * size; i++)
        {
            Button button = new Button();
            button.Click += Button_Click;
            button.Content = "-";
            Grid.SetRow(button, CommonExtensions.GetXFromIndex(i, size));
            Grid.SetColumn(button, CommonExtensions.GetYFromIndex(i, size));
            myGrid.Children.Add(button);
        }

        mainStack.Children.Add(myGrid);
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        if(game.IsFinished)
        {
            return;
        }
        
        int x = Grid.GetColumn(sender as Button);
        int y = Grid.GetRow(sender as Button);
        var res = game.MakeATurn(x, y);

        txtStatus.Text = "Status: " + res.ToString(); 

        if(res != TurnState.WrongInput)
        {
            UpdateCells();
        }
    }

    public void UpdateCells()
    {
        var griddie = this.FindName("windowGrid") as Grid;

        for (int i = 0; i < game.Size * game.Size; i++)
        {
            var x = CommonExtensions.GetXFromIndex(i, game.Size);
            var y = CommonExtensions.GetYFromIndex(i, game.Size);

            var child = griddie.Children
                .Cast<UIElement>()
                .First(elem => Grid.GetRow(elem) == x && Grid.GetColumn(elem) == y) as Button;

            child.Content = game.Arr[i];
        }
    }

}