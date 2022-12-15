using TMPro;
using UnityEngine;

namespace app.bowling.ui
{
    public class PinDroppedDisplay : MonoBehaviour
    {
        [SerializeField] private TMP_Text pinDroppedText;
        
        public void SetPinDroppedText(int pinsDropped)
        {
            pinDroppedText.text = pinsDropped.ToString();
        }

        public void UnsetPinDroppedText()
        {
            pinDroppedText.text = "";
        }
    }
}
