using TMPro;
using UnityEngine;

namespace app.bowling
{
    internal class RollUIBehaviour : MonoBehaviour
    {
        [SerializeField] private TMP_Text droppedPinNumber;

        public void SetText(int droppedPinNumber)
        {
            this.droppedPinNumber.text = droppedPinNumber.ToString();
        }
    }
}