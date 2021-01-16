using Bogus;
using Compiler.Model;

namespace CompilerTest.Bogus.Factory
{
    static class CoordinateFactory
    {
        private static readonly string[] Latitudes =
        {
            "N052.28.42.000",
            "N053.24.22.000",
            "N053.28.14.000",
            "N053.38.45.000",
            "N054.18.20.001",
            "N053.31.08.531",
            "N050.50.25.256",
            "N054.59.06.041",
            "N051.22.15.000"
        };

        private static readonly string[] Longitudes =
        {
            "E002.34.14.000",
            "W003.26.43.000",
            "E002.25.41.322",
            "W001.21.37.000",
            "W001.09.25.000",
            "W004.51.09.412",
            "E003.53.45.000",
            "W003.35.14.321",
            "E001.33.31.521"
        };
    

        
        public static Coordinate Make()
        {
            Faker faker = new();
            return new Coordinate(
                faker.PickRandom(Latitudes),
                faker.PickRandom(Longitudes)
            );
        }
    }
}
