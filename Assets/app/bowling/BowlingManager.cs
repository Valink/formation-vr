using System.Collections;
using app.bowling.score;
using UnityEngine;

namespace Valink.app.bowling
{
    public class BowlingManager : MonoBehaviour
    {
        [SerializeField] private BowlingGame _bowlingGame;
        [SerializeField] private ScoreDisplayer scoreDisplayer;
        [SerializeField] private PinPositioner pinPositioner;
        private int _currentFrameDroppedPinNumber = 0;

        [SerializeField] private BallTrigger ballTrigger;
        [SerializeField] private GameObject ball;

        private void Awake()
        {
            const int frameNumber = 10;
            _bowlingGame = new BowlingGame(frameNumber);
            scoreDisplayer.Setup(_bowlingGame.Frames.Count);
            
            SetupFrame();
            
            ballTrigger.OnBallReachLaneEnd += OnBallReachLaneEnd;
        }

        private void OnBallReachLaneEnd()
        {
            StartCoroutine(ComputeScoreAfterTime(3));
        }

        private IEnumerator ComputeScoreAfterTime(float time)
        {
            yield return new WaitForSeconds(time);

            _bowlingGame.Roll(_currentFrameDroppedPinNumber);
            _currentFrameDroppedPinNumber = 0;

            scoreDisplayer.UpdateFrames(_bowlingGame.Frames);

            pinPositioner.RemoveDroppedPins();
            
            if (_bowlingGame.CurrentFrame.IsFrameComplete())
            {
                SetupFrame();
            }
        }
        
        private void SetupFrame()
        {
            var currentFramePins = pinPositioner.SpawnPins(4);
            currentFramePins.ForEach(p => p.OnPinDropped += OnPinDropped);
        }

        private void OnPinDropped()
        {
            _currentFrameDroppedPinNumber++;
        }
    }
}
