using System.Collections.Generic;

namespace Compiler.Model
{
    public class SectorElementCollection
    {
        public List<SidStar> SidStars { get; } = new List<SidStar>();

        public List<Colour> Colours { get; } = new List<Colour>();

        public void AddSidStar(SidStar sidStar)
        {
            this.SidStars.Add(sidStar);
        }

        public void AddColour(Colour colour)
        {
            this.Colours.Add(colour);
        }
    }
}
