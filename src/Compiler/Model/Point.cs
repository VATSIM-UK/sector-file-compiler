﻿namespace Compiler.Model
{
    public class Point
    {
        // The different types of point
        public const int TypeCoordinate = 0;
        public const int TypeIdentifier = 1;

        public Coordinate Coordinate { get; }
        public string Identifier { get; }

        public Point(Coordinate coordinate)
        {
            this.Coordinate = coordinate;
        }

        public Point(string identifier)
        {
            this.Identifier = identifier;
        }

        public override bool Equals(object obj)
        {
            return (obj is Point) &&
                this.Identifier == ((Point) obj).Identifier &&
                this.Coordinate.Equals(((Point)obj).Coordinate);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public int Type()
        {
            return this.Identifier == null ? Point.TypeCoordinate : Point.TypeIdentifier;
        }

        public override string ToString()
        {
            return this.Identifier != null
                ? this.Identifier + " " + this.Identifier
                : this.Coordinate.ToString();
        }
    }
}
