using System.Collections.Generic;

namespace Compiler.Input
{
    /**
     * An interface for classes that validate compiler
     * input and turn it into arguments.
     */
    public abstract class InputValidator
    {
        // Validation errors
        private List<string> validationErrors;

        public InputValidator() 
        {
            this.validationErrors = new List<string>();
        }

        // The validation method.
        public abstract bool Validate(dynamic argument);

        // Return a list of validation errors.
        public List<string> GetValidationErrors()
        {
            return this.validationErrors;
        }

        // Add errors to the validation error list
        protected void AddValidationError(string error)
        {
            this.validationErrors.Add(error);
        }
    }
}
