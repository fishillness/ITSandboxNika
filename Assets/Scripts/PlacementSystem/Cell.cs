using UnityEngine;


public class Cell : MonoBehaviour
{
    public bool IsEmployed => isEmployed;
    public int BuildingID => buildingID;
    public int BuildingIndex => buildingIndex;

    [SerializeField] private GameObject m_ClossedCell;

    private bool isEmployed;
    private int buildingID;
    private int buildingIndex;
    

    public void OccupyACell(int buildingID, int buildingIndex)
    {
        m_ClossedCell.SetActive(true);
        isEmployed = true;
        this.buildingID = buildingID;
        this.buildingIndex = buildingIndex;
    }

    public void ClearACell()
    {
        m_ClossedCell.SetActive(false);
        isEmployed = false;
        buildingID = 0;
        buildingIndex = 0;
    }
}
