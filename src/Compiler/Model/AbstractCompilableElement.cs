using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Compiler.Model
{
    public abstract class AbstractCompilableElement: IDefinable, ICompilableElement, ICompilableElementProvider
    {
        // Where the element was defined
        private readonly Definition definition;

        // The elements preceding comment line(s) - or Docblock
        public Docblock Docblock { get; }

        // The inline comment for this element - goes on the end of the line
        public Comment InlineComment { get; }

        protected AbstractCompilableElement(Definition definition, Docblock docblock, Comment inlineComment)
        {
            this.definition = definition;
            Docblock = docblock;
            InlineComment = inlineComment;
        }

        /*
         * Gets the definition for this element
         */
        public Definition GetDefinition()
        {
            return this.definition;
        }

        /*
         * Returns the data for a single compiled line
         */
        public abstract string GetCompileData();

        /*
         * Compiles the data, along with inline comments and docblocks
         */
        public void Compile(SectorElementCollection elements, TextWriter output)
        {
            foreach (Comment line in this.Docblock)
            {
                output.WriteLine(line.ToString());
            }

            output.WriteLine(
                string.Format(
                    "{0}{1}{2}",
                    this.GetCompileData(),
                    this.InlineComment.ToString()
                )
            );
        }

        /*
         * For the majority of data items, they are in themselves a compilable element
         * so implement the interface method virtually so those that need to override it can do so.
         */
        public virtual IEnumerable<ICompilableElement> GetCompilableElements()
        {
            return new ICompilableElement[] { this };
        }
    }
}
