using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.Model
{
    public class InfoLatitude : AbstractCompilableElement
    {
        public InfoLatitude(
            string latitude,
            Definition definition,
            Docblock docblock,
            Comment inlineComment
        ) : base(definition, docblock, inlineComment)
        {
            this.Latitude = latitude;
        }

        public string Latitude { get; }

        public override string GetCompileData()
        {
            return this.Latitude;
        }
    }
}
