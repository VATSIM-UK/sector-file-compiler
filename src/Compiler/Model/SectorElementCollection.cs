using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.Model
{
    public class SectorElementCollection
    {
        public List<SidStar> SidStars { get; } = new List<SidStar>();

        public void AddSidStar(SidStar sidStar)
        {
            this.SidStars.Add(sidStar);
        }
    }
}
