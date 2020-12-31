namespace Compiler.Model
{
    /*
     * A single comment segment in the output - may be a whole line or the end of one.
     */
    public class Comment
    {
        public Comment(string comment)
        {
            CommentString = comment;
        }

        public string CommentString { get; }

        public override string ToString()
        {
            return this.CommentString == "" ? "" : $"; {this.CommentString}";
        }

        public override bool Equals(object? obj)
        {
            return obj is Comment compareComment &&
                   compareComment.CommentString == this.CommentString;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
