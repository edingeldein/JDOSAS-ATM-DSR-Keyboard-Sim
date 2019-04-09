using System;
using System.Collections.Generic;
using System.Text;

namespace DsrBackend.Utilities
{
    public class ValidatedAction
    {
        string CorrectAction;
        string UserAction;
        public List<Token> Result { get; }
        public bool Correct { get; private set; }

        public ValidatedAction(string correct, string user)
        {
            CorrectAction = correct.Trim();
            UserAction = user.Trim();
            Result = new List<Token>();
            Correct = true;
            Validate();
        }

        void Validate()
        {
            var corrSpl = CorrectAction.Split(' ');
            var userSpl = UserAction.Split(' ');

            var sameLen = corrSpl.Length == userSpl.Length;
            var max = (corrSpl.Length > userSpl.Length) ? corrSpl.Length : userSpl.Length;
            for (var i = 0; i < max; i++)
            {
                var corrTok = (i < corrSpl.Length) ? corrSpl[i] : "?";
                var toChTok = (i < userSpl.Length) ? userSpl[i] : "?";

                var same = corrTok.Equals(toChTok);
                Result.Add(new Token(toChTok, same));
            }

            foreach (var token in Result)
            {
                if (!token.Correct)
                {
                    Correct = false;
                    break;
                }
            }
        }
    }
}
