using System;
using System.Collections.Generic;

namespace Compiler.Model
{
    public class SidStar : SectorElementModelAbstract
    {
        public const string TYPE_SID = "SID";

        public const string TYPE_STAR = "STAR";

        // Whether it is a SID or STAR
        private readonly string sidstar;
        public string SidOrStar
        {
            get
            {
                return this.sidstar;
            }
        }

        // The airport the SID or STAR belongs to
        private readonly string airport;
        public string Airport
        {
            get
            {
                return this.airport;
            }
        }

        // The runway the SID or STAR belongs to
        private readonly string runway;
        public string Runway
        {
            get
            {
                return this.runway;
            }
        }

        // The identifier of the SID or STAR
        private readonly string identifier;
        public string Identifier
        {
            get
            {
                return this.identifier;
            }
        }

        // The identifier of the SID or STAR
        private readonly List<string> route;
        public List<string> Route
        {
            get
            {
                return this.route;
            }
        }

        public SidStar(
            string sidstar,
            string airport,
            string runway,
            string identifier,
            List<string> route,
            string comment
        ) : base(comment)
        {
            this.sidstar = sidstar;
            this.airport = airport;
            this.runway = runway;
            this.identifier = identifier;
            this.route = route;
        }

        public override string BuildOutput()
        {
            return base.AddComment(
                this.sidstar + SectorElementModelAbstract.ITEM_DELIMETER + this.airport + SectorElementModelAbstract.ITEM_DELIMETER +
                    this.runway + SectorElementModelAbstract.ITEM_DELIMETER + this.identifier + 
                    SectorElementModelAbstract.ITEM_DELIMETER + String.Join(" ", this.route)
            );
        }
    }
}
