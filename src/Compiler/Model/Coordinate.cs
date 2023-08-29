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

        public static double DegreeMinSecToDecimalDegree(string latOrLong) {
            double output = 0;
            string[] sections = latOrLong.Split('.');
            output += int.Parse(sections[0].Substring(1));
            output += int.Parse(sections[1]) / 60.0d;
            output += int.Parse(sections[2]) / 3600.0d;
            output += int.Parse(sections[3]) / 3600000.0d;
            if (sections[0].StartsWith("S") || sections[0].StartsWith("W")) {
                output = -output;
            }
            return output;
        }

        public static string DecimalDegreeToDegreeMinSec(double decimalDegree, bool isLat) {
            string output = "";
            if (decimalDegree > 0) {
                if (isLat) output += "N";
                else output += "E";
            } else {
                decimalDegree = -decimalDegree;
                if (isLat) output += "S";
                else output += "W";
            }

            output += ((int)decimalDegree).ToString().PadLeft(3, '0') + ".";
            decimalDegree = decimalDegree - (int)decimalDegree;
            decimalDegree *= 60;
            output += ((int)decimalDegree).ToString().PadLeft(2, '0') + ".";
            decimalDegree = decimalDegree - (int)decimalDegree;
            decimalDegree *= 60;
            output += ((int)decimalDegree).ToString().PadLeft(2, '0') + ".";
            decimalDegree = decimalDegree - (int)decimalDegree;
            decimalDegree *= 1000;
            output += ((int)decimalDegree).ToString().PadLeft(3, '0');

            return output;
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
            return $"{this.latitude} {this.longitude}";
        }
    }
}
