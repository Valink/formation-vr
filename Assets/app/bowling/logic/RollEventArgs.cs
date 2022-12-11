using System.Collections.Generic;

namespace app.bowling.logic
{
    public class RollEventArgs
    {
        public List<Frame> Frames;

        public RollEventArgs(List<Frame> frames)
        {
            Frames = frames;
        }
    }
}