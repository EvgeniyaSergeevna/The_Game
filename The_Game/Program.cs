using System;



namespace The_Game
{
    class Program
    {
        static void Main(string[] args)
        {
            Rules currentRule = new Rules(args);
            if (Rules.CheckArguments())
            {
                Rules.Menu();
                while (!Rules.exit)
                {
                    currentRule.InputPCData();
                    currentRule.InputUserData();
                    currentRule.ShowAllCoices();
                    currentRule.WhoWins();
                }
            }
        }
    }
}
