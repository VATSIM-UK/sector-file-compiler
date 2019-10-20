using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Compiler.Output;

namespace Compiler.Input
{
    class SectionFactory
    {   

        private readonly Object configFile;

        private readonly Dictionary<OutputSections, string> configFileSections = new Dictionary<OutputSections, string>
        {
            { OutputSections.ESE_PREAMBLE, "preamble"},
        };

        public SectionFactory(IFileInterface configFile)
        {
            this.configFile = JsonConvert.DeserializeObject(configFile.Contents());
        }

        public Section Create(OutputSections section)
        {
            return new Section();
        }
    }
}
