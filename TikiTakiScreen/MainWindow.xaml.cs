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

        Grid myGrid = new Grid();
        myGrid.Width = 150;
        myGrid.Height = 150;
        myGrid.ShowGridLines = true;
        myGrid.HorizontalAlignment = HorizontalAlignment.Stretch;
        myGrid.VerticalAlignment= VerticalAlignment.Stretch;


        for(int i = 0; i < game.Size; i++)
        {
            var newColDef = new ColumnDefinition();
            newColDef.Width = new GridLength(50);
            newColDef.MinWidth = 50;
            newColDef.MaxWidth = 50;
            myGrid.ColumnDefinitions.Add(newColDef);
        }

        for(int i = 0; i < game.Size; i++)
        {
            var newRowDef = new RowDefinition();
            newRowDef.Height = new GridLength(50);
            newRowDef.MinHeight = 50;
            newRowDef.MaxHeight = 50;
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