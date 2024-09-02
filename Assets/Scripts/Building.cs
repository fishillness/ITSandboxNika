using System.Collections.Generic;
using UnityEngine;


public class Building : MonoBehaviour
{
    public Vector2 Size => m_Size;
    public int BuildingID => m_BuildingID;
    public List<Vector2> OccupiedCells => occupiedCells;


    [SerializeField] private Vector2 m_Size;
    [SerializeField] private int m_BuildingID;

    private List<Vector2> occupiedCells = new List<Vector2>();

    public void AddOccupiedCell(Vector2 cell)
    {
        occupiedCells.Add(cell);
    }
}
