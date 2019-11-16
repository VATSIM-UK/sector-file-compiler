using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.Parser
{
    /**
     * Interface for classes that parse and check frequencies.
     * Initially, only relevant to VHF/UHF, but may expand in time to include
     * HF.
     */
    public interface IFrequencyParser
    {
        public string ParseFrequency(string frequency);
    }
}
