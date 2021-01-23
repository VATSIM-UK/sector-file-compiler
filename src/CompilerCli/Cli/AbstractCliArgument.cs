using System;
using System.Collections.Generic;

namespace CompilerCli.Cli
{
    public abstract class AbstractCliArgument: IComparable
    {
        /**
         * Parses values for the argument and adjusts compiler arguments.
         */
        public abstract void Parse(List<string> values, CliArguments cliArguments);

        /**
         * Returns the string that specifies the argument
         */
        public abstract string GetSpecifier();

        public override bool Equals(object obj)
        {
            return obj is AbstractCliArgument &&
                   (obj as AbstractCliArgument).GetSpecifier() == GetSpecifier();
        }

        public override int GetHashCode()
        {
            return GetSpecifier().GetHashCode();
        }

        public int CompareTo(object obj)
        {
            if (obj is not AbstractCliArgument)
            {
                return 1;
            }

            return String.Compare(GetSpecifier(), ((AbstractCliArgument) obj).GetSpecifier(), StringComparison.Ordinal);
        }
    }
}