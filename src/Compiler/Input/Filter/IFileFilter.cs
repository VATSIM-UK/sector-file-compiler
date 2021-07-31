namespace Compiler.Input.Filter
{
    public interface IFileFilter
    {
        /**
         * Filter the files bas
         */
        public bool Filter(string path);
    }
}
