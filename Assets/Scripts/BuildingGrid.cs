using System;
using UnityEngine;

public class BuildingGrid : MonoBehaviour
{
    [Serializable]
    public class CellArray
    {
        public Cell[] Array;
    }

    public float CellSize => m_CellSize;

    [SerializeField] private Vector2Int m_GridSize;
    [SerializeField] private float m_CellSize;
    [SerializeField] private Cell m_CellPrefab;

    [SerializeField] private CellArray[] grid;


    [ContextMenu(nameof(CellSpawn))]
    public void CellSpawn()
    {
        grid = new CellArray[m_GridSize.x];
        
        for (int i = 0; i < grid.Length; i++)
        {
            grid[i] = new CellArray();
            grid[i].Array = new Cell[m_GridSize.y];
            
            for (int j = 0; j < grid[i].Array.Length; j++)
            {                
                Cell Cell = Instantiate(m_CellPrefab, transform);
                Cell.transform.localPosition = new Vector3((m_CellSize / 2) + i * m_CellSize, 0, (m_CellSize / 2) + j * m_CellSize);
                Cell.transform.localScale = Vector3.one * m_CellSize;
                grid[i].Array[j] = Cell; 
            }
        }
    }    

    public Vector3 ConvertMousePositionToCellWorldPosition(Vector3 mousePosition)
    {    
        Vector3 cellWorldPosition = transform.InverseTransformPoint(mousePosition);
        cellWorldPosition.x = Mathf.CeilToInt(cellWorldPosition.x / m_CellSize) * m_CellSize - (m_CellSize / 2);
        cellWorldPosition.y = 0;
        cellWorldPosition.z = Mathf.CeilToInt(cellWorldPosition.z / m_CellSize) * m_CellSize - (m_CellSize / 2);
        cellWorldPosition = transform.TransformPoint(cellWorldPosition);

        return cellWorldPosition;
    }

    public Vector3 ConvertCellLocalPositionToCellWorldPosition(Vector2 cellLocallPosition)
    {
        Vector3 cellWorldPosition = new Vector3(cellLocallPosition.x, 0, cellLocallPosition.y);
        cellWorldPosition.x = cellWorldPosition.x * m_CellSize - (m_CellSize / 2);
        cellWorldPosition.z =cellWorldPosition.z * m_CellSize - (m_CellSize / 2);
        cellWorldPosition = transform.TransformPoint(cellWorldPosition);

        return cellWorldPosition;
    }

    public Vector2 ConvertMousePositionToLocalCellPosition(Vector3 mousePosition)
    {
        mousePosition = transform.InverseTransformPoint(mousePosition);
        Vector2 cellLocalPosition = new Vector2(mousePosition.x, mousePosition.z);
        cellLocalPosition.x = Mathf.CeilToInt(mousePosition.x / m_CellSize);
        cellLocalPosition.y = Mathf.CeilToInt(mousePosition.z / m_CellSize);

        return cellLocalPosition;
    }

    public bool CheckingCellActivity(Vector2 cellLocallPosition)
    {
        if (cellLocallPosition.x - 1 < 0 || cellLocallPosition.y - 1 < 0 || cellLocallPosition.x - 1 >= m_GridSize.x || cellLocallPosition.y - 1 >= m_GridSize.y)
        {
            return false;
        }
        return grid[(int)cellLocallPosition.x - 1].Array[(int)cellLocallPosition.y - 1].gameObject.activeSelf;
    }

    public bool CheckingCellOccupancy(Vector2 cellLocallPosition)
    {
        if (CheckingCellActivity(cellLocallPosition) == false)
        {
            return true;
        }

        return grid[(int)cellLocallPosition.x - 1].Array[(int)cellLocallPosition.y - 1].IsEmployed;
    }

    public void OccupyACell(Vector2 cellLocalPosition, int buildingID, int buildingIndex)
    {   
        grid[(int)cellLocalPosition.x - 1].Array[(int)cellLocalPosition.y - 1].OccupyACell(buildingID, buildingIndex);
    }

    public void ClearACell(Vector2 cellLocalPosition)
    {
        grid[(int)cellLocalPosition.x - 1].Array[(int)cellLocalPosition.y - 1].ClearACell();
    }

    public Cell GetCell(Vector2 cellLocalPosition)
    {
        if (CheckingCellActivity(cellLocalPosition) == true)
        {
            return grid[(int)cellLocalPosition.x - 1].Array[(int)cellLocalPosition.y - 1];
        }
        else
        {
            return null;
        }

    }
}
