using Magicboard.Tikitaki;

Start(new Game());


void Start(Game game)
{

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

        if(res == -1)
        {
            System.Console.WriteLine("Looks like, this cell is captured already");
            continue;
        }
        else if(res == 1)
        {
            break;
        }	

    }
    System.Console.WriteLine("Game is over! Winner is: " + game.CurrentAvatar);
}