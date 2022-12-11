using System.Collections.Generic;

namespace app.bowling.score
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