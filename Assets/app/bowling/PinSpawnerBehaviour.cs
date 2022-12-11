using System.Collections.Generic;
using app.bowling.pin;
using UnityEngine;

namespace app.bowling
{
    public class PinSpawnerBehaviour : MonoBehaviour
    {
        [SerializeField] private PinBehaviour pinPrefab;
        [SerializeField] private Transform pinParent;
        [SerializeField] private float pinXDistance = .2635f;
        [SerializeField] private float pinZDistance = .305f;
        private List<PinBehaviour> _pins;
    
        // inspector section
        [Header("Manual pins spawn")]
        [SerializeField] private int pinRowCount = 4;
        [SerializeField] private bool spawnPins = false;

        public List<PinBehaviour> SpawnPins(int pinRowNumber)
        {
            _pins = new List<PinBehaviour>();
            for (var pinRowIndex = pinRowNumber; pinRowIndex >= 0; pinRowIndex--)
            {
                var pinsRow = SpawnRow(pinRowIndex, pinRowNumber);
                _pins.AddRange(pinsRow);
            }
            return _pins;
        }

        private List<PinBehaviour> SpawnRow(int pinRowIndex, int pinRowNumber)
        {
            var pins = new List<PinBehaviour>();
            for (var pinIndexInRow = 0; pinIndexInRow < pinRowIndex; pinIndexInRow++)
            {
                var pin = SpawnPin(pinRowIndex, pinIndexInRow, pinRowNumber);
                pins.Add(pin);
            }
            return pins;
        }

        private PinBehaviour SpawnPin(int pinRowIndex, int pinIndexInRow, int pinRowNumber)
        {
            var pin = Instantiate(pinPrefab, pinParent.transform);
            pin.name = pinRowIndex + " - " + pinIndexInRow;
            pin.transform.localPosition = new Vector3(
                pinIndexInRow * pinXDistance - (pinRowIndex - 1) * pinXDistance / 2,
                0,
                (pinRowNumber - pinRowIndex) * pinZDistance
            );
            return pin;
        }

        private void Update()
        {
            if (!spawnPins) return;
            spawnPins = false;
            SpawnPins(pinRowCount);
        }

        public void RemovePins()
        {
            foreach (Transform pin in pinParent)
            {
                Destroy(pin.gameObject);
            }
        }
        
        public void RemoveDroppedPins()
        {
            foreach (Transform pin in pinParent)
            {
                var pinGo = pin.GetComponent<PinBehaviour>();
                if (pinGo.isDropped)
                {
                    Destroy(pin.gameObject);
                }
            }
        }
    }
}