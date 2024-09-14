using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static PlacedBuildings;

public class PlacementSystem : MonoBehaviour
{    
    public event UnityAction<BuildingInfo> BuildingDeleteEvent;
    public event UnityAction<BuildingInfo> CancellationBuildingPlacementEvent;

    [SerializeField] private BuildingDataBase m_BuildingDataBase;
    [SerializeField] private ConstructionModeUI m_ConstructionModeUI;
    [SerializeField] private Indicator m_Indicator;
    [SerializeField] private ConstructionGrid m_Grid;

    private PlacedBuildings placedBuildings;
    private Building currentBuilding;
    private Vector2 currentCellLocalPosition;
    private bool isBuild;

    private void Awake()
    {
        m_Indicator.CellSelected += OnCellSelected;
        placedBuildings = new PlacedBuildings();
        CreatingUploadedBuildings();                
    }
    private void OnDestroy()
    {
        m_Indicator.CellSelected -= OnCellSelected;
    }    
    
    
    private void CreatingUploadedBuildings()
    {
        List<BuildingData> uploadedBuildingsInfo = placedBuildings.LoadBuildingsData();
        if (uploadedBuildingsInfo == null) return;

        for (int i = 0; i < uploadedBuildingsInfo.Count; i++)
        {
            Building building = Instantiate(m_BuildingDataBase.GetBuilding(uploadedBuildingsInfo[i].BuildingID));
            building.BuildingPlacement(uploadedBuildingsInfo[i].OccupiedCell, placedBuildings.GetBuildIndex());
            placedBuildings.AddBuilding(building);
            building.transform.position = m_Grid.ConvertCellLocalPositionToCellWorldPosition(building.OccupiedCell);
            for (int x = 0; x < building.Size.x; x++)
            {
                for (int y = 0; y < building.Size.y; y++)
                {
                    Vector2 cellPosition = new Vector2(building.OccupiedCell.x + x, building.OccupiedCell.y + y);
                    m_Grid.OccupyACell(cellPosition, building.BuildingIndex);                    
                }
            }           
            
        }        
    }
    
    private void OnCellSelected(Vector2 cellLocalPosition)
    {
        currentCellLocalPosition = cellLocalPosition;

        if (isBuild == true)
        {   
            SetBuildingPosition();
            CheckingPossibilityOfBuildingPlacement();   
        }
        else
        {
            CheckingCellOccupancy();
        }
    }

    private void CheckingCellOccupancy()
    {
        if (m_Grid.CheckingCellOccupancy(currentCellLocalPosition) == true)
        {
            Building building = placedBuildings.GetBuilding(m_Grid.GetCell(currentCellLocalPosition).BuildingIndex);            
            m_Indicator.BuildingSelect(building.Size, building.OccupiedCell);
            m_ConstructionModeUI.EnablingReplacement();
        }
        else
        {
            m_ConstructionModeUI.DisablingReplacement();
            m_Indicator.BuildingUnselect();
        }
    }

    private void CheckingPossibilityOfBuildingPlacement()
    {
        if (currentBuilding == null) return;   
        
        for (int x = 0; x < currentBuilding.Size.x; x++)
        {
            for (int y = 0; y < currentBuilding.Size.y; y++)
            {
                if (m_Grid.CheckingCellOccupancy(new Vector2(currentCellLocalPosition.x + x, currentCellLocalPosition.y + y)) == true)
                {
                    DisablingPlacement();
                    return;
                }
            }
        }
        EnablingPlacement();
    }

    public void StartPlacement(int iD)
    {
        isBuild = true;

        if (currentBuilding != null)
        {
            Destroy(currentBuilding.gameObject);
        }

        currentBuilding = Instantiate(m_BuildingDataBase.GetBuilding(iD));
        currentBuilding.transform.position = m_Indicator.transform.position;
        m_Indicator.IndicatorVisualization(false);
        m_ConstructionModeUI.StartPlacement();
        CheckingPossibilityOfBuildingPlacement();
    }

    private void DisablingPlacement()
    {
        currentBuilding.ImpossibleToPlaceABuilding();
        m_ConstructionModeUI.DisablingPlacement();
    }

    private void EnablingPlacement()
    {
        currentBuilding.PossibleToPlaceABuilding();
        m_ConstructionModeUI.EnablingPlacement();
    }    

    public void BuildingPlacement()
    {        
        int buildIndex = placedBuildings.GetBuildIndex();
        currentBuilding.BuildingPlacement(currentCellLocalPosition, buildIndex);
        placedBuildings.AddBuilding(currentBuilding);
        for (int x = 0; x < currentBuilding.Size.x; x++)
        {
            for (int y = 0; y < currentBuilding.Size.y; y++)
            {
                Vector2 cellPosition = new Vector2(currentCellLocalPosition.x + x, currentCellLocalPosition.y + y);
                m_Grid.OccupyACell(cellPosition, buildIndex);                
            }
        }
        EndPlacement();
    }

    public void CancellationBuildingPlacement()
    {
        CancellationBuildingPlacementEvent?.Invoke(m_BuildingDataBase.GetBuildingInfo(currentBuilding.BuildingID));
        Destroy(currentBuilding.gameObject);    
        EndPlacement();
    }

    private void EndPlacement()
    {
        m_Indicator.IndicatorVisualization(true);
        
        m_ConstructionModeUI.EndPlacement();
        currentBuilding = null;
        isBuild = false;
        CheckingCellOccupancy();
    }      

    

    public void StartReplacement()
    {
        m_Indicator.IndicatorVisualization(false);
        m_ConstructionModeUI.DisablingReplacement();
        GridCleaning();
        isBuild = true;
        SetBuildingPosition();
        CheckingPossibilityOfBuildingPlacement();
    }
    public void BuildingDelete()
    {
        m_ConstructionModeUI.DisablingReplacement();
        GridCleaning();
        BuildingDeleteEvent?.Invoke(m_BuildingDataBase.GetBuildingInfo(currentBuilding.BuildingID));
        Destroy(currentBuilding.gameObject);
        currentBuilding = null;
        m_Indicator.BuildingUnselect();
    }

    private void GridCleaning()
    {
        int buildingIndex = m_Grid.GetCell(currentCellLocalPosition).BuildingIndex;
        currentBuilding = placedBuildings.GetBuilding(buildingIndex);
        for (int i = 0; i < currentBuilding.Size.x; i++)
        {
            for (int j = 0; j < currentBuilding.Size.y; j++)
            {
                m_Grid.ClearACell(currentBuilding.OccupiedCell + new Vector2(i,j));
            }
            
        }
        currentBuilding.ClearOccupiedCell();
        placedBuildings.RemoveBuilding(buildingIndex);
    }
     
    private void SetBuildingPosition()
    {
        if (currentBuilding == null) return;

        currentBuilding.transform.position = m_Indicator.transform.position;     
    }
}
