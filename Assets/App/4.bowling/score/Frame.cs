using System.Linq;
using UnityEngine;

namespace App._4.bowling.score
{
    internal class Frame
    {
        public readonly int[] Rolls;
        private int _currentRoll;
        public readonly int PinCount;
        private readonly int _index; // for debug

        public Frame(int index, int pinCount, int rollNumber)
        {
            _index = index;
            PinCount = pinCount;
            Rolls = new int[rollNumber];
            _currentRoll = 0;
        }

        public void Roll(int droppedPinNumber)
        {
            Rolls[_currentRoll++] = droppedPinNumber;
        }

        public bool IsFrameComplete()
        {
            return AllRollsDone() || (
                IsStrike() && Rolls.Length == 2
            );
        }

        private bool AllRollsDone()
        {
            return _currentRoll == Rolls.Length;
        }

        public bool IsSpare()
        {
            return Rolls[0] + Rolls[1] == 10;
        }

        public bool IsStrike()
        {
            return Rolls.First() == 10;
        }

        public int GetFirstRoll()
        {
            return Rolls.First();
        }

        public override string ToString()
        {
            return Rolls.Aggregate($"Frame n°{_index} :", (current, roll) => current + $" {roll}");
        }

        public int? GetNextRollIndex(int rollInFrameIndex)
        {
            if (IsIndexIsLast(rollInFrameIndex))
            {
                return null;
            }
            else
            {
                if (IsStrike())
                {
                    if (Rolls.Length == 3)
                    {
                        return rollInFrameIndex + 1;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return rollInFrameIndex + 1;
                }
            }
        }

        private bool IsIndexIsLast(int rollInFrameIndex)
        {
            return rollInFrameIndex == Rolls.Length - 1;
        }
    }
}