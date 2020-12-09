namespace Compiler.Model
{
    public class SectorGuest : AbstractCompilableElement
    {
        public SectorGuest(
            string controller,
            string departureAirport,
            string arrivalAirport,
            Definition definition,
            Docblock docblock,
            Comment inlineComment
        ) : base(definition, docblock, inlineComment)
        {
            Controller = controller;
            DepartureAirport = departureAirport;
            ArrivalAirport = arrivalAirport;
        }

        public string Controller { get; }
        public string DepartureAirport { get; }
        public string ArrivalAirport { get; }

        public override bool Equals(object obj)
        {
            return obj is SectorGuest &&
                ((SectorGuest)obj).Controller == this.Controller &&
                ((SectorGuest)obj).ArrivalAirport == this.ArrivalAirport &&
                ((SectorGuest)obj).DepartureAirport == this.DepartureAirport;
        }

        public override string GetCompileData()
        {
            return string.Format(
                "GUEST:{0}:{1}:{2}",
                this.Controller,
                this.DepartureAirport,
                this.ArrivalAirport
            );
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
