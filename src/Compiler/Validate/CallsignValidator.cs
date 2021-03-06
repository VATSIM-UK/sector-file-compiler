﻿using System.Collections.Generic;

namespace Compiler.Validate
{
    public class CallsignValidator
    {
        private static readonly List<string> AllowedTypes = new()
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

        private static readonly char[] AllowedDelimiters = new[]
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
            return callsign.Split(AllowedDelimiters);
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
            return AllowedTypes.Contains(splitCallsign[splitCallsign.Length - 1]);
        }
    }
}
