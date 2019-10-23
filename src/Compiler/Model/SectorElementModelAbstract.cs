namespace Compiler.Model
{
    /**
     * An abstract class for classes that represent the data from
     * the sectorfile.
     */
    public abstract class SectorElementModelAbstract
    {
        protected const char ITEM_DELIMETER = ':';

        private readonly string comment;

        public SectorElementModelAbstract(string comment)
        {
            this.comment = comment;
        }

        /**
         * Adds the comment to the output line.
         */
        protected string AddComment(string content)
        {
            return content + "; " + comment;
        }

        /**
         * Called to generate the final output for the sectorfile
         */
        public abstract string BuildOutput();
    }
}
