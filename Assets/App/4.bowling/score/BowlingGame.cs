using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace App._4.bowling.score
{
    internal class Frame
    {
        public readonly int[] Rolls;
        private int _currentRoll = 0;
        public readonly int Index;

        public Frame(int index, int rollNumber)
        {
            Index = index;
            Rolls = new int[rollNumber];
        }

        public void Roll(int droppedPinNumber)
        {
            Rolls[_currentRoll++] = droppedPinNumber;
        }

        public bool IsFrameComplete()
        {
            return _currentRoll == Rolls.Length || (
                IsStrike() && Rolls.Length == 2
            );
        }

        public bool IsSpare()
        {
            return Rolls[0] + Rolls[1] == 10;
        }

        public bool IsStrike()
        {
            return Rolls[0] == 10;
        }

        public int GetScore()
        {
            return Rolls[0] + Rolls[1];
        }

        public int GetFirstRoll()
        {
            return Rolls[0];
        }

        public override string ToString()
        {
            return Rolls.Aggregate($"Frame n°{Index} :", (current, roll) => current + $" {roll}");
        }
    }

    internal class BowlingGame
    {
        private readonly List<Frame> _frames;
        private Frame _currentFrame;

        public BowlingGame()
        {
            _frames = new List<Frame>();
            for (var i = 1; i <= 9; i++)
            {
                _frames.Add(new Frame(i, 2));
            }

            _frames.Add(new Frame(10, 3));

            _currentFrame = _frames[0];
        }

        public void Roll(int droppedPinNumber)
        {
            _currentFrame.Roll(droppedPinNumber);

            if (!_currentFrame.IsFrameComplete()) return;
            if (!IsFrameIsLastOne(_currentFrame))
            {
                _currentFrame = GetFrameAfter(_currentFrame);
            }
        }

        private Frame GetFrameAfter(Frame frame)
        {
            var frameIndex = _frames.IndexOf(frame);
            return _frames[frameIndex + 1];
        }

        private bool IsFrameIsLastOne(Frame frame)
        {
            var frameIndex = _frames.IndexOf(frame);
            return frameIndex == _frames.Count - 1;
        }

        public double ComputeScore()
        {
            var score = 0;
            for (var frameIndex = 0; frameIndex < _frames.Count; frameIndex++)
            {
                var frame = _frames[frameIndex];

                var frameScore = 0;
                if (frame.IsStrike())
                {
                    frameScore = 10 + GetStrikeBonus(frame);
                }
                else if (frame.IsSpare())
                {
                    frameScore = 10 + GetSpareBonus(frame);
                }
                else
                {
                    frameScore = frame.GetScore();
                }

                score += frameScore;
            }

            return score;
        }

        private int GetSpareBonus(Frame spareFrame)
        {
            var nextFrame = GetFrameAfter(spareFrame);
            return nextFrame.GetFirstRoll();
        }

        private int GetStrikeBonus(Frame strikeFrame)
        {
            return GetTwoNextRollSum(strikeFrame);
        }

        private int GetTwoNextRollSum(Frame frame) // TODO rn
        {
            int score;

            if (IsFrameIsLastOne(frame))
            {
                score = frame.Rolls[1] + frame.Rolls[2];
            }
            else
            {
                var nextFrame = GetFrameAfter(frame);
                if (nextFrame.IsStrike())
                {
                    if (IsFrameIsLastOne(nextFrame))
                    {
                        score = 10 + nextFrame.Rolls[1];
                    }
                    else
                    {
                        var nextNextFrame = GetFrameAfter(nextFrame);
                        score = 10 + nextNextFrame.Rolls[0];
                    }
                }
                else
                {
                    score = nextFrame.Rolls[0] + nextFrame.Rolls[1];
                }
            }

            return score;
        }
    }
}