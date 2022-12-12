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

        public void Setup(List<Frame> frames)
        {
            _framesUI = new List<FrameUIBehaviour>();
            foreach (var frame in frames)
            {
                var frameUI = Instantiate(framePrefab, framesContainer);
                frameUI.Setup(frame);
                _framesUI.Add(frameUI);
            }
        }

        public void UpdateFrame(Frame frame)
        {
            _framesUI[frame.Index - 1].SetScores(frame);
        }
    }
}
