using System;

namespace Compiler.Model
{
    public class ActiveRunway : AbstractSectorElement, ICompilable
    {
        public ActiveRunway(string identifier, string airfield, int mode, string comment) : base(comment)
        {
            Identifier = identifier;
            Airfield = airfield;
            Mode = mode;
        }

        public string Identifier { get; }
        public string Airfield { get; }
        public int Mode { get; }

        public string Compile()
        {
            return String.Format(
                "ACTIVE_RUNWAY:{0}:{1}:{2}{3}\r\n",
                this.Airfield,
                this.Identifier,
                this.Mode,
                this.CompileComment()
            );
        }
    }
}
