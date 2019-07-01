using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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