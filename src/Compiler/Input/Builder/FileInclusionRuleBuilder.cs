using System;
using System.Collections.Generic;
using Compiler.Input.Filter;
using Compiler.Input.Generator;
using Compiler.Input.Rule;
using Compiler.Input.Sorter;
using Compiler.Input.Validator;
using Compiler.Output;

namespace Compiler.Input.Builder
{
    public class FileInclusionRuleBuilder: IFileInclusionRuleBuilder
    {
        private IFileListGenerator generator;
        private List<IFileFilter> filters;
        private List<IFileValidator> validators;
        private List<IFileSetValidator> filesetValidators;
        private List<IFileSorter> sorters;
        private InputDataType? type;
        private OutputGroup group;
        private IRuleDescriptor descriptor;

        protected FileInclusionRuleBuilder()
        {
            filters = new();
            filesetValidators = new();
            validators = new();
            sorters = new();
        }
        
        public static FileInclusionRuleBuilder Begin()
        {
            return new();
        }

        public IFileInclusionRuleBuilder SetGenerator(IFileListGenerator listGenerator)
        {
            generator = listGenerator;
            return this;
        }

        public IFileInclusionRuleBuilder AddFilter(IFileFilter filter)
        {
            filters.Add(filter);
            return this;
        }

        public IFileInclusionRuleBuilder AddValidator(IFileValidator validator)
        {
            validators.Add(validator);
            return this;
        }
        
        public IFileInclusionRuleBuilder AddFilesetValidator(IFileSetValidator validator)
        {
            filesetValidators.Add(validator);
            return this;
        }

        public IFileInclusionRuleBuilder SetDataType(InputDataType inputDataType)
        {
            type = inputDataType;
            return this;
        }
        
        public IFileInclusionRuleBuilder SetOutputGroup(OutputGroup outputGroup)
        {
            group = outputGroup;
            return this;
        }

        public IFileInclusionRuleBuilder AddSorter(IFileSorter fileSorter)
        {
            sorters.Add(fileSorter);
            return this;
        }

        public IFileInclusionRuleBuilder SetDescriptor(IRuleDescriptor ruleDescriptor)
        {
            descriptor = ruleDescriptor;
            return this;
        }

        public IInclusionRule Build()
        {
            if (generator == null || type == null || group == null)
            {
                throw new InvalidOperationException("Invalid inclusion rule build");
            }

            return new InclusionRule(
                generator,
                filters,
                validators,
                filesetValidators,
                sorters,
                descriptor,
                (InputDataType) type,
                group
            );
        }
    }
}
