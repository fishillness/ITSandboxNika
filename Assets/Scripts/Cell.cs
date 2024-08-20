using UnityEngine;


public class Cell : MonoBehaviour
{
    public bool IsEmployed => isEmployed;
    public int BuildingID => buildingID;
    public int BuildingIndex => buildingIndex;

    private bool isEmployed;
    private int buildingID;
    private int buildingIndex;

    public void BuildingPlacement(int buildingID, int buildingIndex)
    {
        isEmployed = true;
        this.buildingID = buildingID;
        this.buildingIndex = buildingIndex;
    }
}
