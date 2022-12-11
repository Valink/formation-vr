using System.Collections.Generic;
using app.bowling.logic;
using UnityEngine;

namespace app.bowling
{
    public class ScoreDisplayerBehaviour : MonoBehaviour
    {
        [SerializeField] private Transform framesContainer;
        [SerializeField] private FrameUIBehaviour framePrefab;
        private List<FrameUIBehaviour> _framesUI;

        public void Setup(int frameNumber)
        {
            _framesUI = new List<FrameUIBehaviour>();
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
