using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementSystem : MonoBehaviour
{
    [SerializeField] private BuildModeUI m_BuildModeUI;
    [SerializeField] private Indicator m_Indicator;
    [SerializeField] private BuildingGrid m_Grid;

    private PlacedBuildings placedBuildings;
    private Building currentBuilding;
    private Vector2 currentCellLocalPosition;
    private bool isBuild;

    private void Awake()
    {
        if (placedBuildings == null)
        {
            placedBuildings = new PlacedBuildings();
        }
        m_Indicator.CellSelected += OnCellSelected;
    }
    private void OnDestroy()
    {
        m_Indicator.CellSelected -= OnCellSelected;
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
            m_Indicator.BuildingSelect(building.Size, building.OccupiedCells);
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
        m_Indicator.IndicatorEnabled(false);
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
        for (int x = 0; x < currentBuilding.Size.x; x++)
        {
            for (int y = 0; y < currentBuilding.Size.y; y++)
            {
                Vector2 cellPosition = new Vector2(currentCellLocalPosition.x + x, currentCellLocalPosition.y + y);
                m_Grid.OccupyACell(cellPosition, currentBuilding.BuildingID, placedBuildings.AddBuilding(currentBuilding));
                currentBuilding.AddOccupiedCell(cellPosition);
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
        m_Indicator.IndicatorEnabled(true);
        currentBuilding.BuildingIsLocated();
        
        m_BuildModeUI.EndPlacement();
        currentBuilding = null;
        isBuild = false;
        CheckingCellOccupancy();
    }      

    

    public void StartReplacement()
    {
        m_Indicator.IndicatorEnabled(false);
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
        currentBuilding = placedBuildings.GetBuilding(m_Grid.GetCell(currentCellLocalPosition).BuildingIndex);
        for (int i = 0; i < currentBuilding.OccupiedCells.Count; i++)
        {
            m_Grid.ClearACell(currentBuilding.OccupiedCells[i]);
        }
        currentBuilding.ClearOccupiedCell();
    }
     
    private void SetBuildingPosition()
    {
        if (currentBuilding == null) return;
        
        currentBuilding.transform.position = m_Indicator.transform.position;     
    }
}
