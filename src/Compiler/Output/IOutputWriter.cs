namespace Compiler.Output
{
    public interface IOutputWriter
    {
        /**
         * Write a line to the output.
         */
        public void WriteLine(string line = "");

        /**
         * Flush the output stream
         */
        public void Flush();
    }
}