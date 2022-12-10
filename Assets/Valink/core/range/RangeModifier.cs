using UnityEngine;

namespace Valink.App.core.range
{
    public static class RangeModifier
    {
        public static float GetValueFromRangeAToB(
            float valueInOriginRangeReferential,
            RangeAttribute originRange,
            RangeAttribute destinationRange
        )
        {
            var value = SubtractRangeOffset(valueInOriginRangeReferential, originRange);
            value = value * SubtractRangeOffset(destinationRange.max, destinationRange) /
                    SubtractRangeOffset(originRange.max, originRange);
            return AddRangeOffset(value, destinationRange);
        }

        private static float AddRangeOffset(float value, RangeAttribute range)
        {
            return value + range.min;
        }

        private static float SubtractRangeOffset(float value, RangeAttribute range)
        {
            return value - range.min;
        }
    }
}