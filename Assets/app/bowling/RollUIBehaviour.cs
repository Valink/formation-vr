using TMPro;
using UnityEngine;

namespace app.bowling
{
    internal class RollUIBehaviour : MonoBehaviour
    {
        [SerializeField] private TMP_Text droppedPinNumberText;

        public void SetRoll(string roll)
        {
            droppedPinNumberText.text = roll;
        }
    }
}