using System;
using UnityEngine;
using UnityEngine.Events;

public class Store : MonoBehaviour
{   
    public event UnityAction<BuildingInfo> BuyEvent;

    [SerializeField] private ValueManager m_ResourceManager;
    [SerializeField] [Range(0, 1)] private float m_RefundPercentage;
    [SerializeField] private StoreCell[] m_Cells;

    private void Start()
    {
        CellsUpdate();
        for (int i = 0; i < m_Cells.Length; i++)
        {
            m_Cells[i].BuyEvent += TryBuy;
        }
    }
    private void OnDestroy()
    {
        for (int i = 0; i < m_Cells.Length; i++)
        {
            m_Cells[i].BuyEvent -= TryBuy;
        }
    }

    public void TryBuy(BuildingInfo buildingInfo)
    {
        BuyEvent?.Invoke(buildingInfo);
    }

    public void Buy(BuildingInfo buildingInfo)
    {        
        m_ResourceManager.DeleteResources(buildingInfo.NeededCoins, buildingInfo.NeededBoards, buildingInfo.NeededBricks, buildingInfo.NeededNails);
        CellsUpdate();               
    }

    public void Refund(BuildingInfo buildingInfo)
    {
        m_ResourceManager.AddResources(
            Mathf.CeilToInt(buildingInfo.NeededCoins * m_RefundPercentage),
            Mathf.CeilToInt(buildingInfo.NeededBoards * m_RefundPercentage),
            Mathf.CeilToInt(buildingInfo.NeededBricks * m_RefundPercentage),
            Mathf.CeilToInt(buildingInfo.NeededNails * m_RefundPercentage)
            );
               
        CellsUpdate();
    }  
    private void CellsUpdate()
    {
        for (int i = 0; i < m_Cells.Length; i++)
        {
            m_Cells[i].CellUpdate(m_ResourceManager.CoinsCount, m_ResourceManager.BoardsCount, m_ResourceManager.BricksCount, m_ResourceManager.NailsCount);
        }
    }

    
}
