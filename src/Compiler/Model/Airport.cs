using System.IO;

namespace Compiler.Model
{
    public class Airport : AbstractCompilableElement
    {
        public string Name { get; }
        public string Icao { get; }
        public Coordinate LatLong { get; }
        public string Frequency { get; }

        public Airport(
            string name,
            string icao,
            Coordinate latLong,
            string frequency,
            Definition definition,
            Docblock docblock,
            Comment inlineComment
        ) : base(definition, docblock, inlineComment)
        {
            this.Name = name;
            this.Icao = icao;
            this.LatLong = latLong;
            this.Frequency = frequency;
        }

        /*
         * Airports compile in a special way - they combine 4 lines of Basic data with the ICAO
         * from the folder containing the airport data. They also put the airfield name
         * on the end as a comment.
         * 
         * Therefore, we disregard any comments or docblocks in this section.
         */
        public override void Compile(SectorElementCollection elements, TextWriter output)
        {
            output.WriteLine(
                $"{this.GetCompileData(elements)} ;{this.Name}"
            );
        }

        public override string GetCompileData(SectorElementCollection elements)
        {
            return $"{this.Icao} {this.Frequency} {this.LatLong.ToString()} E";
        }
    }
}
