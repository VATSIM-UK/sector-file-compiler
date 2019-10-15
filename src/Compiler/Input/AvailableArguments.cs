using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.Input
{
    class AvailableArguments
    {
        public Dictionary<Argument, ArgumentSpecification> GetAvailableArguments()
        {
            Dictionary<Argument, ArgumentSpecification> arguments = new Dictionary<Argument, ArgumentSpecification>();

            arguments.Add(
                Argument.ConfigFile,
                new ArgumentSpecification(ArgumentDataType.String, ArgumentRequirement.Required, ArgumentCardinality.Single)
            );

            return arguments;
        }
    }
}
