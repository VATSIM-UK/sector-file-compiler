using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.Model
{
    public class Artcc : AbstractSectorElement, ICompilable
    {
        public Artcc(
            string identifier,
            ArtccType type,
            Coordinate startCoordinate,
            Coordinate endCoordinate,
            string comment
        ) 
            : base(comment)
        {
            Identifier = identifier;
            Type = type;
            StartCoordinate = startCoordinate;
            EndCoordinate = endCoordinate;
        }

        public string Identifier { get; }
        public ArtccType Type { get; }
        public Coordinate StartCoordinate { get; }
        public Coordinate EndCoordinate { get; }

        public string Compile()
        {
            return String.Format(
                "{0} {1} {2}{3}\r\n",
                this.Identifier,
                this.StartCoordinate.ToString(),
                this.EndCoordinate.ToString(),
                this.CompileComment()
            );
        }
    }
}
