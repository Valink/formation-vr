using System.Collections.Generic;
using app.bowling.score;
using UnityEngine;

namespace Valink.app.bowling
{
    public class ScoreDisplayer : MonoBehaviour
    {
        [SerializeField] private Transform framesContainer;
        [SerializeField] private FrameUI framePrefab;
        private List<FrameUI> _framesUI;

        public void Setup(int frameNumber)
        {
            _framesUI = new List<FrameUI>();
            for (var frameIndex = 1; frameIndex <= frameNumber; frameIndex++)
            {
                var frameUI = Instantiate(framePrefab, framesContainer);
                frameUI.SetIndex(frameIndex);
                _framesUI.Add(frameUI);
            }
        }

        public void UpdateFrames(List<Frame> frames)
        {
            frames.ForEach(frame =>
            {
                _framesUI[frame.Index - 1].SetScores(frame);
            });
        }
    }
}
