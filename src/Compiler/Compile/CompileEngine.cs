using System;
using System.Collections.Generic;
using System.Text;
using Compiler.Argument;

namespace Compiler.Compile
{
    public class CompileEngine
    {
        private readonly List<SectionCompiler> compilers;

        public CompileEngine(List<SectionCompiler> compilers)
        {
            this.compilers = compilers;
        }

        public void Compile()
        {
            foreach (SectionCompiler compiler in this.compilers)
            {
                compiler.Compile();
            }
        }
    }
}
