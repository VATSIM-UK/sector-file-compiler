using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.Model
{
    public abstract class AbstractSectorElement
    {
        private readonly string comment;

        public string Comment
        {
            get
            {
                return this.comment != null && this.comment != "" ? " ;" + this.comment : "";
            }
        }

        protected AbstractSectorElement(string comment)
        {
            this.comment = comment;
        }
    }
}
