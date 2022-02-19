using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
namespace Motorola
{
    class Program
    {
        static void Main(string[] args)
        {
            string FilePath = @"C:\Users\huber\OneDrive\Desktop\MotorolaTask\Motorola\Motorola\Words.txt";
            string[] WordsFromFile = File.ReadAllText(FilePath).Split("\r\n");
            Random Rnd = new Random();

            List<string> WordsToGame = new List<string>();
            for (int i = 0; i < 4;)
            {
                int Index = Rnd.Next(0, WordsFromFile.Length);
                string WordsToMemory = WordsFromFile[Index];
                if (!WordsToGame.Contains(WordsToMemory))
                {
                    WordsToGame.Add(WordsToMemory);
                    WordsToGame.Add(WordsToMemory);
                    i++;
                }

            }


            for (int i = 0; i < 100; i++)
            {
                int Index = Rnd.Next(0, WordsToGame.Count);
                int SecondIndex = Rnd.Next(0, WordsToGame.Count);
                string Tmp = WordsToGame[Index];
                WordsToGame[Index] = WordsToGame[SecondIndex];
                WordsToGame[SecondIndex] = Tmp;
            }

            
            var Board = WordsToGame.Select(x => new Word { Text = x }).ToList();

            Console.Write("  ");
            for (int i = 0; i < 4; i++)
            {
                Console.Write(i + " ");
            }

            Console.WriteLine();

            
            for (int i = 0; i < Board.Count; i++)
            {
                
                if (i != 0 && i % 4 == 0)
                {
                    Console.WriteLine();
                }
                if (i % 4 == 0)
                {
                    Console.Write((char)('A' + i ) + " ");
                }
                Console.Write(Board[i] + " ");
            }
            

         }
    }
}
