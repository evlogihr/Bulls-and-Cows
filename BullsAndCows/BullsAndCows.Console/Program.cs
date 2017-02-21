namespace BullsAndCows.Console
{
    using Core;
    using System;

    class Program
    {
        static void Main()
        {
            while (true)
            {
                Console.WriteLine(Engine.GenerateNumber());
            }

            var res = Engine.EvaluateGuess("1234", "7890");

            Console.WriteLine("bulls: {0}, cows: {1}", res.Bulls, res.Cows);
        }
    }
}
