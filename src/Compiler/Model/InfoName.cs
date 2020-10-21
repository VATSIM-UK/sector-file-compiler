using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.Model
{
    public class InfoName : AbstractCompilableElement
    {
        public InfoName(string name, Definition definition, Docblock docblock, Comment inlineComment)
            : base(definition, docblock, inlineComment)
        {
            this.Name = name;
        }

        public string Name { get; }

        public override string GetCompileData()
        {
            return this.Name;
        }
    }
}
