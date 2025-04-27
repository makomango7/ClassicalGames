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
       
        int size = 4;      
        int cellSize = 50;

        Grid myGrid = new Grid();
        myGrid.Width = size * cellSize;
        myGrid.Height = size * cellSize;
        myGrid.ShowGridLines = true;
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
            button.Content = "-";
            Grid.SetRow(button, CommonExtensions.GetXFromIndex(i, size));
            Grid.SetColumn(button, CommonExtensions.GetYFromIndex(i, size));
            myGrid.Children.Add(button);
        }

        this.Content = myGrid;
    }
}