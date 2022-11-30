using NUnit.Framework;
using RangeAttribute = UnityEngine.RangeAttribute;

namespace App.core.range
{
    public class RangeModifierTest
    {
        private RangeAttribute _originRange;
        private RangeAttribute _destinationRange;

        private void Setup()
        {
            _originRange = new RangeAttribute(-1, 1);
            _destinationRange = new RangeAttribute(0, -100);
        }

        [Test]
        public void ShouldReturnDestRangeFirstBoundWhenGivingOriginRangeFirstBound()
        {
            Setup();
            var value = RangeModifier.GetValueFromRangeAToB(_originRange.min, _originRange,
                _destinationRange);
            Assert.AreEqual(_destinationRange.min, value);
        }

        [Test]
        public void ShouldReturnDestRangeSecondBoundWhenGivingOriginRangeSecondBound()
        {
            Setup();
            var value =
                RangeModifier.GetValueFromRangeAToB(_originRange.max, _originRange, _destinationRange);
            Assert.AreEqual(_destinationRange.max, value);
        }
        
        [Test]
        public void ShouldReturnDestRangeMidValueWhenGivingOriginRangeMidValue()
        {
            Setup();
            var middleOriginRangeValue = (_originRange.min + _originRange.max) / 2;
            var value = RangeModifier.GetValueFromRangeAToB(middleOriginRangeValue, _originRange,
                _destinationRange);

            var middleDestRangeValue = (_destinationRange.min + _destinationRange.max) / 2;
            Assert.AreEqual(middleDestRangeValue, value);
        }
    }
}