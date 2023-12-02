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
        /// <para>Note: the text files must be copied to the output directory. In order to do this, right-click on the text file in the Solution Explorer, and select Properties. In
        /// the Advanced dropdown, set Copy to Output Directory to "copy always".</para>
        /// </summary>
        public static StreamReader GetInputDataPath(string fileName)
        {
            return new StreamReader(new FileStream(Path.Combine(AppContext.BaseDirectory, $"Data\\{fileName.Replace(".txt","")}.txt"), FileMode.Open, FileAccess.Read));
        }

        /// <summary>
        /// <para>Outputs the answer to the console.</para>
        /// </summary>
        public static void OutputSolve(int day, int part, int solution)
        {
            Console.WriteLine($"Day {day} Part {part} solution is: {solution}" + "\n");
        }
    }
}
