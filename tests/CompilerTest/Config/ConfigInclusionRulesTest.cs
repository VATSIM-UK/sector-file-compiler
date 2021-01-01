using System.Collections.Generic;
using System.Linq;
using Compiler.Config;
using Compiler.Input;
using Compiler.Output;
using Xunit;

namespace CompilerTest.Config
{
    public class ConfigInclusionRulesTest
    {
        private readonly FileListInclusionRule rule1;
        private readonly FileListInclusionRule rule2;
        private readonly FileListInclusionRule rule3;
        private readonly ConfigInclusionRules ruleset;

        public ConfigInclusionRulesTest()
        {
            rule1 = new FileListInclusionRule(
                new List<string>(),
                false,
                "abc",
                InputDataType.ESE_AGREEMENTS,
                new OutputGroup("1")
            );
            rule2 = new FileListInclusionRule(
                new List<string>(),
                false,
                "abc",
                InputDataType.ESE_AGREEMENTS,
                new OutputGroup("1")
            );
            rule3 = new FileListInclusionRule(
                new List<string>(),
                false,
                "abc",
                InputDataType.ESE_AGREEMENTS,
                new OutputGroup("1")
            );

            ruleset = new ConfigInclusionRules();
            ruleset.AddAirportInclusionRule(rule3);
            ruleset.AddEnrouteInclusionRule(rule1);
            ruleset.AddMiscInclusionRule(rule2);
        }

        [Fact]
        public void TestItMergesRule()
        {
            Assert.Equal(3, this.ruleset.Count());
        }
        
        [Fact]
        public void TestItEnumeratesMergedRules()
        {
            using IEnumerator<IInclusionRule> enumerator = this.ruleset.GetEnumerator();
            Assert.Same(this.rule3, enumerator.Current);
            enumerator.MoveNext();
            Assert.Same(this.rule1, enumerator.Current);
            enumerator.MoveNext();
            Assert.Same(this.rule2, enumerator.Current);
        }
    }
}
