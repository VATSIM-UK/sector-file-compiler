﻿using System;
using Compiler.Output;
using System.Collections.Generic;

namespace Compiler.Model
{
    public class SectorElementCollection
    {
        public List<SidStar> SidStars { get; } = new List<SidStar>();

        public List<Colour> Colours { get; } = new List<Colour>();

        public Dictionary<OutputSections, List<ICompilable>> Compilables { get; } = new Dictionary<OutputSections, List<ICompilable>>();

        public SectorElementCollection()
        {
            foreach (OutputSections section in Enum.GetValues(typeof(OutputSections)))
            {
                Compilables.Add(section, new List<ICompilable>());
            }
        }

        public void Add(SidStar sidStar)
        {
            this.Compilables[OutputSections.ESE_SIDSSTARS].Add(sidStar);
            this.SidStars.Add(sidStar);
        }

        public void Add(Colour colour)
        {
            this.Compilables[OutputSections.SCT_COLOUR_DEFS].Add(colour);
            this.Colours.Add(colour);
        }

        public void Add(BlankLine blankLine, OutputSections section)
        {
            this.Compilables[section].Add(blankLine);
        }

        public void Add(CommentLine comment, OutputSections section)
        {
            this.Compilables[section].Add(comment);
        }
    }
}
