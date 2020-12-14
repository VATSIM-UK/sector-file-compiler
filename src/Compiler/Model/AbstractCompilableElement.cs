﻿using System.Collections.Generic;
using System.IO;
using Compiler.Output;

namespace Compiler.Model
{
    public abstract class AbstractCompilableElement: ICompilableElement, ICompilableElementProvider
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
        public virtual void Compile(SectorElementCollection elements, TextWriter output)
        {
            foreach (Comment line in this.Docblock)
            {
                output.WriteLine(line.ToString().Trim());
            }

            output.WriteLine(
                string.Format(
                    "{0}{1}",
                    this.GetCompileData(),
                    this.InlineComment.ToString()
                )
            );
        }

        public OutputGroup GetDataGroup()
        {
            throw new System.NotImplementedException();
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