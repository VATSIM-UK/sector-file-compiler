using Compiler.Input.Filter;
using Compiler.Input.Generator;
using Compiler.Input.Rule;
using Compiler.Input.Sorter;
using Compiler.Input.Validator;
using Compiler.Output;

namespace Compiler.Input.Builder
{
    public interface IFileInclusionRuleBuilder
    {
        public IFileInclusionRuleBuilder SetGenerator(IFileListGenerator generator);
        
        public IFileInclusionRuleBuilder AddFilter(IFileFilter filters);
        
        public IFileInclusionRuleBuilder AddValidator(IFileValidator validator);
        
        public IFileInclusionRuleBuilder AddFilesetValidator(IFileSetValidator validator);

        public IFileInclusionRuleBuilder SetDataType(InputDataType inputDataType);
        
        public IFileInclusionRuleBuilder SetOutputGroup(OutputGroup outputGroup);
        
        public IFileInclusionRuleBuilder AddSorter(IFileSorter sorter);
        
        public IFileInclusionRuleBuilder SetDescriptor(IRuleDescriptor descriptor);

        public IInclusionRule Build();
    }
}
