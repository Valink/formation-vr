using TMPro;
using UnityEngine;

namespace app.bowling
{
    internal class RollUIBehaviour : MonoBehaviour
    {
        [SerializeField] private TMP_Text droppedPinNumberText;

        public void SetRoll(int? droppedPinNumber)
        {
            droppedPinNumberText.text = droppedPinNumber.HasValue ? droppedPinNumber.Value.ToString() : " ";
        }
    }
}