using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSR.Keyboard.Interfaces
{
    public interface IKeyboardController
    {
        void SetShift(bool shift);
        bool GetShift();
        void QueueKeypress(string keypress);
    }
}
