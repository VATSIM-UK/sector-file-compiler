using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Compiler.Event;
using Compiler.Input;
using Compiler.Model;
using Compiler.Parser;
using CompilerTest.Bogus.Factory;
using Xunit;

namespace CompilerTest.Parser
{
    public abstract class AbstractParserTestCase
    {
        protected string inputFileName = "TESTFOLDER/TEST.txt";
        
        protected readonly SectorElementCollection sectorElementCollection;
        protected readonly Moq.Mock<IEventLogger> logger;
        protected abstract InputDataType GetInputDataType();

        protected AbstractParserTestCase()
        {
            logger = new Moq.Mock<IEventLogger>();
            sectorElementCollection = new SectorElementCollection();
        }
        
        private AbstractSectorDataFile GetInputFile(List<string> lines)
        {
            return SectorDataFileFactoryFactory.Make(lines)
                .Create(inputFileName, GetInputDataType());
        }

        protected void RunParserOnLines(List<string> lines)
        {
            AbstractSectorDataFile file = GetInputFile(lines);
            new DataParserFactory(sectorElementCollection, logger.Object).GetParserForFile(file).ParseData(file);
        }

        [SuppressMessage("ReSharper", "ParameterOnlyUsedForPreconditionCheck.Local")]
        private static void AssertExpectedComment(string expected, Comment actual)
        {
            Assert.Equal(expected, actual.CommentString);
        }

        /**
         * Line numbers start at one as that's more human friendly
         */
        protected void AssertExpectedMetadata(
            AbstractCompilableElement element,
            int definitionLineNumber = 1,
            string commentString = "comment",
            List<string> docblockLines = null
        )
        {
            AssertExpectedComment(commentString, element.InlineComment);
            AssertExpectedDefinition(element.GetDefinition(), definitionLineNumber);
            AssertExpectedDocblockLines(element.Docblock, docblockLines);
        }
        
        // ReSharper disable once ParameterOnlyUsedForPreconditionCheck.Local
        private void AssertExpectedDefinition(Definition definition, int lineNumber)
        {
            Assert.Equal(new Definition(inputFileName, lineNumber), definition);
        }

        private static void AssertExpectedDocblockLines(Docblock docblock, List<string> docblockLines)
        {
            Docblock expectedDocblock = new();
            docblockLines?.ForEach(line => docblock.AddLine(new Comment(line)));

            Assert.Equal(expectedDocblock, docblock);
        }

        protected void SetInputFileName(string filename)
        {
            inputFileName = filename;
        }
    }
}
