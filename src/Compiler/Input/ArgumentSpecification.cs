using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.Input
{
    struct ArgumentSpecification
    {
        public ArgumentSpecification(ArgumentDataType dataType, ArgumentRequirement required, ArgumentCardinality cardinality)
        {
            this.dataType = dataType;
            this.required = required;
            this.cardinality = cardinality;
        }

        // The type of the argument
        public readonly ArgumentDataType dataType;

        // Is the argument required for running the compiler
        public readonly ArgumentRequirement required;

        // How many times can this argument appear
        public readonly ArgumentCardinality cardinality;
    }
}
