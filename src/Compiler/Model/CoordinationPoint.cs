namespace Compiler.Model
{
    public class CoordinationPoint : AbstractCompilableElement
    {
        public static readonly string POINT_TYPE_FIR = "FIR_COPX";
        public static readonly string POINT_TYPE_INTERNAL = "COPX";
        public static readonly string DATA_NOT_SPECIFIED = "*";

        public CoordinationPoint(
            bool isFirCopx,
            string departureAirportOrFixBefore,
            string departureRunway,
            string coordinationFix,
            string arrivalAirportOrFixAfter,
            string arrivalRunway,
            string fromSector,
            string toSector,
            string climbLevel,
            string descendLevel,
            string name,
            Definition definition,
            Docblock docblock,
            Comment inlineComment
        ) : base(definition, docblock, inlineComment) 
        {
            IsFirCopx = isFirCopx;
            DepartureAirportOrFixBefore = departureAirportOrFixBefore;
            DepartureRunway = departureRunway;
            CoordinationFix = coordinationFix;
            ArrivalAirportOrFixAfter = arrivalAirportOrFixAfter;
            ArrivalRunway = arrivalRunway;
            FromSector = fromSector;
            ToSector = toSector;
            ClimbLevel = climbLevel;
            DescendLevel = descendLevel;
            Name = name;
        }

        public bool IsFirCopx { get; }
        public string DepartureAirportOrFixBefore { get; }
        public string DepartureRunway { get; }
        public string CoordinationFix { get; }
        public string ArrivalAirportOrFixAfter { get; }
        public string ArrivalRunway { get; }
        public string FromSector { get; }
        public string ToSector { get; }
        public string ClimbLevel { get; }
        public string DescendLevel { get; }
        public string Name { get; }

        public override string GetCompileData(SectorElementCollection elements)
        {
            return
                $"{(this.IsFirCopx ? "FIR_COPX" : "COPX")}:{this.DepartureAirportOrFixBefore}:{this.DepartureRunway}:{this.CoordinationFix}:{this.ArrivalAirportOrFixAfter}:{this.ArrivalRunway}:{this.FromSector}:{this.ToSector}:{this.ClimbLevel}:{this.DescendLevel}:{this.Name}";
        }
    }
}
