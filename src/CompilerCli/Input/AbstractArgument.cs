using System;
using System.Collections.Generic;
using Compiler.Argument;

namespace CompilerCli.Input
{
    public abstract class AbstractArgument: IComparable
    {
        /**
         * Parses values for the argument and adjusts compiler arguments.
         */
        public abstract void Parse(List<string> values, CompilerArguments compilerSettings);

        /**
         * Returns the string that specifies the argument
         */
        public abstract string GetSpecifier();

        public override bool Equals(object obj)
        {
            return obj is AbstractArgument &&
                   (obj as AbstractArgument).GetSpecifier() == GetSpecifier();
        }

        public override int GetHashCode()
        {
            return GetSpecifier().GetHashCode();
        }

        public int CompareTo(object obj)
        {
            if (obj is not AbstractArgument)
            {
                return 1;
            }

            return String.Compare(GetSpecifier(), ((AbstractArgument) obj).GetSpecifier(), StringComparison.Ordinal);
        }
    }
}
