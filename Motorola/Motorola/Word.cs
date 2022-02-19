using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motorola
{
    class Word
    {
        public string Text { get; set; }

        public bool Shown { get; set; }

        public bool Paired { get; set; }

        public override string ToString()
        {
            if (Shown || Paired)
            {
                return Text;
            }
            else
                return "X";
        }
    }
}
