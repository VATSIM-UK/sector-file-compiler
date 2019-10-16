using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.Input
{
    class AvailableArguments
    {
        public Dictionary<ArgumentType, ArgumentSpecification> GetAvailableArguments()
        {
            Dictionary<ArgumentType, ArgumentSpecification> arguments = new Dictionary<ArgumentType, ArgumentSpecification>();

            arguments.Add(
                ArgumentType.ConfigFile,
                new ArgumentSpecification(ArgumentDataType.String, ArgumentRequirement.Required, ArgumentCardinality.Single)
            );

            return arguments;
        }
    }
}
