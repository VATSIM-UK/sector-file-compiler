using System.Collections.Generic;

namespace Compiler.Model
{
    public class SectorElementCollection
    {
        public List<SidStar> SidStars { get; } = new List<SidStar>();

        public List<Colour> Colours { get; } = new List<Colour>();

        public List<ICompilable> Compilables { get; } = new List<ICompilable>();

        public void Add(SidStar sidStar)
        {
            this.Compilables.Add(sidStar);
            this.SidStars.Add(sidStar);
        }

        public void Add(Colour colour)
        {
            this.Compilables.Add(colour);
            this.Colours.Add(colour);
        }
    }
}
