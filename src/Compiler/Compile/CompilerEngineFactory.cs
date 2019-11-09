using System.Collections.Generic;
using Compiler.Argument;
using Compiler.Model;
using Compiler.Output;

namespace Compiler.Compile
{
    public class CompilerEngineFactory
    {
        public static CompileEngine Create(CompilerArguments arguments, SectorElementCollection elements)
        {
            List<SectionCompiler> compilers = new List<SectionCompiler>();
            SectionCompilerFactory sectionFactory = new SectionCompilerFactory(arguments, elements);
            foreach (OutputSections section in arguments.SctSections)
            {
                compilers.Add(sectionFactory.Create(section));
            }

            foreach (OutputSections section in arguments.EseSections)
            {
                compilers.Add(sectionFactory.Create(section));
            }

            return new CompileEngine(compilers);
        }
    }
}
