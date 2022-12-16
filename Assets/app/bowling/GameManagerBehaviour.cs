using System.Collections.Generic;
using System.Threading.Tasks;
using app.bowling.logic;
using app.bowling.pin;
using app.bowling.room;
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
        [SerializeField] private BallSpawnerBehaviour ballSpawnerBehaviour;
        [SerializeField] private Follower ballCamera;
        [SerializeField] private BallDetectorBehaviour ballDetectorBehaviour;
        [SerializeField] private LaneSizer laneSizer;
        [SerializeField] private int pinRows = 4;

        private List<PinBehaviour> _currentFramePins;
        private bool _isCompleted;
        private GameObject _currentBall;
        private static readonly Vector3 BallCameraOffset = new(0, .5f, -.5f);
        private static readonly Vector3 BallCameraRotation = new(25, 0, 0);

        [Header("Debug")] [SerializeField] private bool mockRoll;
        [SerializeField] private bool mockGutter;
        [SerializeField] private bool mockRestingPins;

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
            var restingPinsNumber = _game.CurrentFrame.PinCount - (_game.CurrentFrame.Rolls[0] ?? 0);
            if (mockRoll)
            {
                mockRoll = false;
                var droppedPinNumber = Random.Range(1, restingPinsNumber);
                ProcessDroppedPins(droppedPinNumber);
            }
            else if (mockGutter)
            {
                mockGutter = false;
                ProcessDroppedPins(0);
            }
            else if (mockRestingPins)
            {
                mockRestingPins = false;
                ProcessDroppedPins(restingPinsNumber);
            }
        }

        public void SkipRoll()
        {
            ProcessRoll(0);
        }

        private void InitGame(int frameNumber)
        {
            _isCompleted = false;
            var pinsNumber = pinRows * (pinRows + 1) / 2;
            _game = new Game(frameNumber, pinsNumber);
            scoreDisplayerBehaviour.Init(_game);
            SpawnBall();
            SetupFrame();
        }

        private void SpawnBall()
        {
            _currentBall = ballSpawnerBehaviour.SpawnBall();

            ballCamera.positionTarget = _currentBall.transform.GetChild(0);
        }

        private void SetupFrame()
        {
            pinSpawnerBehaviour.RemovePins();
            laneSizer.SetupLaneFor(pinRows);
            _currentFramePins = pinSpawnerBehaviour.SpawnPins(pinRows);
        }

        private void OnGameCompleted(Game game)
        {
            _isCompleted = true;
        }

        private void OnBallEntered()
        {
            ProcessRoll();
        }

        private async Task ProcessRoll(float delayInSeconds = 2f)
        {
            await Task.Delay((int)delayInSeconds * 1000);

            ballSpawnerBehaviour.DestroyAllBalls();
            SpawnBall();

            var droppedPinsNumber = GetDroppedPinsNumber();

            ProcessDroppedPins(droppedPinsNumber);
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