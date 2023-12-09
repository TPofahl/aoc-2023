using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aoc_2023
{
    internal static class Helpers
    {
        /// <summary>
        /// <para>Provides the path to the data text file.</para>
        /// <para>Data files can only include the .txt extension, and must be included in the Data folder. Adding .txt is not required.</para>
        /// <para>Note: The text files must be copied to the output directory. In order to do this, right-click on the text file in the Solution Explorer, and select Properties. In
        /// the Advanced dropdown, set Copy to Output Directory to "copy always".</para>
        /// </summary>
        public static StreamReader GetInputData(string fileName)
        {
            return new StreamReader(new FileStream(Path.Combine(AppContext.BaseDirectory, $"Data\\{fileName.Replace(".txt","")}.txt"), FileMode.Open, FileAccess.Read));
        }

        /// <summary>
        /// <para>Outputs the answer to the console.</para>
        /// </summary>
        public static void OutputSolve(int day, int part, double solution)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write($"Day ");
            Console.ResetColor();
            Console.Write(day);
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write(" Part ");
            Console.ResetColor();
            Console.Write(part);
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write(" solution is: ");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write($"{solution}" + "\n");
            Console.ResetColor();
        }

        public static void OutputChristmasTree()
        {
            string christmasTree =
                "                                            |\n" +
                @"                                           \|/" + "\n" +
                "                                          --*--\n" +
                "                                           >O<\n" +
                "                                          >O<<<\n" +
                "                                         >>o>>*<\n" +
                "                                        >o<<<o<<<\n" +
                "                                       >>@>*<<O<<<\n" +
                "                                      >o>>@>>>o>o<<\n" +
                "                                     >*>>*<o<@<o<<<<\n" +
                "                                    >o>o<<<O<*>>*>>O<\n" +
                "                                       _ __| |__ _";

            for (int i = 0; i < christmasTree.Length; i++)
            {
                char currentChar = christmasTree[i];

                switch (currentChar)
                {
                    case '*':
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        break;
                    case '-':
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        break;
                    case '\\':
                        Console.ForegroundColor= ConsoleColor.Yellow;
                        break;
                    case '|':
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        break;
                    case '/':
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        break;
                    case '>':
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        break;
                    case '<':
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        break;
                    case '@':
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        break;
                    case 'o':
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        break;
                    case 'O':
                        Console.ForegroundColor = ConsoleColor.Blue;
                        break;
                    default:
                        Console.ResetColor();
                        break;
                }

                Console.Write(currentChar);
            }
            Console.ResetColor();
            Console.Write("\n\n");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("                                   ADVENT OF CODE 2023 \n");
            Console.ResetColor();
        }
    }
}
