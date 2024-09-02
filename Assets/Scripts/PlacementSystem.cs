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
            CheckingPossibilityOfBuildingReplacement();
        }
    }

    private void CheckingPossibilityOfBuildingReplacement()
    {
        if (m_Grid.CheckingCellOccupancy(new Vector2(currentCellLocalPosition.x, currentCellLocalPosition.y)) == true)
        {
            EnablingReplacement();
        }
        else
        {
            DisablingReplacement();
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
        currentBuilding = Instantiate(building, m_Indicator.transform.position, Quaternion.identity);
        m_BuildModeUI.StartPlacement();
        CheckingPossibilityOfBuildingPlacement();
    }

    private void DisablingPlacement()
    {
        m_BuildModeUI.DisablingPlacement();
    }

    private void EnablingPlacement()
    {
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
        CheckingPossibilityOfBuildingReplacement();
        m_BuildModeUI.EndPlacement();
        currentBuilding = null;
        isBuild = false;
    }

    private void DisablingReplacement()
    {
        m_BuildModeUI.DisablingReplacement();
    }

    private void EnablingReplacement()
    {
        m_BuildModeUI.EnablingReplacement();
    }

    public void StartReplacement()
    {
        DisablingReplacement();        
        GridCleaning();
        isBuild = true;
        SetBuildingPosition();
        CheckingPossibilityOfBuildingPlacement();
    }
    public void BuildingDelete()
    {
        DisablingReplacement();
        GridCleaning();        
        Destroy(currentBuilding.gameObject);
        currentBuilding = null;
    }

    private void GridCleaning()
    {
        currentBuilding = placedBuildings.GetBuilding(m_Grid.GetCell(currentCellLocalPosition).BuildingIndex);
        for (int i = 0; i < currentBuilding.OccupiedCells.Count; i++)
        {
            m_Grid.ClearACell(currentBuilding.OccupiedCells[i]);
        }
    }
     
    private void SetBuildingPosition()
    {
        if (currentBuilding == null) return;
        
        currentBuilding.transform.position = m_Indicator.transform.position;     
    }
}
