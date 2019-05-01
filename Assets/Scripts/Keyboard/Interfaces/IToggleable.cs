using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Events;

namespace DSR.Keyboard.Interfaces
{
    public interface IToggleable
    {
        void AddListener(UnityAction<bool> toggleHandler);
        void OnToggle(bool value);
    }
}
