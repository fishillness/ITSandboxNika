using System;
using UnityEngine;
using UnityEngine.Events;

public class Store : MonoBehaviour
{   
    public event UnityAction<BuildingInfo> BuyEvent;

    [SerializeField] private ResourceManager m_ResourceManager;
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
        m_ResourceManager.DeleteResources(buildingInfo.NeededCoins, buildingInfo.NeededBoards, buildingInfo.NeededBricks, buildingInfo.NeededNails);
        CellsUpdate();
        BuyEvent?.Invoke(buildingInfo);        
    }

    private void Refund(BuildingInfo buildingInfo, float refundPercentage)
    {
        m_ResourceManager.AddResources(
            Mathf.CeilToInt(buildingInfo.NeededCoins * refundPercentage),
            Mathf.CeilToInt(buildingInfo.NeededBoards * refundPercentage),
            Mathf.CeilToInt(buildingInfo.NeededBricks * refundPercentage),
            Mathf.CeilToInt(buildingInfo.NeededNails * refundPercentage)
            );
               
        CellsUpdate();
    }

    public void FullRefund(BuildingInfo buildingInfo)
    {
        Refund(buildingInfo, 1);
    }
    public void PartialRefund(BuildingInfo buildingInfo)
    {
        Refund(buildingInfo, m_RefundPercentage);
    }


    private void CellsUpdate()
    {
        for (int i = 0; i < m_Cells.Length; i++)
        {
            m_Cells[i].CellUpdate(m_ResourceManager.CoinsCount, m_ResourceManager.BoardsCount, m_ResourceManager.BricksCount, m_ResourceManager.NailsCount);
        }
    }

    
}
