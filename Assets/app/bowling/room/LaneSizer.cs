using UnityEngine;

namespace app.bowling.room
{
    public class LaneSizer : MonoBehaviour
    {
        [SerializeField] private GutterBehaviour leftGutterPrefab;
        [SerializeField] private GutterBehaviour rightGutterPrefab;
        [SerializeField] private GameObject leftLaneFloorPrefab;
        [SerializeField] private GameObject rightLaneFloorPrefab;
        [SerializeField] private GameObject laneFloorPrefab;
        [SerializeField] private GameObject machineFillerPrefab;

        [SerializeField] private int lastRowPinNumber = 4;
        [SerializeField] private float pinXSpacing = .305f;
        [SerializeField] private float gutterWidth = .68f;
        [SerializeField] private float pinAndGutterSpace = .0743f;
        [SerializeField] private float roomWidth = 10;
        
        public GutterBehaviour leftGutter;
        public GutterBehaviour rightGutter;

        private void Awake()
        {
            var laneWidth = (lastRowPinNumber - 1) * pinXSpacing + pinAndGutterSpace * 2;
            var lane = Instantiate(laneFloorPrefab, transform);
            lane.transform.localScale = new Vector3(laneWidth, 1, 1);
            lane.transform.localPosition = new Vector3(-laneWidth / 2, 0, 0);
            lane.transform.GetChild(0).GetComponent<Renderer>().material.mainTextureScale = new Vector2(laneWidth, 1);
            
            var machineFiller = Instantiate(machineFillerPrefab, transform);
            machineFiller.transform.localScale = new Vector3(laneWidth, 1, 1);
            machineFiller.transform.localPosition = new Vector3(-laneWidth / 2, .463f, -18.883f);
            machineFiller.transform.GetChild(0).GetComponent<Renderer>().material.mainTextureScale = new Vector2(laneWidth, 1);
            
            leftGutter = Instantiate(leftGutterPrefab, transform);
            leftGutter.transform.localPosition = new Vector3(laneWidth / 2, 0, 0);
            
            rightGutter = Instantiate(rightGutterPrefab, transform);
            rightGutter.transform.localPosition = new Vector3(-laneWidth / 2, 0, 0);
            
            var floorWidth = (roomWidth - laneWidth - gutterWidth * 2) / 2;

            var leftLaneFloor = Instantiate(leftLaneFloorPrefab, transform);
            leftLaneFloor.transform.localScale = new Vector3(floorWidth, 1, 1);
            leftLaneFloor.transform.localPosition = new Vector3(laneWidth / 2 + gutterWidth, 0, 0);
            
            var rightLaneFloor = Instantiate(rightLaneFloorPrefab, transform);
            rightLaneFloor.transform.localScale = new Vector3(floorWidth, 1, 1);
            rightLaneFloor.transform.localPosition = new Vector3(-laneWidth / 2 - gutterWidth, 0, 0);
        }
    }
}