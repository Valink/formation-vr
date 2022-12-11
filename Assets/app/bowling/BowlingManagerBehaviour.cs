using System.Collections;
using app.bowling.logic;
using UnityEngine;

namespace app.bowling
{
    public class BowlingManagerBehaviour : MonoBehaviour
    {
        [SerializeField] private Game _game;
        [SerializeField] private ScoreDisplayerBehaviour scoreDisplayerBehaviour;
        [SerializeField] private PinSpawnerBehaviour pinSpawnerBehaviour;
        private int _currentFrameDroppedPinNumber = 0;

        [SerializeField] private BallDetectorBehaviour ballDetectorBehaviour;
        [SerializeField] private GameObject ball;

        private void Awake()
        {
            const int frameNumber = 10;
            _game = new Game(frameNumber);
            scoreDisplayerBehaviour.Setup(_game.Frames.Count);
            
            SetupFrame();
            
            ballDetectorBehaviour.OnBallReachLaneEnd += OnBallReachLaneEnd;
        }

        private void OnBallReachLaneEnd()
        {
            StartCoroutine(ComputeScoreAfterTime(3));
        }

        private IEnumerator ComputeScoreAfterTime(float time)
        {
            yield return new WaitForSeconds(time);

            _game.Roll(_currentFrameDroppedPinNumber);
            _currentFrameDroppedPinNumber = 0;

            scoreDisplayerBehaviour.UpdateFrames(_game.Frames);

            pinSpawnerBehaviour.RemoveDroppedPins();
            
            if (_game.CurrentFrame.IsFrameComplete())
            {
                SetupFrame();
            }
        }
        
        private void SetupFrame()
        {
            var currentFramePins = pinSpawnerBehaviour.SpawnPins(4);
            currentFramePins.ForEach(p => p.OnPinDropped += OnPinDropped);
        }

        private void OnPinDropped()
        {
            _currentFrameDroppedPinNumber++;
        }
    }
}
