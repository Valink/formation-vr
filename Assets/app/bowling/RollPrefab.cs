using TMPro;
using UnityEngine;

namespace Valink.app.bowling
{
    internal class RollPrefab : MonoBehaviour
    {
        [SerializeField] private TMP_Text droppedPinNumber;

        public void SetText(int droppedPinNumber)
        {
            this.droppedPinNumber.text = droppedPinNumber.ToString();
        }
    }
}