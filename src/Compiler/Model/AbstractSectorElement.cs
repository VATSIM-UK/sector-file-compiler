using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.Model
{
    public abstract class AbstractSectorElement
    {
        public string Comment { get; }

        protected AbstractSectorElement(string comment)
        {
            this.Comment = comment;
        }

        protected string CompileComment()
        {
            return this.Comment != null && this.Comment != "" ? " ;" + this.Comment : "";
        }
    }
}
