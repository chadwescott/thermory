using System;
using System.Linq;

namespace Thermory.Domain.Utils
{
    public static class HtmlHelpers
    {
        public static string GetPacksHtml(int pieces, int bundleSize)
        {
            if (pieces == 0) return "0";
            var fullPacks = pieces / bundleSize;
            var remainingPacks = Math.Abs(pieces % bundleSize);
            var numbers = new[] { remainingPacks, bundleSize };
            SimplifyFraction(numbers);
            var numerator = numbers[0];
            var denominator = numbers[1];
            var remainingPacksFraction = pieces%bundleSize == 0
                ? ""
                : string.Format("<sup>{0}</sup>&frasl;<sub>{1}</sub>", numerator, denominator);
            var fullPackString = fullPacks == 0 && remainingPacksFraction != string.Empty ? "" : fullPacks.ToString();
            return string.Format("{0}{1}", fullPackString, remainingPacksFraction);
        }

        private static void SimplifyFraction(int[] numbers)
        {
            var gcd = CalculateGreatestCommonDenominator(numbers);
            for (var i = 0; i < numbers.Length; i++)
                numbers[i] /= gcd;
        }
        private static int CalculateGreatestCommonDenominator(int a, int b)
        {
            while (b > 0)
            {
                var remainder = a % b;
                a = b;
                b = remainder;
            }
            return a;
        }
        private static int CalculateGreatestCommonDenominator(int[] args)
        {
            return args.Aggregate(CalculateGreatestCommonDenominator);
        }
    }
}
