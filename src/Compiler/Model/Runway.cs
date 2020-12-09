namespace Compiler.Model
{
    public class Runway : AbstractCompilableElement
    {

        public Runway(
            string airfieldIcao,
            string firstIdentifier,
            int firstHeading,
            Coordinate firstThreshold,
            string reverseIdentifier,
            int reverseHeading,
            Coordinate reverseThreshold,
            Definition definition,
            Docblock docblock,
            Comment inlineComment
        ) 
            : base(definition, docblock, inlineComment) 
        {
            AirfieldIcao = airfieldIcao;
            FirstIdentifier = firstIdentifier;
            FirstHeading = firstHeading;
            FirstThreshold = firstThreshold;
            ReverseIdentifier = reverseIdentifier;
            ReverseHeading = reverseHeading;
            ReverseThreshold = reverseThreshold;
        }

        public string AirfieldIcao { get; }
        public string FirstIdentifier { get; }
        public int FirstHeading { get; }
        public Coordinate FirstThreshold { get; }
        public string ReverseIdentifier { get; }
        public int ReverseHeading { get; }
        public Coordinate ReverseThreshold { get; }

        public override string GetCompileData()
        {
            return string.Format(
                "{0} {1} {2} {3} {4} {5}",
                this.FirstIdentifier,
                this.ReverseIdentifier,
                this.FormatHeading(this.FirstHeading),
                this.FormatHeading(this.ReverseHeading),
                this.FirstThreshold.ToString(),
                this.ReverseThreshold.ToString()
            );
        }

        private string FormatHeading(int heading)
        {
            return heading.ToString().PadLeft(3, '0');
        }
    }
}
