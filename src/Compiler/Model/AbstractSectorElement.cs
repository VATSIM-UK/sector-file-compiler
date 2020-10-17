using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.Model
{
    public abstract class AbstractSectorElement: IDefinable
    {
        private readonly Definition definition;

        // The unique ID of the element
        public int Id { get; }

        // The elements preceding comment line(s) - or Docblock
        public Docblock Docblock { get; }

        // The inline comment for this element - goes on the end of the line
        public Comment InlineComment { get; }

        protected AbstractSectorElement(int id, Definition definition, Docblock docblock, Comment inlineComment)
        {
            this.Id = id;
            this.definition = definition;
            Docblock = docblock;
            InlineComment = inlineComment;
        }

        public Definition GetDefinition()
        {
            return this.definition;
        }
    }
}
