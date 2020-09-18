using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Compiler.Model
{
    public class Docblock : IEnumerable<Comment>
    {
        protected List<Comment> Lines { get; set; }

        public Docblock()
        {
            this.Lines = new List<Comment>();
        }

        public void AddLine(Comment line)
        {
            this.Lines.Add(line);
        }

        public IEnumerator<Comment> GetEnumerator()
        {
            return ((IEnumerable<Comment>)Lines).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
