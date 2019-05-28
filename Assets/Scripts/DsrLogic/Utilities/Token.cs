using System;
using System.Collections.Generic;
using System.Text;

namespace DSR.DsrLogic.Utilities
{
    public class Token
    {
        public string Section { get; }
        public bool Correct { get; }

        public Token(string section, bool correct)
        {
            Section = section;
            Correct = correct;
        }
    }
}
