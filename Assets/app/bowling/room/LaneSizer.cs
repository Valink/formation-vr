using UnityEngine;

namespace app.bowling.room
{
    public class LaneSizer : MonoBehaviour
    {
        [SerializeField] private GutterBehaviour leftGutter;
        [SerializeField] private GutterBehaviour rightGutter;
        [SerializeField] private GameObject leftLaneFloor;
        [SerializeField] private GameObject rightLaneFloor;
        [SerializeField] private GameObject laneFloor;
        [SerializeField] private GameObject machineFiller;

        [SerializeField] private float pinXSpacing = .305f;
        [SerializeField] private float gutterWidth = .68f;
        [SerializeField] private float pinAndGutterSpace = .0743f;
        [SerializeField] private float roomWidth = 10;
        
        public void SetupLaneFor(int lastRowPinNumber)
        {
            var laneWidth = (lastRowPinNumber - 1) * pinXSpacing + pinAndGutterSpace * 2;
            laneFloor.transform.localScale = new Vector3(laneWidth, 1, 1);
            laneFloor.transform.localPosition = new Vector3(-laneWidth / 2, 0, 0);
            laneFloor.transform.GetChild(0).GetComponent<Renderer>().material.mainTextureScale = new Vector2(laneWidth, 1);
            
            machineFiller.transform.localScale = new Vector3(laneWidth, 1, 1);
            machineFiller.transform.localPosition = new Vector3(-laneWidth / 2, .463f, -18.883f);
            machineFiller.transform.GetChild(0).GetComponent<Renderer>().material.mainTextureScale = new Vector2(laneWidth, 1);
            
            leftGutter.transform.localPosition = new Vector3(laneWidth / 2, 0, 0);
            
            rightGutter.transform.localPosition = new Vector3(-laneWidth / 2, 0, 0);
            
            var floorWidth = (roomWidth - laneWidth - gutterWidth * 2) / 2;

            leftLaneFloor.transform.localScale = new Vector3(floorWidth, 1, 1);
            leftLaneFloor.transform.localPosition = new Vector3(laneWidth / 2 + gutterWidth, 0, 0);
            
            rightLaneFloor.transform.localScale = new Vector3(floorWidth, 1, 1);
            rightLaneFloor.transform.localPosition = new Vector3(-laneWidth / 2 - gutterWidth, 0, 0);
        }
    }
}