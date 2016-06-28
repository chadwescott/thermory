using System;

namespace Thermory.Domain.Utils
{
    public static class LumberCalculations
    {
        /// <summary>
        /// Calculates the linear feet for the given number of pieces of lumber of the given length.
        /// </summary>
        /// <param name="pieces"></param>
        /// <param name="lengthInInches">Length in inches</param>
        /// <returns>Linear Feet</returns>
        public static double GetLinearFeet(int pieces, double lengthInInches)
        {
            return Math.Round(pieces*lengthInInches/12, 0);
        }

        /// <summary>
        /// Calculates the square feet given the linear feet and the width.
        /// </summary>
        /// <param name="linearFeet"></param>
        /// <param name="widthInInches">Width in inches</param>
        /// <returns>Square Feet</returns>
        public static double GetSquareFeet(double linearFeet, double widthInInches)
        {
            return Math.Round(linearFeet * widthInInches / 12, 0);
        }
    }
}
