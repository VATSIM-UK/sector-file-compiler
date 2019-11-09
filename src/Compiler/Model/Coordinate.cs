namespace Compiler.Model
{
    public struct Coordinate
    {
        public readonly string latitude;
        public readonly string longitude;

        public Coordinate(string latitude, string longitude)
        {
            this.latitude = latitude;
            this.longitude = longitude;
        }

        public override string ToString()
        {
            return string.Format(
                "{0} {1}",
                this.latitude,
                this.longitude
            );
        }
    }
}
