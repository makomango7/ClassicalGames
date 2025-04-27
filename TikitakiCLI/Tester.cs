using Magicboard.Tikitaki;

namespace TikitakiCLI
{
    internal static class Tester
    {
        internal static void RunTests()
        {
            var tests = new List<Func<bool>>()
            {
                () => Tester.SimpleTest1(new Game()),
                () => Tester.SimpleTest2(new Game())
            };


            var results = new List<(bool, string)>();

            foreach (var test in tests)
            {
                results.Add((test.Invoke(), test.Method.Name));
            }


            bool res = true;
            foreach (var result in results)
            {
                res &= result.Item1;
                Console.WriteLine($"{result.Item2} : {result.Item1}");
            }

            System.Console.WriteLine("All test result: " + res);
            Thread.Sleep(150);
            Console.Clear();
        }


        static bool SimpleTest1(Game game)
        {
            var inputSequence = new (int x, int y)[]
            {
                (0,0), (1,1), (2,2),
                (1,0), (2,1), (0,1),
                (0,2), (2,0), (1,2)
            };

            TurnState lastState = TurnState.NormalTurn;
            foreach (var item in inputSequence)
            {
                lastState = game.MakeATurn(item.x, item.y);
                BoardDrawer.RedrawBoard(game, false);
                Thread.Sleep(75);
            }
            return lastState == TurnState.GameFinshed;
            /*
             * Под вопросом:
             * Все-таки, игра должна еще сама проверять внутренние условия
             * своего продолжения (условия выполнения MakeATurn)
             */
        }

        static bool SimpleTest2(Game game)
        {
            var inputSequence = new (int x, int y)[]
            {
                (0,0), (1,1), (2,2),
            };

            TurnState lastState = TurnState.NormalTurn;
            foreach (var item in inputSequence)
            {
                lastState = game.MakeATurn(item.x, item.y);
                BoardDrawer.RedrawBoard(game, false);
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

    }
}
