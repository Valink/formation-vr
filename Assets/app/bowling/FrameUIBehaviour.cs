using System.Collections.Generic;
using System.Linq;
using app.bowling.logic;
using TMPro;
using UnityEngine;

namespace app.bowling
{
    internal class FrameUIBehaviour : MonoBehaviour
    {
        [SerializeField] private Transform rolls;
        [SerializeField] private RollUIBehaviour rollUiBehaviourPrefab;
        [SerializeField] private List<RollUIBehaviour> rollUis;
        [SerializeField] private TMP_Text index;
        [SerializeField] private TMP_Text cumulativeScore;

        public void Awake()
        {
            rollUis = new List<RollUIBehaviour>();
        }
        
        public void Init(Frame frame)
        {
            index.text = frame.Index.ToString();
            
            foreach (var _ in frame.Rolls)
            {
                var rollUi = Instantiate(rollUiBehaviourPrefab, rolls);
                rollUis.Add(rollUi);
            }
            
            SetScores(frame);
        }

        public void SetScores(Frame frame)
        {
            if (frame.Rolls.All(r => r.HasValue))
            {
                cumulativeScore.text = frame.CumulativeScore.ToString();
            }

            var rollIndexInFrame = 0;

            if (frame.IsStrike())
            {
                rollUis[rollIndexInFrame].SetRoll("X");
            }
            else if (frame.IsSpare())
            {
                rollUis[rollIndexInFrame++].SetRoll(frame.Rolls[0]?.ToString());
                rollUis[rollIndexInFrame].SetRoll("/");
            }
            else
            {
                foreach (var roll in frame.Rolls)
                {
                    rollUis[rollIndexInFrame++].SetRoll(roll.ToString());
                }
            }
        }
    }
}