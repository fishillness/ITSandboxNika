using UnityEngine;

public class ConstructionSystem : MonoBehaviour
{
    [SerializeField] private Store m_Store;
    [SerializeField] private ConstructionModeActivator m_ConstructionModeActivator;
    [SerializeField] private PlacementSystem m_PlacementSystem;
    [SerializeField] private ValueManager m_ShelterCharacteristicsManager;

    private void Awake()
    {
        m_Store.BuyEvent += StartConstruction;
        m_PlacementSystem.BuildingDeleteEvent += BuildingDelete;
        m_PlacementSystem.BuildingPlacementEvent += BuildingPlacement;
    }
    private void OnDestroy()
    {
        m_Store.BuyEvent -= StartConstruction;
        m_PlacementSystem.BuildingDeleteEvent -= BuildingDelete;
        m_PlacementSystem.BuildingPlacementEvent -= BuildingPlacement;
    }

    private void StartConstruction(BuildingInfo buildingInfo)
    {
        m_ConstructionModeActivator.ConstructionModeActivate();
        m_PlacementSystem.StartPlacement(buildingInfo.Building);
    }
    private void BuildingDelete(BuildingInfo buildingInfo)
    {
        m_Store.Refund(buildingInfo);
        m_ShelterCharacteristicsManager.DeleteShelterCharacteristics(buildingInfo.Advancement, buildingInfo.Cosiness, buildingInfo.Health, buildingInfo.Joy);
    }
    private void BuildingPlacement(BuildingInfo buildingInfo)
    {
        m_Store.Buy(buildingInfo);
        m_ShelterCharacteristicsManager.AddShelterCharacteristics(buildingInfo.Advancement, buildingInfo.Cosiness, buildingInfo.Health, buildingInfo.Joy);
    }
}
