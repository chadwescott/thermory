using System;

namespace Thermory.Domain.Utils
{
    public static class LumberCalculations
    {
        /// <summary>
        /// Calculates the linear feet for the given number of pieces of lumber of the given length.
        /// </summary>
        /// <param name="pieces"></param>
        /// <param name="length">Length in inches</param>
        /// <returns>Linear Feet</returns>
        public static double GetLinearFeet(int pieces, double length)
        {
            return Math.Round(pieces*length/12, 0);
        }

        /// <summary>
        /// Calculates the square feet given the linear feet and the width.
        /// </summary>
        /// <param name="linearFeet"></param>
        /// <param name="width">Width in inches</param>
        /// <returns>Square Feet</returns>
        public static double GetSquareFeet(double linearFeet, double width)
        {
            return Math.Round(linearFeet * width / 12, 0);
        }
    }
}
