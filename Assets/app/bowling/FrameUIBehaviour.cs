using System.Collections.Generic;
using app.bowling.logic;
using TMPro;
using UnityEngine;

namespace app.bowling
{
    internal class FrameUIBehaviour : MonoBehaviour
    {
        [SerializeField] private Transform rolls;
        [SerializeField] private RollUIBehaviour rollUiBehaviourPrefab;
        [SerializeField] private List<RollUIBehaviour> rollUiBehaviours;
        [SerializeField] private TMP_Text index;
        [SerializeField] private TMP_Text cumulativeScore;

        public void Awake()
        {
            rollUiBehaviours = new List<RollUIBehaviour>();
        }
        
        public void Setup(Frame frame)
        {
            index.text = frame.Index.ToString();
            cumulativeScore.text = frame.CumulativeScore.ToString();
            
            foreach (var roll in frame.Rolls)
            {
                var rollUi = Instantiate(rollUiBehaviourPrefab, rolls);
                rollUi.SetRoll(roll);
                rollUiBehaviours.Add(rollUi);
            }
        }

        public void SetScores(Frame frame)
        {
            Debug.Log("frame.CumulativeScore");
            Debug.Log(frame.CumulativeScore);
            cumulativeScore.text = frame.CumulativeScore.ToString();
            
            var rollIndexInFrame = 0;
            foreach (var roll in frame.Rolls)
            {
                rollUiBehaviours[rollIndexInFrame++].SetRoll(roll);
            }
        }
    }
}