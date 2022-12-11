using app.bowling.logic;
using TMPro;
using UnityEngine;

namespace app.bowling
{
    internal class FrameUIBehaviour : MonoBehaviour
    {
        [SerializeField] private Transform rolls;
        [SerializeField] private RollUIBehaviour rollUiBehaviour;
        [SerializeField] private TMP_Text index;
        [SerializeField] private TMP_Text cumulativeScore;

        public void SetScores(Frame frame)
        {
            cumulativeScore.text = frame.CumulativeScore.ToString();

            foreach (Transform roll in rolls)
            {
                Destroy(roll.gameObject);
            }
            
            foreach (var roll in frame.Rolls)
            {
                var rollUi = Instantiate(rollUiBehaviour, rolls);
                rollUi.SetText(roll);
            }
        }

        public void SetIndex(int frameIndex)
        {
            index.text = frameIndex.ToString();
        }
    }
}