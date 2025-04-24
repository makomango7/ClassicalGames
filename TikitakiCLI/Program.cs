using Magicboard.Tikitaki;

bool isDebug = false;

SimpleTest1(new Game());
//Start(new Game());


void SimpleTest1(Game game)
{
    game.MakeATurn(0,0);
    RedrawBoard(game);
    Thread.Sleep(150);

    game.MakeATurn(1,1);
    RedrawBoard(game);
    Thread.Sleep(150);

    game.MakeATurn(2,2);
    RedrawBoard(game);
    Thread.Sleep(150);

    game.MakeATurn(1,0);
    RedrawBoard(game);
    Thread.Sleep(150);
    
    game.MakeATurn(2,1);
    RedrawBoard(game);
    Thread.Sleep(150);

    game.MakeATurn(0,1);
    RedrawBoard(game);
    Thread.Sleep(150);


    game.MakeATurn(0,2);
    RedrawBoard(game);
    Thread.Sleep(150);
    
    game.MakeATurn(2,0);
    RedrawBoard(game);
    Thread.Sleep(150);

    game.MakeATurn(1,2);
    RedrawBoard(game);
    Thread.Sleep(150);

    /*
     * Все-таки, игра должна еще сама проверять внутренние условия
     * своего продолжения (условия выполнения MakeATurn)
     */
}
    

void Start(Game game)
{
    RedrawBoard(game);
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

        int res = game.MakeATurn(x, y);

        RedrawBoard(game);

        // cell is captured already
        if(res == -1) 
        {
            System.Console.WriteLine("Looks like, this cell is captured already");
            continue;
        }
        // somebody won the game
        else if(res == 1) 
        {
            // finaly, we exit from input loop
            break;
        }

    }
    
    string winnerStr = game.CurrentAvatar == '-' ? "nobody" : game.CurrentAvatar.ToString(); 
    System.Console.WriteLine("Game is over! Winner is: " + winnerStr);
}

void RedrawBoard(Game game)
{
    if(!isDebug)
        Console.Clear();
    for(int i = 0; i < game.Arr.Length; i++)
    {
        if(i != 0 && i % game.Size == 0)
        {
            Console.WriteLine();
        }
        Console.Write(game.Arr[i] + " ");
    }
    Console.WriteLine();
    Console.WriteLine();
}

