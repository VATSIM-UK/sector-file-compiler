using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.Input
{
    /**
     *  Creates arguments from strings.
     */
    public class ArgumentFactory
    {
        public static Argument CreateFromString(ArgumentType type, string value)
        {
            AvailableArguments arguments = new AvailableArguments();

            switch (arguments.GetArgumentSpecification(type).dataType)
            {
                case ArgumentDataType.String:
                {
                    return new Argument(type, value);
                }
            }

            return new Argument(type, null);
        }
    }
}
