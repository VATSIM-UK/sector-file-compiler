using System.Collections.Generic;
using System.Linq;
using Compiler.Config;
using Compiler.Input;
using Compiler.Input.Builder;
using Compiler.Input.Generator;
using Compiler.Input.Rule;
using Compiler.Output;
using Xunit;

namespace CompilerTest.Config
{
    public class ConfigInclusionRulesTest
    {
        private readonly IInclusionRule rule1;
        private readonly IInclusionRule rule2;
        private readonly IInclusionRule rule3;
        private readonly ConfigInclusionRules ruleset;

        public ConfigInclusionRulesTest()
        {
            rule1 = CreateInclusionRule();
            rule2 = CreateInclusionRule();
            rule3 = CreateInclusionRule();
            
            ruleset = new ConfigInclusionRules();
            ruleset.AddAirportInclusionRule(rule3);
            ruleset.AddEnrouteInclusionRule(rule1);
            ruleset.AddMiscInclusionRule(rule2);
        }

        private IInclusionRule CreateInclusionRule()
        {
            return FileInclusionRuleBuilder.Begin()
                .SetGenerator(new FileListGenerator(new List<string>()))
                .SetDataType(InputDataType.ESE_AGREEMENTS)
                .SetOutputGroup(new OutputGroup("1"))
                .Build();
        }

        [Fact]
        public void TestItMergesRule()
        {
            Assert.Equal(3, ruleset.Count());
        }
        
        [Fact]
        public void TestItEnumeratesMergedRules()
        {
            using IEnumerator<IInclusionRule> enumerator = ruleset.GetEnumerator();
            enumerator.MoveNext();
            Assert.Same(rule3, enumerator.Current);
            enumerator.MoveNext();
            Assert.Same(rule1, enumerator.Current);
            enumerator.MoveNext();
            Assert.Same(rule2, enumerator.Current);
        }
    }
}
