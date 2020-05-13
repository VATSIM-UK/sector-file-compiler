using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.Model
{
    public class Freetext : AbstractSectorElement, ICompilable
    {
        public Freetext(string title, string text, Coordinate coordinate, string comment)
            : base(comment)
        {
            Title = title;
            Text = text;
            Coordinate = coordinate;
        }

        public string Title { get; }
        public string Text { get; }
        public Coordinate Coordinate { get; }

        public string Compile()
        {
            return String.Format(
                "{0}:{1}:{2}:{3}{4}\r\n",
                this.Coordinate.latitude,
                this.Coordinate.longitude,
                this.Title,
                this.Text,
                this.CompileComment()
            );
        }
    }
}
