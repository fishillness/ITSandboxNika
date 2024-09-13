using UnityEngine;
using UnityEngine.Events;

public class Store : MonoBehaviour
{
    public event UnityAction<BuildingInfo> BuyEvent;

    [SerializeField] private Resource m_Coins;
    [SerializeField] private Resource m_Boards;
    [SerializeField] private Resource m_Bricks;
    [SerializeField] private Resource m_Nails;

    [SerializeField] [Range(0, 1)] private float m_RefundPercentage;

    [SerializeField] private StoreCell[] m_Cells;

    private void Start()
    {
        CellsUpdate();
        for (int i = 0; i < m_Cells.Length; i++)
        {
            m_Cells[i].BuyEvent += Buy;
        }
    }
    private void OnDestroy()
    {
        for (int i = 0; i < m_Cells.Length; i++)
        {
            m_Cells[i].BuyEvent -= Buy;
        }
    }

    public void Buy(BuildingInfo buildingInfo)
    {
        if (m_Coins.DeleteValue(buildingInfo.NeededCoins) & m_Boards.DeleteValue(buildingInfo.NeededBoards) & m_Bricks.DeleteValue(buildingInfo.NeededBricks) & m_Nails.DeleteValue(buildingInfo.NeededNails))
        {
            CellsUpdate();
            BuyEvent?.Invoke(buildingInfo);
        }         
    }

    public void Refund(BuildingInfo buildingInfo)
    {
        m_Coins.AddValue(Mathf.CeilToInt(buildingInfo.NeededCoins * m_RefundPercentage));
        m_Boards.AddValue(Mathf.CeilToInt(buildingInfo.NeededBoards * m_RefundPercentage));
        m_Bricks.AddValue(Mathf.CeilToInt(buildingInfo.NeededBricks * m_RefundPercentage));
        m_Nails.AddValue(Mathf.CeilToInt(buildingInfo.NeededNails * m_RefundPercentage));
        CellsUpdate();
    }

    private void CellsUpdate()
    {
        for (int i = 0; i < m_Cells.Length; i++)
        {
            m_Cells[i].CellUpdate(m_Coins.CurrentValue, m_Boards.CurrentValue, m_Bricks.CurrentValue, m_Nails.CurrentValue);
        }
    }
}
