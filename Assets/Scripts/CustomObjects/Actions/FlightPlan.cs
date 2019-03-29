using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomEnums;

namespace CustomObjects.Actions
{
    public class FlightPlan : IActionType
    {

        private List<Token> _actionFormat;
        private string _correctFormat;
        private string _userString;

        public FlightPlan(string correctFormat)
        {
            _correctFormat = correctFormat;
            _actionFormat = Tokenize(correctFormat);
        }

        private List<Token> Tokenize(string source)
        {
            source.Trim();
            var tknSrc = source.Split(' ');

            var tokenizedList = new List<Token>();
            tokenizedList.Add(new Token(ActionToken.AircraftID, tknSrc[0]));
            tokenizedList.Add(new Token(ActionToken.AircraftType, tknSrc[1]));
            tokenizedList.Add(new Token(ActionToken.BeaconCode, tknSrc[2]));
            tokenizedList.Add(new Token(ActionToken.FiledTrueAirspeed, tknSrc[3]));
            tokenizedList.Add(new Token(ActionToken.PointWhereProcessingBegins, tknSrc[4]));
            tokenizedList.Add(new Token(ActionToken.CoordinationTimeAtFix, tknSrc[5]));
            tokenizedList.Add(new Token(ActionToken.RequestedAltitude, tknSrc[6]));
            tokenizedList.Add(new Token(ActionToken.RouteOfFlight, tknSrc[7]));

            return tokenizedList;
        }

        public string GetActionFormatReport()
        {
            throw new NotImplementedException();
        }

        public bool VerifyActionFormat(string userString)
        {
            throw new NotImplementedException();
        }

    }
}
