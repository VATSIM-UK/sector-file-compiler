using Compiler.Error;
using Compiler.Validate;
using CompilerTest.Bogus.Factory;
using Moq;
using Xunit;

namespace CompilerTest.Validate
{
    public class AllPositionsMustHaveUniqueIdentifierTest: AbstractValidatorTestCase
    {
        [Fact]
        public void TestItPassesIfNoDuplicates()
        {
            sectorElements.Add(ControllerPositionFactory.Make(identifier: "LS"));
            sectorElements.Add(ControllerPositionFactory.Make(identifier: "LC"));
            sectorElements.Add(ControllerPositionFactory.Make(identifier: "LLN"));

            AssertNoValidationErrors();
        }

        [Fact]
        public void TestItFailsIfThereAreDuplicates()
        {
            sectorElements.Add(ControllerPositionFactory.Make(identifier: "LS"));
            sectorElements.Add(ControllerPositionFactory.Make(identifier: "LC"));
            sectorElements.Add(ControllerPositionFactory.Make(identifier: "LS"));

            AssertValidationErrors();
        }

        [Fact]
        public void TestItFailsOnlyOncePerDuplicatedIdentifier()
        {
            sectorElements.Add(ControllerPositionFactory.Make(identifier: "LS"));
            sectorElements.Add(ControllerPositionFactory.Make(identifier: "LS"));
            sectorElements.Add(ControllerPositionFactory.Make(identifier: "LS"));
            sectorElements.Add(ControllerPositionFactory.Make(identifier: "LC"));
            sectorElements.Add(ControllerPositionFactory.Make(identifier: "LC"));

            AssertValidationErrors(2);
        }

        [Fact]
        public void TestItTreatsIdentifiersAsCaseInsensitive()
        {
            sectorElements.Add(ControllerPositionFactory.Make(identifier: "LS"));
            sectorElements.Add(ControllerPositionFactory.Make(identifier: "ls"));

            AssertValidationErrors();
        }

        [Fact]
        public void TestItFailsIfPositionsShareAnIdentifierEvenWithTheSameCallsign()
        {
            sectorElements.Add(ControllerPositionFactory.Make(identifier: "LLP", callsign: "EGLL_P_GND"));
            sectorElements.Add(ControllerPositionFactory.Make(identifier: "LLP", callsign: "EGLL_P_GND"));

            AssertValidationErrors();
        }

        [Fact]
        public void TestItListsEveryPositionSharingTheIdentifier()
        {
            sectorElements.Add(ControllerPositionFactory.Make(identifier: "LS", callsign: "LON_S_CTR"));
            sectorElements.Add(ControllerPositionFactory.Make(identifier: "LS", callsign: "LON_C_CTR"));

            new AllPositionsMustHaveUniqueIdentifier().Validate(sectorElements, args, loggerMock.Object);

            loggerMock.Verify(
                foo => foo.AddEvent(
                    It.Is<ValidationRuleFailure>(
                        failure => failure.GetMessage().Contains("LON_S_CTR")
                            && failure.GetMessage().Contains("LON_C_CTR")
                    )
                )
            );
        }

        protected override IValidationRule GetValidationRule()
        {
            return new AllPositionsMustHaveUniqueIdentifier();
        }
    }
}
