using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementSystem : MonoBehaviour
{
    [SerializeField] private BuildModeUI m_BuildModeUI;
    [SerializeField] private Indicator m_Indicator;
    [SerializeField] private BuildingGrid m_Grid;

    private Building currentBuilding;
    private Vector2 currentCellLocalPosition;
    public void StartPlacement(Building building)
    {
        if (currentBuilding != null)
        {
            Destroy(currentBuilding.gameObject);
        }
        currentBuilding = Instantiate(building, m_Indicator.transform.position, Quaternion.identity);
        m_BuildModeUI.StartPlacement();

        m_Indicator.CellSelected += OnCellSelected;
    }

    private void OnCellSelected(Vector2 cellLocalPosition)
    {
        currentBuilding.transform.position = m_Indicator.transform.position;
        currentCellLocalPosition = cellLocalPosition;
        CheckingPossibilityOfBuildingPlacement(currentCellLocalPosition);
    }

    private void CheckingPossibilityOfBuildingPlacement(Vector2 cellLocalPosition)
    {
        for (int x = 0; x < currentBuilding.Size.x; x++)
        {
            for (int y = 0; y < currentBuilding.Size.y; y++)
            {
                if (m_Grid.CheckingCellOccupancy(new Vector2(cellLocalPosition.x + x, cellLocalPosition.y + y)) == true)
                {
                    DisablingPlacement();
                    return;
                }
            }
        }
        EnablingPlacement();
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
                m_Grid.OccupyACell(new Vector2(currentCellLocalPosition.x + x, currentCellLocalPosition.y + y), currentBuilding.BuildingID, );
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
        m_Indicator.CellSelected -= OnCellSelected;
        m_BuildModeUI.EndPlacement();
        currentBuilding = null;
    }
}
