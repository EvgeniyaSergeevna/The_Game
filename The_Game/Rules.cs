using System;
using System.Collections.Generic;
using System.Linq;

namespace The_Game
{
    class Rules
    {
        internal static string[] usersArgs;     
        internal static int userChoice, pcChoice;
        internal static bool exit = false;
        internal static string key;
        internal static int argLen;
        private static List<List<object>> possibleRounds = new List<List<object>>();
        private static bool argStatus;
        private static Random randomChoice = new Random();
        private Help help;

        internal Rules(string[] argsInput)
        {
            usersArgs = argsInput;
            argLen = usersArgs.Length;
            TableGen();
            help = new Help(possibleRounds);
        }
        internal static bool CheckArguments()
        {
            return argStatus = (argLen == 0) ?
                (MessageForUser("You need to run TheGame with 3 or more (odd number) arguments.")) :
            (argLen < 3) ?
                (MessageForUser("You need more arguments for playing!")) :
            (argLen % 2 == 0) ?
                (MessageForUser("Number of arguments must be odd!")) :
            (!UnicArgs()) ?
                (MessageForUser("Do not repeat the arguments please.")) : true;
        }
        internal static void Menu()
        {
            Console.WriteLine("Available moves:");
            for (int i = 0; i < argLen; i++)
            {
                Console.WriteLine($"{i + 1} - {usersArgs[i]}");
            }
            Console.WriteLine("0 - Exit\n? - Help");
        }
        internal void InputUserData()
        {
            Console.Write("Please, enter your move: ");
            var input = Console.ReadLine();
            if (int.TryParse(input, out userChoice)) InputIsNumber();
            else InputIsString(input);
        }
        internal void InputPCData()
        {
            pcChoice = randomChoice.Next(argLen);
            key = HmacIt.FirstStep(usersArgs[pcChoice]);
        }
        internal void ShowAllCoices()
        {
            ShowPCChoice();
            ShowUserChoice();
        }
        internal void WhoWins()
        {
            Console.WriteLine($"\n{possibleRounds[pcChoice][userChoice]}\nKey: {key}\n");
        }
        private void InputIsNumber()
        {
            if (userChoice == 0) Environment.Exit(0);
            else if (userChoice <= argLen & userChoice > 0) userChoice--;
            else WrongInput();
        }
        private void InputIsString(string input)
        {
            if (input == "?")
            {
                Help.ShowMeTheTable();
                InputUserData();
            }
            else WrongInput();
        }
        private void WrongInput()
        {
            Console.WriteLine($"Please choose one of Menu options.");
            InputUserData();
        }       
        private static bool MessageForUser(string Message)
        {
            Console.WriteLine(Message);
            return false;
        }
        private static bool UnicArgs()
        {
            if (argLen == usersArgs.Distinct().Count()) return true;
            else return false;
        }
        private void TableGen()
        {
            possibleRounds.Add(FillField());
            for (int i = 1; i < argLen; i++)
            {
                possibleRounds.Add(ShiftField(possibleRounds[i - 1]));
            }
        }
        private List<object> FillField()
        {
            List<object> currentMoves = new List<object>();
            for (int i = 0; i < argLen; i++)
            {
                object temp = (i == 0) ? "Draw" :
                    (i <= (argLen - 1) / 2) ? "You win!" :
                        (i < argLen) ? "You Lose!" : "";
                currentMoves.Add(temp);
            }
            return currentMoves;
        }
        private List<object> ShiftField(List<object> previousString)
        {
            List<object> temp = new List<object>();
            temp.AddRange(previousString.GetRange(argLen - 1, 1));
            temp.AddRange(previousString.GetRange(0, argLen - 1));
            return temp;
        }
        private void ShowPCChoice()
        {
            Console.WriteLine($"\nPC move is No. {pcChoice + 1}: {usersArgs[pcChoice]}");
        }
        private void ShowUserChoice()
        {
            Console.WriteLine($"Your move is No. {userChoice + 1}: {usersArgs[userChoice]}");
        } 
    }
}
