using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSR.Console.Interfaces
{
    public interface IConsoleController
    {
        void UpdateLine(string line, string cursor);
        void NewLine(string line, string cursor);
        void ClearLines();
    }
}
