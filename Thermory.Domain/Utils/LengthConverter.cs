using System;

namespace Thermory.Domain.Utils
{
    public static class LengthConverter
    {
        public const double InchesPerMillimeter = 0.0393700787;
        public const double InchesPerFeet = 12;

        public static double ConvertMillimetersToInches(int millimeters)
        {
            return Math.Round(millimeters*InchesPerMillimeter, 2);
        }

        public static double ConvertMillimetersToFeet(int millimeters)
        {
            var inches = ConvertMillimetersToInches(millimeters);
            return ConvertInchesToFeet(inches);
        }

        public static double ConvertInchesToFeet(double inches)
        {
            return Math.Round(inches / InchesPerFeet, 2);
        }
    }
}
