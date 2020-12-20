using System.Collections.Generic;
using Compiler.Event;
using Compiler.Input;
using Compiler.Model;
using Compiler.Parser;
using CompilerTest.Mock;

namespace CompilerTest.Parser
{
    public abstract class AbstractParserTestCase
    {
        protected readonly SectorElementCollection sectorElementCollection;
        
        protected readonly Moq.Mock<IEventLogger> logger;
        
        protected abstract InputDataType GetInputDataType();

        protected AbstractParserTestCase()
        {
            this.logger = new Moq.Mock<IEventLogger>();
            this.sectorElementCollection = new SectorElementCollection();
        }
        
        private AbstractSectorDataFile GetInputFile(List<string> lines)
        {
            return new SectorDataFileFactory(
                new MockInputStreamFactory(lines)
            ).Create("nah", GetInputDataType());
            
        }

        protected void RunParserOnLines(List<string> lines)
        {
            AbstractSectorDataFile file = this.GetInputFile(lines);
            (new DataParserFactory(this.sectorElementCollection, this.logger.Object)).GetParserForFile(file).ParseData(file);
        }
    }
}
