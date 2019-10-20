using System;
using System.Collections.Generic;

namespace Compiler.Input
{
    /**
     * Represents all the available arguments for the compiler.
     */
    public class AvailableArguments
    {

        // The arguments
        private readonly Dictionary<ArgumentType, ArgumentSpecification> arguments;

        public Dictionary<ArgumentType, ArgumentSpecification> Arguments {
            get 
            {
                return this.arguments;
            }
        }

        public AvailableArguments()
        {
            this.arguments = new Dictionary<ArgumentType, ArgumentSpecification>()
            {
                { 
                    ArgumentType.ConfigFile,
                    new ArgumentSpecification(ArgumentDataType.String, ArgumentRequirement.Required, ArgumentCardinality.Single)
                }
            };
        }

        public ArgumentSpecification GetArgumentSpecification(ArgumentType type)
        {
            return this.arguments[type];
        }
    }
}
