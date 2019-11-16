using System;

namespace Compiler.Model
{
    public class Airport : AbstractSectorElement, ICompilable
    {
        public string Name { get; }
        public string Icao { get; }
        public Coordinate LatLong { get; }
        public string Frequency { get; }

        public Airport(string name, string icao, Coordinate latLong, string frequency, string comment) : base(comment)
        {
            this.Name = name;
            this.Icao = icao;
            this.LatLong = latLong;
            this.Frequency = frequency;
        }

        public string Compile()
        {
            if (this.Comment == "" || this.Comment == null)
            {
                return String.Format(
                    "{0} {1} {2} E ;{3}\r\n",
                    this.Icao,
                    this.Frequency,
                    this.LatLong.ToString(),
                    this.Name
                );
            }

            return String.Format(
                "{0} {1} {2} E{3} - {4}\r\n",
                this.Icao,
                this.Frequency,
                this.LatLong.ToString(),
                this.CompileComment(),
                this.Name
            );
        }
    }
}
