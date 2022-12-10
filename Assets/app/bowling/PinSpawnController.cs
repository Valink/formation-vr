using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Valink.app.bowling
{
    public class PinSpawnController : MonoBehaviour
    {
        [SerializeField] private Button spawnButton;
        [SerializeField] private Button lessPinRowButton;
        [SerializeField] private Button morePinRowButton;
        [SerializeField] private int pinRowNumber;
        [SerializeField] private TMP_Text pinRowNumberText;
        [SerializeField] private PinPositioner pinPositioner;

        private void Awake()
        {
            spawnButton.onClick.AddListener(SpawnPins);
            lessPinRowButton.onClick.AddListener(LessPinRow);
            morePinRowButton.onClick.AddListener(MorePinRow);
            UpdatePinRowNumberText();
        }

        private void MorePinRow()
        {
            pinRowNumber++;
            UpdatePinRowNumberText();
        }

        private void UpdatePinRowNumberText()
        {
            pinRowNumberText.text = pinRowNumber.ToString();
        }

        private void LessPinRow()
        {
            pinRowNumber--;
            UpdatePinRowNumberText();
        }

        private void SpawnPins()
        {
            pinPositioner.RemovePins();
            var pinRowNumber = int.Parse(pinRowNumberText.text);
            pinPositioner.SpawnPins(pinRowNumber);
        }
    }
}
 