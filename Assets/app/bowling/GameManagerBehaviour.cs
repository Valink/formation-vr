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
        private static readonly Vector3 BallCameraOffset = new(0,.5f,-.5f);
        private static readonly Vector3 BallCameraRotation = new(25,0,0);

        [Header("Debug")]
        [SerializeField] private bool mockRoll;
        [SerializeField] private bool mockGutter;
        [SerializeField] private bool mockTen;
        
        private void Awake()
        {
            InitGame(10);
            
            InitBallCamera();

            _game.GameCompleted += OnGameCompleted;
            ballDetectorBehaviour.BallEntered += OnBallEntered;
        }

        private void InitBallCamera()
        {
            ballCamera.offset = BallCameraOffset;
            ballCamera.SetRotation(BallCameraRotation);
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

        public void SkipRoll()
        {
            ProcessRoll(0);
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
            
            ballCamera.positionTarget = _currentBall.transform.GetChild(0);
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

        private void OnBallEntered()
        {
            ProcessRoll();
        }

        private async Task ProcessRoll(int delayInSeconds = 4)
        {
            DestroyCurrentBall();

            await Task.Delay(delayInSeconds * 1000);

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