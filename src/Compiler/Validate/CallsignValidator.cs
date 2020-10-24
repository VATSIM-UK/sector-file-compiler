using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Compiler.Validate
{
    public class CallsignValidator
    {
        private static readonly List<string> allowedTypes = new List<string>()
        {
            "OBS",
            "DEL",
            "GND",
            "APP",
            "TWR",
            "CTR",
            "FSS",
            "ATIS",
            "INFO"
        };

        private static readonly char[] allowedDelimiters = new char[]
        {
            '_',
            '-'
        };

        public static bool Validate(string callsign)
        {
            return CallsignLengthValid(callsign) &&
                CallsignContainsSplit(callsign) &&
                CallsignContainsValidNumberOfSplits(callsign) &&
                CallsignTypeValid(callsign);
        }

        private static string[] SplitCallsign(string callsign)
        {
            return callsign.Split(allowedDelimiters);
        }

        private static bool CallsignLengthValid(string callsign)
        {
            return callsign.Length > 0 && callsign.Length < 11;
        }

        private static bool CallsignContainsSplit(string callsign)
        {
            return SplitCallsign(callsign).Length != 1;
        }

        private static bool CallsignContainsValidNumberOfSplits(string callsign)
        {
            string[] split = SplitCallsign(callsign);
            return split.Length == 2 || split.Length == 3;
        }

        private static bool CallsignTypeValid(string callsign)
        {
            string[] splitCallsign = SplitCallsign(callsign);
            return allowedTypes.Contains(splitCallsign[splitCallsign.Length - 1]);
        }
    }
}
