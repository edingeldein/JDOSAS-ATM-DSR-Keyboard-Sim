using System;
using System.Collections.Generic;
using System.Linq;
using CustomEnums;

namespace CustomObjects.Actions
{
    public class Token
    {
        public ActionToken ActionToken { get; }
        public string Text { get; }

        public Token(ActionToken token, string text)
        {
            ActionToken = token;
            Text = text;
        }
    }
}
