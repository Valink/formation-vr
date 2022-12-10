﻿namespace Valink.app.bowling.score
{
    internal class RollFrameAndIndexInFrame
    {
        public Frame Frame;
        public int RollIndexInFrame;
        
        public RollFrameAndIndexInFrame(Frame frame, int rollIndexInFrame)
        {
            Frame = frame;
            RollIndexInFrame = rollIndexInFrame;
        }
    }
}