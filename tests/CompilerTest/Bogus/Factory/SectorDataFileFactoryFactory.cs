using System.Collections.Generic;
using Compiler.Input;
using CompilerTest.Mock;

namespace CompilerTest.Bogus.Factory
{
    static class SectorDataFileFactoryFactory
    {
        public static SectorDataFileFactory Make(List<string> linesInFile = null)
        {
            return new(new MockInputStreamFactory(linesInFile ?? new List<string>()));
        }
    }
}
