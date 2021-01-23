using System.Collections;
using System.Collections.Generic;
using Compiler.Input;
using System.Linq;

namespace Compiler.Config
{
    public class ConfigInclusionRules: IEnumerable<IInclusionRule>
    {
        private readonly List<IInclusionRule> airportInclusionRules = new();
        private readonly List<IInclusionRule> enrouteInclusionRules = new();
        private readonly List<IInclusionRule> miscInclusionRules = new();

        public void AddAirportInclusionRule(IInclusionRule rule)
        {
            this.airportInclusionRules.Add(rule);
        }

        public void AddEnrouteInclusionRule(IInclusionRule rule)
        {
            this.enrouteInclusionRules.Add(rule);
        }

        public void AddMiscInclusionRule(IInclusionRule rule)
        {
            this.miscInclusionRules.Add(rule);
        }

        public IEnumerator<IInclusionRule> GetEnumerator()
        {
            return this.GetMergedRules().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)this.GetMergedRules()).GetEnumerator();
        }

        private IEnumerable<IInclusionRule> GetMergedRules()
        {
            return (new List<IInclusionRule>()).Concat(this.airportInclusionRules)
                .Concat(this.enrouteInclusionRules)
                .Concat(this.miscInclusionRules);
        }
    }
}
