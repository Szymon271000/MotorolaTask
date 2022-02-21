using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Motorola
{
    class Game
    {
        private readonly string FilePath = @"C:\Users\huber\OneDrive\Desktop\MotorolaTask\Motorola\Motorola\Words.txt";
        private const int ShuffleCount = 100;
        private const int MaxShownWordsOnce = 2;
        private List<Word> Board;
        private string Playmode;
        private int Chances;
        private int Width;
        private Word lastWord;

        public Game(string Playmode)
        {
            this.Playmode = Playmode;
            if (Playmode == "Easy")
            {
                Chances = 10;
                Width = 4;
                
            }
            else if (Playmode == "Hard")
            {
                Chances = 15;
                Width = 8;
            }
            else
            {
                throw new ArgumentException("Incorrect value of Playmode");
            }

            string[] WordsFromFile = File.ReadAllText(FilePath).Split("\r\n");
            Random Rnd = new Random();

            List<string> WordsToGame = new List<string>();
            for (int i = 0; i < Width;)
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

            for (int i = 0; i < ShuffleCount; i++)
            {
                int Index = Rnd.Next(0, WordsToGame.Count);
                int SecondIndex = Rnd.Next(0, WordsToGame.Count);
                string Tmp = WordsToGame[Index];
                WordsToGame[Index] = WordsToGame[SecondIndex];
                WordsToGame[SecondIndex] = Tmp;
            }

            Board = WordsToGame.Select(x => new Word { Text = x }).ToList();
        }

        public void DisplayBoard()
        {
            Console.WriteLine($"Chances: {Chances}");
            Console.Write("  ");
            for (int i = 0; i < Width; i++)
            {
                Console.Write(i + " ");
            }

            Console.WriteLine();


            for (int i = 0; i < Board.Count; i++)
            {

                if (i != 0 && i % Width == 0)
                {
                    Console.WriteLine();
                }
                if (i % Width == 0)
                {
                    Console.Write((char)('A' + i) + " ");
                }
                Console.Write(Board[i] + " ");
            }
            Console.WriteLine();
        }

        public bool AllWordsArePaired => Board.All(x => x.Paired);

        public bool HasChances => Chances > 0;

        public bool GameOver => AllWordsArePaired || !HasChances;

        public int ShownWords => Board.Count(x => x.Shown);

        public void HideWords() => Board.ForEach(x => x.Shown = false);

        public bool ShowWord(string coords)
        {
            if (coords == null || coords.Length != MaxShownWordsOnce || !Char.IsLetter(coords[0]) || !Char.IsDigit(coords[1]))
            {
                return false;
            }

            int x = Char.ToUpper(coords[0]) - 'A';
            int y = coords[1] - '0';

            int index = x * Width + y;
            if(index >= Board.Count)
            {
                return false;
            }

            if(ShownWords >= MaxShownWordsOnce)
            {
                HideWords();
                lastWord = null;
            }

            Chances--;
            Board[index].Shown = true;

            if (lastWord != null)
            {
                if (Board[index].Text == lastWord.Text)
                {
                    Board[index].Paired = true;
                    lastWord.Paired = true;
                    lastWord = null;
                    return true;
                }
            }

            lastWord = Board[index];
            return true;
        }
    }
}
