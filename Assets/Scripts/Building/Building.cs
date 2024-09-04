using System.Collections.Generic;
using UnityEngine;


public class Building : MonoBehaviour
{
    public Vector2 Size => m_Size;
    public int BuildingID => m_BuildingID;
    public List<Vector2> OccupiedCells => occupiedCells;

    [SerializeField] private VisualizationSystem m_VisualizationSystem;
    [SerializeField] private Vector2 m_Size;
    [SerializeField] private int m_BuildingID;

    private List<Vector2> occupiedCells = new List<Vector2>();

    [ContextMenu("CellSpawnSpawn")]
    public void CellSpawnSpawn()
    {
        m_VisualizationSystem.CellSpawnSpawn(m_Size);
    }

    public void AddOccupiedCell(Vector2 cell)
    {
        occupiedCells.Add(cell);
    }
    public void ClearOccupiedCell()
    {
        occupiedCells.Clear();
    }

    public void ImpossibleToPlaceABuilding()
    {
        m_VisualizationSystem.ImpossibleToPlaceABuilding();
    }
    public void PossibleToPlaceABuilding()
    {
        m_VisualizationSystem.PossibleToPlaceABuilding();
    }
    public void BuildingIsLocated()
    {
        m_VisualizationSystem.BuildingIsLocated();
    }
    
}
