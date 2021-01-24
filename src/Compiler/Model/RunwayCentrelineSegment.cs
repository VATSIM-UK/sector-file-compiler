namespace Compiler.Model
{
    public class RunwayCentrelineSegment
    {
        public Coordinate FirstCoordinate { get; }
        public Coordinate SecondCoordinate { get; }

        public RunwayCentrelineSegment(
            Coordinate firstCoordinate,
            Coordinate secondCoordinate
        ) {
            FirstCoordinate = firstCoordinate;
            SecondCoordinate = secondCoordinate;
        }

        public override string ToString()
        {
            return $"{FirstCoordinate} {SecondCoordinate}";
        }
    }
}