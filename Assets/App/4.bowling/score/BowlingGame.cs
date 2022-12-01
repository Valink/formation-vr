using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace App._4.bowling.score
{
    internal class BowlingGame
    {
        private readonly List<Frame> _frames;
        private Frame _currentFrame;

        public BowlingGame(int frameNumber)
        {
            _frames = new List<Frame>();
            for (var i = 1; i <= frameNumber - 1; i++)
            {
                _frames.Add(new Frame(i, 10, 2));
            }

            _frames.Add(new Frame(10, 10, 3));

            _currentFrame = _frames.First();
        }

        public void Roll(int droppedPinNumber)
        {
            _currentFrame.Roll(droppedPinNumber);

            if (!_currentFrame.IsFrameComplete()) return;
            if (!IsFrameLastOne(_currentFrame))
            {
                _currentFrame = GetFrameAfter(_currentFrame);
            }
        }

        private Frame GetFrameAfter(Frame frame)
        {
            var frameIndex = _frames.IndexOf(frame);
            return _frames[frameIndex + 1];
        }

        private bool IsFrameLastOne(Frame frame)
        {
            return _frames.Last() == frame;
        }

        public double ComputeScore()
        {
            var score = 0;
            foreach (var frame in _frames)
            {
                score += GetScoreForFrame(frame);
                Debug.Log("Frame " + frame + " score: " + GetScoreForFrame(frame));
            }
            return score;
        }

        private int GetScoreForFrame(Frame frame)
        {
            int frameScore;
            
            if (frame.IsStrike())
            {
                frameScore = frame.PinCount + GetStrikeBonus(frame);
            }
            else if (frame.IsSpare())
            {
                frameScore = frame.PinCount + GetSpareBonus(frame);
            }
            else
            {
                frameScore = frame.Rolls[0] + frame.Rolls[1]; // TODO frame.sum ?
            }

            return frameScore;
        }

        private int GetSpareBonus(Frame spareFrame)
        {
            var nextFrame = GetFrameAfter(spareFrame);
            return nextFrame.GetFirstRoll();
        }

        private int GetStrikeBonus(Frame strikeFrame)
        {
            var nextRollFrameAndFrameIndex = GetNextRollFrameAndIndexInFrame(strikeFrame, 0);
            var nextNextRollFrameAndFrameIndex = GetNextRollFrameAndIndexInFrame(nextRollFrameAndFrameIndex.Frame, nextRollFrameAndFrameIndex.RollIndexInFrame);
            
            return nextRollFrameAndFrameIndex.Frame.Rolls[nextRollFrameAndFrameIndex.RollIndexInFrame] +
                   nextNextRollFrameAndFrameIndex.Frame.Rolls[nextNextRollFrameAndFrameIndex.RollIndexInFrame];
        }

        private RollFrameAndIndexInFrame GetNextRollFrameAndIndexInFrame(Frame frame, int rollIndexInFrame)
        {
            var nextRollIndexInFrame = frame.GetNextRollIndex(rollIndexInFrame);
            if (nextRollIndexInFrame != null)
            {
                return new RollFrameAndIndexInFrame(frame, (int)nextRollIndexInFrame);
            }
            else
            {
                return new RollFrameAndIndexInFrame(GetFrameAfter(frame), 0);
            }
        }
    }
}