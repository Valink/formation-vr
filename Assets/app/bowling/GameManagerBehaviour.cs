using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app.bowling.logic;
using app.bowling.pin;
using UnityEngine;

namespace app.bowling
{
    public class GameManagerBehaviour : MonoBehaviour
    {
        private Game _game;
        [SerializeField] private ScoreDisplayerBehaviour scoreDisplayerBehaviour;
        [SerializeField] private PinSpawnerBehaviour pinSpawnerBehaviour;

        [SerializeField] private BallDetectorBehaviour ballDetectorBehaviour;
        [SerializeField] private GameObject ball;
        private List<PinBehaviour> _currentFramePins;

        private void Awake()
        {
            const int frameNumber = 10;
            _game = new Game(frameNumber);
            scoreDisplayerBehaviour.Setup(_game.Frames);

            _game.GameCompleted += OnGameCompleted;
            ballDetectorBehaviour.BallEntered += OnBallEntered;

            SetupFrame();
        }

        private void SetupFrame()
        {
            pinSpawnerBehaviour.RemovePins();
            _currentFramePins = pinSpawnerBehaviour.SpawnPins(4);
        }

        private void OnGameCompleted(Game game)
        {
            Debug.Log("Game Completed");
        }

        private async void OnBallEntered()
        {
            await Task.Delay(5000);

            var droppedPinsNumber = 0;
            foreach (var pin in _currentFramePins)
            {
                if (pin != null && pin.isDropped)
                {
                    droppedPinsNumber++;
                    Destroy(pin.gameObject);
                }
            }

            var gameCurrentFrame = _game.CurrentFrame;
            _game.Roll(droppedPinsNumber);
            _game.ComputeScore();
            
            scoreDisplayerBehaviour.UpdateFrame(gameCurrentFrame);

            if (gameCurrentFrame.IsComplete())
            {
                SetupFrame();
            }

            Debug.Log("Test3");
        }
    }
}