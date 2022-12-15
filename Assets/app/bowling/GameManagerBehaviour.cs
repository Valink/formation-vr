using System.Collections.Generic;
using System.Threading.Tasks;
using app.bowling.logic;
using app.bowling.pin;
using app.bowling.ui;
using Editor.transform;
using UnityEngine;

namespace app.bowling
{
    public class GameManagerBehaviour : MonoBehaviour
    {
        private Game _game;
        [SerializeField] private PinDroppedDisplay pinDroppedDisplay;
        [SerializeField] private ScoreDisplayerBehaviour scoreDisplayerBehaviour;
        [SerializeField] private PinSpawnerBehaviour pinSpawnerBehaviour;
        [SerializeField] private BallSpawner ballSpawner;
        [SerializeField] private Follower ballCamera;
        [SerializeField] private BallDetectorBehaviour ballDetectorBehaviour;
        
        private List<PinBehaviour> _currentFramePins;
        private bool _isCompleted;
        private GameObject _currentBall;

        [Header("Debug")]
        [SerializeField] private bool mockRoll;
        [SerializeField] private bool mockGutter;
        [SerializeField] private bool mockTen;
        
        private void Awake()
        {
            InitGame(10);

            _game.GameCompleted += OnGameCompleted;
            ballDetectorBehaviour.BallEntered += OnBallEntered;
        }

        private void Update()
        {
            if (mockRoll)
            {
                mockRoll = false;
                var max = _game.CurrentFrame.PinCount - (_game.CurrentFrame.Rolls[0] ?? 0);
                var droppedPinNumber = Random.Range(1, max);
                ProcessDroppedPins(droppedPinNumber);
            } else if (mockGutter)
            {
                mockGutter = false;
                ProcessDroppedPins(0);
            } else if (mockTen)
            {
                mockTen = false;
                ProcessDroppedPins(10);
            }
        }

        private void InitGame(int frameNumber)
        {
            _isCompleted = false;
            _game = new Game(frameNumber);
            scoreDisplayerBehaviour.Init(_game);
            SpawnBall();
            SetupFrame();
        }

        private void SpawnBall()
        {
            _currentBall = ballSpawner.SpawnBall();
            ballCamera.Target = _currentBall.transform;
        }

        private void SetupFrame()
        {
            pinSpawnerBehaviour.RemovePins();
            _currentFramePins = pinSpawnerBehaviour.SpawnPins(4);
        }

        private void OnGameCompleted(Game game)
        {
            _isCompleted = true;
        }

        private async void OnBallEntered()
        {
            await Task.Delay(5000);

            DestroyCurrentBall();
            SpawnBall();

            var droppedPinsNumber = GetDroppedPinsNumber();

            ProcessDroppedPins(droppedPinsNumber);
        }

        private void DestroyCurrentBall()
        {
            Destroy(_currentBall);
        }

        private int GetDroppedPinsNumber()
        {
            var droppedPinsNumber = 0;
            for (var i = _currentFramePins.Count - 1; i >= 0; i--)
            {
                var pin = _currentFramePins[i];
                if (pin.isDropped)
                {
                    RemovePin(pin);
                    droppedPinsNumber++;
                }
            }

            return droppedPinsNumber;
        }

        private void ProcessDroppedPins(int droppedPinsNumber)
        {
            pinDroppedDisplay.SetPinDroppedText(droppedPinsNumber);
            UnsetPinDroppedTextAfterDelay();

            var gameCurrentFrame = _game.CurrentFrame;
            _game.Roll(droppedPinsNumber);
            _game.ComputeScore();

            scoreDisplayerBehaviour.UpdateDisplay(_game);

            if (gameCurrentFrame.IsComplete())
            {
                if (_isCompleted)
                {
                    Debug.Log("Game Completed !!!");
                }
                else
                {
                    SetupFrame();
                }
            }
        }

        private async void UnsetPinDroppedTextAfterDelay()
        {
            await Task.Delay(3000);
            pinDroppedDisplay.UnsetPinDroppedText();
        }

        private void RemovePin(PinBehaviour pin)
        {
            _currentFramePins.Remove(pin);
            Destroy(pin.gameObject);
        }
    }
}