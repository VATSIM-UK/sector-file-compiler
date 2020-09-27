using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.Model
{
    public abstract class AbstractSectorElement: IDefinable
    {
        private readonly Definition definition;

        public string Comment { get; }

        protected AbstractSectorElement(Definition definition, string comment)
        {
            this.definition = definition;
            this.Comment = comment;
        }

        protected string CompileComment()
        {
            return this.Comment != null && this.Comment != "" ? " ;" + this.Comment : "";
        }

        public Definition GetDefinition()
        {
            return this.definition;
        }
    }
}
