using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Compiler.Model
{
    /*
     * Represents a possibly multi-line comment that exists on its own lines.
     */
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
