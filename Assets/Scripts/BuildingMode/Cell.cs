using UnityEngine;


public class Cell : MonoBehaviour
{
    public bool IsEmployed => isEmployed;
    public int BuildingIndex => buildingIndex;

    [SerializeField] private GameObject m_ClossedCell;

    private bool isEmployed;
    private int buildingIndex;
    

    public void OccupyACell(int buildingIndex)
    {
        m_ClossedCell.SetActive(true);
        isEmployed = true;
        this.buildingIndex = buildingIndex;
    }

    public void ClearACell()
    {
        m_ClossedCell.SetActive(false);
        isEmployed = false;
        buildingIndex = 0;
    }
}
