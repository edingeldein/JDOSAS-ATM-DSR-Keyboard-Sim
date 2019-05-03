using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using DSR.LineManager.Interfaces;
using DSR.Interpreter;

namespace DSR.LineManager
{
    public class LineManagerController : MonoBehaviour, ILineManagerController
    {

        private Line _currentLine;

        private void Start()
        {
            _currentLine = new Line();
        }

        private void Update()
        {
            
        }

        public void KeyValueInput(KeyValue keyValue)
        {
            _currentLine.AddChar(keyValue.Value);
            Debug.Log($"KeyValue: {keyValue.Value}");
        }

        public void KeyCommandInput(KeyCommand keyCommand)
        {
            Debug.Log($"KeyValue: {keyCommand.Command}");
        }
    }
}
