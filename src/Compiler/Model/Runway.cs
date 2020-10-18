using System;

namespace Compiler.Model
{
    public class Runway : AbstractCompilableElement, ICompilable
    {

        public Runway(
            string firstIdentifier,
            int firstHeading,
            Coordinate firstThreshold,
            string reverseIdentifier,
            int reverseHeading,
            Coordinate reverseThreshold,
            string runwayDialogDescription,
            string comment
        ) 
            : base(comment) 
        {
            FirstIdentifier = firstIdentifier;
            FirstHeading = firstHeading;
            FirstThreshold = firstThreshold;
            ReverseIdentifier = reverseIdentifier;
            ReverseHeading = reverseHeading;
            ReverseThreshold = reverseThreshold;
            RunwayDialogDescription = runwayDialogDescription;
        }

        public string FirstIdentifier { get; }
        public int FirstHeading { get; }
        public Coordinate FirstThreshold { get; }
        public string ReverseIdentifier { get; }
        public int ReverseHeading { get; }
        public Coordinate ReverseThreshold { get; }
        public string RunwayDialogDescription { get; }

        public string Compile()
        {
            return String.Format(
                "{0} {1} {2} {3} {4} {5} {6}{7}\r\n",
                this.FirstIdentifier,
                this.ReverseIdentifier,
                this.FormatHeading(this.FirstHeading),
                this.FormatHeading(this.ReverseHeading),
                this.FirstThreshold.ToString(),
                this.ReverseThreshold.ToString(),
                this.RunwayDialogDescription,
                this.CompileComment()
            );
        }

        private string FormatHeading(int heading)
        {
            return heading.ToString().PadLeft(3, '0');
        }
    }
}
