using Magicboard.Tikitaki;
using TikitakiCLI;



Tester.RunTests();

Start(new Game());




void Start(Game game)
{
    var drawer = new BoardDrawer(BoardDrawer.Set3);
    drawer.RedrawSquareBoard(game, false);
    Console.WriteLine("Type exit to exit");
    while(true)
    {
        Console.WriteLine("Enter next turn: ");
        string inputLine = Console.ReadLine();
        if(inputLine == "exit")
        {
            break;
        }

        string[] results = inputLine.Split(',');
        
        if(results.Length != 2)
        {
            System.Console.WriteLine("Wrong input");
            continue;
        }
        int x = -1;                
        int y = -1;

        if(!int.TryParse(results[0], out x))
        {
            Console.WriteLine("Wrong input!");
            continue;
        }
        if(!int.TryParse(results[1], out y))
        {
            Console.WriteLine("Wrong input!");
            continue;
        }
        if(x < 0 || x >= game.Size || y < 0 || y >= game.Size)
        {
            System.Console.WriteLine("Your value are out of cell size");
            continue;
        }	

        TurnState res = game.MakeATurn(x, y);

        drawer.RedrawSquareBoard(game, false);

        // cell is captured already
        if(res == TurnState.WrongInput) 
        {
            System.Console.WriteLine("Looks like, this cell is captured already");
            continue;
        }
        // somebody won the game
        else if(res == TurnState.GameFinshed) 
        {
            // finaly, we exit from input loop
            break;
        }

    }
    
    string winnerStr = game.CurrentPlayer == Game.PlayerNULL? "nobody" : drawer.PlayerToAvatar(game.CurrentPlayer).ToString(); 
    System.Console.WriteLine("Game is over! Winner is: " + winnerStr);
}

