using System.Collections.Generic;

namespace Compiler.Model
{
    public class SidStar :ICompilable
    {
        public string Type { get; }
        public string Airport { get; }
        public string Runway { get; }
        public string Identifier { get; }
        public List<string> Route { get; }

        public SidStar(
            string type,
            string airport,
            string runway,
            string identifier,
            List<string> route
        )
        {
            this.Type = type;
            this.Airport = airport;
            this.Runway = runway;
            this.Identifier = identifier;
            this.Route = route;
        }

        public string Compile()
        {
            return string.Format(
                "{0}:{1}:{2}:{3}:{4}\r\n",
                this.Type,
                this.Airport,
                this.Runway,
                this.Identifier,
                string.Join(' ', this.Route)
            );
        }
    }
}
