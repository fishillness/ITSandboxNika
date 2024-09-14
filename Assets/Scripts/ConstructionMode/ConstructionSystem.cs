using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionSystem : MonoBehaviour
{
    [SerializeField] private Store m_Store;
    [SerializeField] private ConstructionModeActivator m_ConstructionModeActivator;
    [SerializeField] private PlacementSystem m_PlacementSystem;

    private void Awake()
    {
        m_Store.BuyEvent += StartConstruction;
        m_PlacementSystem.BuildingDeleteEvent += BuildingDelete;
        m_PlacementSystem.CancellationBuildingPlacementEvent += CancellationBuildingPlacement;
    }
    private void OnDestroy()
    {
        m_Store.BuyEvent -= StartConstruction;
        m_PlacementSystem.BuildingDeleteEvent -= BuildingDelete;
    }

    private void StartConstruction(BuildingInfo buildingInfo)
    {
        m_ConstructionModeActivator.ConstructionModeActivate();
        m_PlacementSystem.StartPlacement(buildingInfo.Building.BuildingID);
    }
    private void BuildingDelete(BuildingInfo buildingInfo)
    {
        m_Store.PartialRefund(buildingInfo);
    }
    private void CancellationBuildingPlacement(BuildingInfo buildingInfo)
    {
        m_Store.FullRefund(buildingInfo);
    }
}
