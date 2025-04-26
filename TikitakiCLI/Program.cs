using Magicboard.Tikitaki;

bool isDebug = false;

RunTests();



void RunTests()
{

    var tests = new List<Func<bool>>()
    {
        () => SimpleTest1(new Game()),
        () => SimpleTest2(new Game())
    };


    var results = new List<(bool, string)>();

    foreach(var test in tests)
    {
        results.Add((test.Invoke(), test.Method.Name));
    }


    bool res = true;
    foreach(var result in results)
    {
        res &= result.Item1;
        Console.WriteLine($"{result.Item2} : {result.Item1}");    
    }
       
    System.Console.WriteLine("All test result: " + res);
}


bool SimpleTest1(Game game)
{
    var inputSequence = new (int x, int y)[]
    {
        (0,0), (1,1), (2,2),
        (1,0), (2,1), (0,1),
        (0,2), (2,0), (1,2)
    };
    
    TurnState lastState = TurnState.NormalTurn;
    foreach(var item in inputSequence)
    {
        lastState = game.MakeATurn(item.x, item.y);
        RedrawBoard(game);
        Thread.Sleep(75);
    }
    return lastState == TurnState.GameFinshed;
    /*
     * Под вопросом:
     * Все-таки, игра должна еще сама проверять внутренние условия
     * своего продолжения (условия выполнения MakeATurn)
     */
}

bool SimpleTest2(Game game)
{
    var inputSequence = new (int x, int y)[]
    {
        (0,0), (1,1), (2,2),
    };
    
    TurnState lastState = TurnState.NormalTurn;
    foreach(var item in inputSequence)
    {
        lastState = game.MakeATurn(item.x, item.y);
        RedrawBoard(game);
        Thread.Sleep(75);
    }
    return lastState == TurnState.NormalTurn;
}

//bool MakeATurnAndDraw(Game game, int x, int y)
//{
//    // Вот здесь я уже не знаю что сказать после знака сравнения
//    // Поэтому нумерации долж
//    // game.MakeATurn(x, y) == ...

//    if(game.MakeATurn(x, y) == TurnState.GameFinshed)
//    {
//        return false;
//    }

//    RedrawBoard(game);
//    Thread.Sleep(150);

//    return true;
//}
    

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

        TurnState res = game.MakeATurn(x, y);

        RedrawBoard(game);

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

