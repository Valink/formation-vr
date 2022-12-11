using System.Collections.Generic;
using System.Linq;

namespace app.bowling.score
{
    public class BowlingGame
    {
        public readonly List<Frame> Frames;
        public Frame CurrentFrame;

        public BowlingGame(int frameNumber)
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

            if (!CurrentFrame.IsFrameComplete()) return;
            if (!IsFrameLastOne(CurrentFrame))
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

        public double ComputeScore()
        {
            var score = 0;
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
                frameScore = frame.Rolls[0] + frame.Rolls[1];
            }

            return frameScore;
        }

        private int GetSpareBonus(Frame spareFrame)
        {
            var nextRollFrameAndIndexInFrame = GetNextRollFrameAndIndexInFrame(spareFrame, 1);
            return nextRollFrameAndIndexInFrame.Frame.Rolls[nextRollFrameAndIndexInFrame.RollIndexInFrame];
        }

        private int GetStrikeBonus(Frame strikeFrame)
        {
            var nextRollFrameAndIndexInFrame = GetNextRollFrameAndIndexInFrame(strikeFrame, 0);
            var nextNextRollFrameAndIndexInFrame = GetNextRollFrameAndIndexInFrame(nextRollFrameAndIndexInFrame.Frame,
                nextRollFrameAndIndexInFrame.RollIndexInFrame);

            return nextRollFrameAndIndexInFrame.Frame.Rolls[nextRollFrameAndIndexInFrame.RollIndexInFrame] +
                   nextNextRollFrameAndIndexInFrame.Frame.Rolls[nextNextRollFrameAndIndexInFrame.RollIndexInFrame];
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