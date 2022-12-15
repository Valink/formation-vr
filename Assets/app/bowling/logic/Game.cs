using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace app.bowling.logic
{
    public class Game
    {
        public readonly List<Frame> Frames;
        public Frame CurrentFrame;

        public delegate void FrameCompletedHandler(Frame frame);

        public event FrameCompletedHandler FrameCompleted;

        public delegate void GameCompletedHandler(Game game);

        public event GameCompletedHandler GameCompleted;

        public Game(int frameNumber)
        {
            Frames = new List<Frame>();
            for (var i = 1; i <= frameNumber - 1; i++)
            {
                Frames.Add(new Frame(i, 10, 2));
            }

            Frames.Add(new Frame(10, 10, 3));

            CurrentFrame = Frames.First();
        }

        public void Roll(int droppedPinNumber)
        {
            CurrentFrame.Roll(droppedPinNumber);

            if (!CurrentFrame.IsComplete()) return;

            FrameCompleted?.Invoke(CurrentFrame);

            if (IsFrameLastOne(CurrentFrame))
            {
                GameCompleted?.Invoke(this);
            }
            else
            {
                CurrentFrame = GetFrameAfter(CurrentFrame);
            }
        }

        private Frame GetFrameAfter(Frame frame)
        {
            var frameIndex = Frames.IndexOf(frame);
            return Frames[frameIndex + 1];
        }

        private bool IsFrameLastOne(Frame frame)
        {
            return Frames.Last() == frame;
        }

        public int? ComputeScore()
        {
            int? score = 0;
            Frames.ForEach(frame =>
            {
                score += GetScoreForFrame(frame);
                frame.CumulativeScore = score;
            });
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
                frameScore = (frame.Rolls[0] ?? 0) + (frame.Rolls[1] ?? 0); // TODO method for this ?
            }

            return frameScore;
        }

        private int GetSpareBonus(Frame spareFrame)
        {
            var nextRollFrameAndIndexInFrame = GetNextRollFrameAndIndexInFrame(spareFrame, 1);
            var frame = nextRollFrameAndIndexInFrame.Frame;
            var rollIndexInFrame = nextRollFrameAndIndexInFrame.RollIndexInFrame;

            return frame.Rolls[rollIndexInFrame] ?? 0;
        }

        private int GetStrikeBonus(Frame strikeFrame)
        {
            var nextRollFrameAndIndexInFrame = GetNextRollFrameAndIndexInFrame(strikeFrame, 0);
            var frameA = nextRollFrameAndIndexInFrame.Frame;
            var rollIndexInFrameA = nextRollFrameAndIndexInFrame.RollIndexInFrame;

            var nextNextRollFrameAndIndexInFrame = GetNextRollFrameAndIndexInFrame(frameA,
                rollIndexInFrameA);
            var frameB = nextNextRollFrameAndIndexInFrame.Frame;
            var rollIndexInFrameB = nextNextRollFrameAndIndexInFrame.RollIndexInFrame;

            var nextRoll = frameA.Rolls[rollIndexInFrameA] ?? 0;
            var nextNextRoll = frameB.Rolls[rollIndexInFrameB] ?? 0;
            return nextRoll + nextNextRoll;
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