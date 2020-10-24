using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.Model
{
    public class InfoMilesPerDegreeLatitude : AbstractCompilableElement
    {
        public InfoMilesPerDegreeLatitude(
            int miles,
            Definition definition,
            Docblock docblock,
            Comment inlineComment
        ) : base(definition, docblock, inlineComment)
        {
            this.Miles = miles;
        }

        public int Miles { get; }

        public override string GetCompileData()
        {
            return this.Miles.ToString();
        }
    }
}
