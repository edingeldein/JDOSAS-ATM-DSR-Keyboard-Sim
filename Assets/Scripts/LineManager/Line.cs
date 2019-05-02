using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSR.LineManager
{
    public class Line
    {
        public string Text { get; private set; }
        public string CursorText { get; private set; }
        public int Length => Text.Length;

        public Line()
        {
            Text = string.Empty;
            UpdateCursor();
        }

        public Line(string startingText)
        {
            Text = startingText;
            UpdateCursor();
        }

        public void AddChar(string charToAdd)
        {
            Text += charToAdd;

            UpdateCursor();
        }

        public void Backspace()
        {
            if (Length <= 0) return;

            Text = Text.Substring(0, Length - 1);
            UpdateCursor();
        }

        public void ClearLine()
        {
            Text = string.Empty;
            UpdateCursor();
        }

        private void UpdateCursor()
        {
            if (Length != 0)
                CursorText = new string(' ', Length);
            else
                CursorText = string.Empty;

            CursorText += '_';
        }

    }
}
