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
            Console.WriteLine("Enter difficulty level: ");
            string level = Console.ReadLine();

            Game game;
            try
            {
                game = new Game(level);
            }
            catch(ArgumentException ex)
            {
                Console.WriteLine(ex);
                Console.WriteLine("Exit program..");
                return;
            }

            while(!game.GameOver)
            {
                game.DisplayBoard();
                Console.WriteLine("Enter coordinates of word: ");
                string coords = Console.ReadLine();
                if(!game.ShowWord(coords))
                {
                    Console.WriteLine("You enter incorrect value!");
                }
            }

            if(game.AllWordsArePaired)
            {
                Console.WriteLine("You won");
            }
            else
            {
                Console.WriteLine("You lost");
            }
        }
    }
}
