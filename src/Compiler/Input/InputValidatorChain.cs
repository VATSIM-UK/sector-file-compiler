using System.Collections.Generic;

namespace Compiler.Input
{
    public class InputValidatorChain
    {
        // The validators in the chain
        private readonly List<InputValidator> validators;

        public InputValidatorChain(List<InputValidator> validators)
        {
            this.validators = validators;
        }

        /**
         * Check that every validator in the chain passes.
         */
        public bool Validate(dynamic value)
        {
            bool valid = true;
            foreach (InputValidator validator in this.validators)
            {
                valid = valid && validator.Validate(value);
            }

            return valid;
        }

        /**
         * Return all the errors from all the validators
         * in the chain.
         */
        public List <string> GetAllValidationErrors()
        {
            List<string> errors = new List<string>();
            foreach (InputValidator validator in this.validators) 
            {
                errors.AddRange(validator.GetValidationErrors());
            }

            return errors;
        }
    }
}
