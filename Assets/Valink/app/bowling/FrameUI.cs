using TMPro;
using UnityEngine;
using Valink.app.bowling.score;

namespace Valink.app.bowling
{
    internal class FrameUI : MonoBehaviour
    {
        [SerializeField] private Transform rolls;
        [SerializeField] private RollPrefab rollPrefab;
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
                var rollUi = Instantiate(rollPrefab, rolls);
                rollUi.SetText(roll);
            }
        }

        public void SetIndex(int frameIndex)
        {
            index.text = frameIndex.ToString();
        }
    }
}