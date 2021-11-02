using ConsoleTableExt;
using System.Collections.Generic;

namespace The_Game
{
    class Help
    {
        private static string[] nameOfColumns = new string[Rules.argLen +1];
        private static List<List<object>> roundTable = new List<List<object>>(Rules.argLen);

        internal Help(List<List<object>> inputGameTable)
        {
            inputGameTable.ForEach((item) =>
            {
                roundTable.Add(new List<object>(item));
            });
            RebuildTable();
        }
        internal static void ShowMeTheTable()
        {
            ConsoleTableBuilder
               .From(roundTable)
               .WithColumn(nameOfColumns)
               .ExportAndWriteLine();
        }
        private static void RebuildTable()
        {
            nameOfColumns[0] = $"PC \\ Player";
            for (int i = 1; i < nameOfColumns.Length; i++)
            {
                nameOfColumns[i] = $"{i}";
                roundTable[i - 1].Insert(0, i);
            }
        }
    }
}

