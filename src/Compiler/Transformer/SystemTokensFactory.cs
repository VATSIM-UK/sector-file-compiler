using System;
using System.Collections.Generic;
using Compiler.Argument;

namespace Compiler.Transformer
{
    public class SystemTokensFactory
    {
        public static Dictionary<string, string> GetSystemTokens(CompilerArguments arguments)
        {
            return new Dictionary<string, string>()
            {
                { "{YEAR}",  DateTime.Now.Year.ToString()},
                { "{VERSION}",  arguments.BuildVersion},
                { "{BUILD}",  DateTime.Now.ToString("yyyy-MM-dd") },
            };
        }
    }
}
