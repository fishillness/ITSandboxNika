using System;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public Vector2 Size => m_Size;
    public int BuildingID => m_BuildingID;
    public Vector2 OccupiedCell => occupiedCell;
    public int BuildingIndex => buildingIndex;

    [SerializeField] private VisualizationSystem m_VisualizationSystem;
    [SerializeField] private Vector2 m_Size;
    [SerializeField] private int m_BuildingID;

    private int buildingIndex;
    private Vector2 occupiedCell;

    [ContextMenu("CellSpawnSpawn")]
    public void CellSpawnSpawn()
    {
        m_VisualizationSystem.CellSpawnSpawn(m_Size);
    }

    public void BuildingPlacement(Vector2 cell, int buildingIndex)
    {
        this.buildingIndex = buildingIndex;
        occupiedCell = cell;
    }
    public void ClearOccupiedCell()
    {
        occupiedCell = Vector2.zero;
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
