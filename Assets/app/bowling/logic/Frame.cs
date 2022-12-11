using System.Linq;

namespace app.bowling.logic
{
    public class Frame
    {
        public readonly int[] Rolls;
        public int CurrentRollIndex;
        public readonly int PinCount;
        public readonly int Index;
        public int CumulativeScore;

        public Frame(int index, int pinCount, int rollNumber)
        {
            Index = index;
            PinCount = pinCount;
            Rolls = new int[rollNumber];
            CurrentRollIndex = 0;
        }

        public void Roll(int droppedPinNumber)
        {
            Rolls[CurrentRollIndex++] = droppedPinNumber;
        }

        public bool IsComplete()
        {
            return AllRollsDone() || (
                IsStrike() && Rolls.Length == 2
            );
        }

        private bool AllRollsDone()
        {
            return CurrentRollIndex == Rolls.Length;
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
            return Rolls.Aggregate($"Frame n°{Index} :", (current, roll) => current + $" {roll}");
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