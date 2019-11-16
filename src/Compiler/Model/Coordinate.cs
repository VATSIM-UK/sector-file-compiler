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

        public override bool Equals(object obj)
        {
            return (obj is Coordinate) &&
                (((Coordinate)obj).latitude == this.latitude) &&
                (((Coordinate)obj).longitude == this.longitude);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
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
