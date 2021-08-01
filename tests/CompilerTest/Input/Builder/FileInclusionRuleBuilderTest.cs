using System;
using System.Collections.Generic;
using System.Linq;
using Compiler.Input;
using Compiler.Input.Builder;
using Compiler.Input.Generator;
using Compiler.Input.Rule;
using Compiler.Input.Sorter;
using Compiler.Input.Validator;
using Compiler.Output;
using Xunit;
using FileExists = Compiler.Input.Filter.FileExists;

namespace CompilerTest.Input.Builder
{
    public class FileInclusionRuleBuilderTest
    {
        private readonly FileInclusionRuleBuilder builder;

        public FileInclusionRuleBuilderTest()
        {
            builder = FileInclusionRuleBuilder.Begin();
        }

        [Fact]
        public void TestItBuildsTheRule()
        {
            var rule = builder.SetGenerator(new FileListGenerator(new List<string> {"Foo.txt"}))
                .SetDataType(InputDataType.SCT_GEO)
                .SetOutputGroup(new OutputGroup("Foo"))
                .SetDescriptor(new RuleDescriptor("Foo"))
                .AddFilter(new FileExists())
                .AddValidator(new Compiler.Input.Validator.FileExists())
                .AddSorter(new AlphabeticalPathSorter())
                .AddFilesetValidator(new FilelistNotEmpty(1))
                .Build() as InclusionRule ?? throw new InvalidOperationException();
            
            Assert.Equal(InputDataType.SCT_GEO, rule.DataType);
            Assert.Equal(new OutputGroup("Foo"), rule.Group);
            Assert.Equal(new RuleDescriptor("Foo"), rule.Descriptor);
            Assert.Single(rule.Filters);
            Assert.IsType<FileExists>(rule.Filters.First());
            Assert.Single(rule.Validators);
            Assert.IsType< Compiler.Input.Validator.FileExists>(rule.Validators.First());
            Assert.Single(rule.Sorters);
            Assert.IsType<AlphabeticalPathSorter>(rule.Sorters.First());
            Assert.Single(rule.FilesetValidators);
            Assert.IsType<FilelistNotEmpty>(rule.FilesetValidators.First());
        }
        
        [Fact]
        public void TestItThrowsExceptionIfNoGenerator()
        {
            Assert.Throws<InvalidOperationException>(
                () => builder.SetDataType(InputDataType.SCT_GEO)
                    .SetOutputGroup(new OutputGroup("Foo"))
                    .SetDescriptor(new RuleDescriptor("Foo"))
                    .AddFilter(new FileExists())
                    .AddValidator(new Compiler.Input.Validator.FileExists())
                    .AddSorter(new AlphabeticalPathSorter())
                    .AddFilesetValidator(new FilelistNotEmpty(1))
                    .Build()
            );
        }
        
        [Fact]
        public void TestItThrowsExceptionIfNoOutputGroup()
        {
            Assert.Throws<InvalidOperationException>(
                () => builder.SetGenerator(new FileListGenerator(new List<string> {"Foo.txt"}))
                    .SetDataType(InputDataType.SCT_GEO)
                    .SetDescriptor(new RuleDescriptor("Foo"))
                    .AddFilter(new FileExists())
                    .AddValidator(new Compiler.Input.Validator.FileExists())
                    .AddSorter(new AlphabeticalPathSorter())
                    .AddFilesetValidator(new FilelistNotEmpty(1))
                    .Build()
            );
        }
        
        [Fact]
        public void TestItThrowsExceptionIfNoDataType()
        {
            Assert.Throws<InvalidOperationException>(
                () => builder.SetGenerator(new FileListGenerator(new List<string> {"Foo.txt"}))
                    .SetOutputGroup(new OutputGroup("Foo"))
                    .SetDescriptor(new RuleDescriptor("Foo"))
                    .AddFilter(new FileExists())
                    .AddValidator(new Compiler.Input.Validator.FileExists())
                    .AddSorter(new AlphabeticalPathSorter())
                    .AddFilesetValidator(new FilelistNotEmpty(1))
                    .Build()
            );
        }
    }
}
