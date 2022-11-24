using UnityEngine;

public class PinPositioner : MonoBehaviour
{
    [SerializeField] private GameObject pinPrefab;
    [SerializeField] private Transform pinParent;
    [SerializeField] private float pinXDistance = .2635f;
    [SerializeField] private float pinZDistance = .305f;
    
    // inspector section
    [Header("Manual pins spawn")]
    [SerializeField] private int pinRowCount = 3;
    [SerializeField] private bool spawnPins = false;

    public void SpawnPins(int pinRowNumber)
    {
        for (var pinRowIndex = pinRowNumber; pinRowIndex >= 0; pinRowIndex--)
        {
            SpawnRow(pinRowIndex, pinRowNumber);
        }
    }

    private void SpawnRow(int pinRowIndex, int pinRowNumber)
    {
        for (var pinIndexInRow = 0; pinIndexInRow < pinRowIndex; pinIndexInRow++)
        {
            SpawnPin(pinRowIndex, pinIndexInRow, pinRowNumber);
        }
    }

    private void SpawnPin(int pinRowIndex, int pinIndexInRow, int pinRowNumber)
    {
        var pin = Instantiate(pinPrefab, pinParent.transform);
        pin.name = pinRowIndex + " - " + pinIndexInRow;
        pin.transform.localPosition = new Vector3(
            pinIndexInRow * pinXDistance - (pinRowIndex - 1) * pinXDistance / 2,
            0,
            (pinRowNumber - pinRowIndex) * pinZDistance
        );
    }

    private void Update()
    {
        if (!spawnPins) return;
        spawnPins = false;
        SpawnPins(pinRowCount);
    }

    public void RemovePins()
    {
        foreach (Transform pin in pinParent)
        {
            Destroy(pin.gameObject);
        }
    }
}