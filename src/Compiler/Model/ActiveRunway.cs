using System;
using System.Collections.Generic;

namespace Compiler.Model
{
    public class ActiveRunway : AbstractCompilableElement
    {
        public ActiveRunway(
            string identifier,
            string airfield,
            int mode,
            Definition definition,
            Docblock docBlock,
            Comment inlineComment

        ) : base(definition, docBlock, inlineComment)
        {
            Identifier = identifier;
            Airfield = airfield;
            Mode = mode;
        }

        public string Identifier { get; }
        public string Airfield { get; }
        public int Mode { get; }

        public override string GetCompileData()
        {
            return String.Format(
                "ACTIVE_RUNWAY:{0}:{1}:{2}",
                this.Airfield,
                this.Identifier,
                this.Mode
            );
        }
    }
}
