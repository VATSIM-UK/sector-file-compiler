namespace Compiler.Model
{
    /*
     * An interface for something that can be defined within
     * the sector file somewhere. Provides methods for where it was defined so that
     * this may be reported.
     */
    public interface IDefinable
    {
        /*
         * Returns the definition for this particular element.
         */
        public Definition GetDefinition();
    }
}
