using System.Collections.Generic;
using app.bowling.logic;
using UnityEngine;

namespace app.bowling
{
    public class ScoreDisplayerBehaviour : MonoBehaviour
    {
        [SerializeField] private Transform framesContainer;
        [SerializeField] private FrameUIBehaviour framePrefab;
        private List<FrameUIBehaviour> _frameUis;

        public void Init(Game game)
        {
            _frameUis = new List<FrameUIBehaviour>();
            foreach (var frame in game.Frames)
            {
                var frameUI = Instantiate(framePrefab, framesContainer);
                frameUI.Init(frame);
                _frameUis.Add(frameUI);
            }
        }

        public void UpdateDisplay(Game game)
        {
            foreach (var frame in game.Frames)
            {
                _frameUis[frame.Index-1].SetScores(frame);
            }
        }
    }
}
