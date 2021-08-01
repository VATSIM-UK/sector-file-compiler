using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Compiler.Input.Rule;

namespace Compiler.Config
{
    public class ConfigInclusionRules: IEnumerable<IInclusionRule>
    {
        private readonly List<IInclusionRule> airportInclusionRules = new();
        private readonly List<IInclusionRule> enrouteInclusionRules = new();
        private readonly List<IInclusionRule> miscInclusionRules = new();

        public void AddAirportInclusionRule(IInclusionRule rule)
        {
            airportInclusionRules.Add(rule);
        }

        public void AddEnrouteInclusionRule(IInclusionRule rule)
        {
            enrouteInclusionRules.Add(rule);
        }

        public void AddMiscInclusionRule(IInclusionRule rule)
        {
            miscInclusionRules.Add(rule);
        }

        public IEnumerator<IInclusionRule> GetEnumerator()
        {
            return GetMergedRules().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)GetMergedRules()).GetEnumerator();
        }

        private IEnumerable<IInclusionRule> GetMergedRules()
        {
            return (new List<IInclusionRule>()).Concat(airportInclusionRules)
                .Concat(enrouteInclusionRules)
                .Concat(miscInclusionRules);
        }
    }
}
