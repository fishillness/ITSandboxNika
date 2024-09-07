using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlacedBuildings;

public class PlacementSystem : MonoBehaviour
{    

    [SerializeField] private BuildingDataBase BuildingDataBase;
    [SerializeField] private BuildingModeUI m_BuildModeUI;
    [SerializeField] private Indicator m_Indicator;
    [SerializeField] private BuildingGrid m_Grid;

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
        List<BuildingInfo> uploadedBuildingsInfo = placedBuildings.Load();
        if (uploadedBuildingsInfo == null) return;

        for (int i = 0; i < uploadedBuildingsInfo.Count; i++)
        {
            Building building = Instantiate(BuildingDataBase.GetBuilding(uploadedBuildingsInfo[i].BuildingID));
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
            m_BuildModeUI.EnablingReplacement();
        }
        else
        {
            m_BuildModeUI.DisablingReplacement();
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

    public void StartPlacement(Building building)
    {
        isBuild = true;

        if (currentBuilding != null)
        {
            Destroy(currentBuilding.gameObject);
        }
        currentBuilding = Instantiate(building, m_Indicator.transform.position, building.transform.rotation);
        m_Indicator.IndicatorVisualization(false);
        m_BuildModeUI.StartPlacement();
        CheckingPossibilityOfBuildingPlacement();
    }

    private void DisablingPlacement()
    {
        currentBuilding.ImpossibleToPlaceABuilding();
        m_BuildModeUI.DisablingPlacement();
    }

    private void EnablingPlacement()
    {
        currentBuilding.PossibleToPlaceABuilding();
        m_BuildModeUI.EnablingPlacement();
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
        Destroy(currentBuilding.gameObject);    
        EndPlacement();
    }

    private void EndPlacement()
    {
        m_Indicator.IndicatorVisualization(true);
        currentBuilding.BuildingIsLocated();
        
        m_BuildModeUI.EndPlacement();
        currentBuilding = null;
        isBuild = false;
        CheckingCellOccupancy();
    }      

    

    public void StartReplacement()
    {
        m_Indicator.IndicatorVisualization(false);
        m_BuildModeUI.DisablingReplacement();
        GridCleaning();
        isBuild = true;
        SetBuildingPosition();
        CheckingPossibilityOfBuildingPlacement();
    }
    public void BuildingDelete()
    {
        m_BuildModeUI.DisablingReplacement();
        GridCleaning();        
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
