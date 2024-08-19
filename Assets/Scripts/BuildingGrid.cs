using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGrid : MonoBehaviour
{
    public float CellSize => m_CellSize;

    [Serializable]
    private class Line
    {
        public bool[] CellsActive;
    }

    [SerializeField] private Line[] m_Lines;
    [SerializeField] private float m_CellSize;
    [SerializeField] private SpriteRenderer m_CellPrefab;    

    private bool[,] grid;

    private void Start()
    {
        grid = new bool[m_Lines[0].CellsActive.Length, m_Lines.Length];

        for (int i = 0; i < m_Lines.Length; i++)
        {
            if (i >= grid.GetLength(1)) return;
            for (int j = 0; j < m_Lines[i].CellsActive.Length; j++)
            {
                if (j >= grid.GetLength(0)) continue;
                grid[j, i] = m_Lines[i].CellsActive[j];
            }
        }

        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                if (grid[i, j] == true)
                {
                    SpriteRenderer Cell = Instantiate(m_CellPrefab, transform);
                    Cell.transform.position = new Vector3(transform.position.x + (m_CellSize / 2) + i * m_CellSize, transform.position.y, transform.position.z + (m_CellSize / 2) + j * m_CellSize);
                    Cell.transform.localScale = Vector3.one * m_CellSize;
                }
                
            }
        }
        
    }

    public Vector3 GetCellPosition(Vector3 mousePosition)
    {    
        mousePosition = transform.InverseTransformPoint(mousePosition);
        mousePosition.x = Mathf.CeilToInt(mousePosition.x / m_CellSize) * m_CellSize - m_CellSize/2;
        mousePosition.y = transform.position.y;
        mousePosition.z = Mathf.CeilToInt(mousePosition.z / m_CellSize) * m_CellSize - m_CellSize / 2;
        mousePosition = transform.TransformPoint(mousePosition);

        return mousePosition;
    }

    public bool CheckCell(Vector3 mousePosition)
    {
        mousePosition = transform.InverseTransformPoint(mousePosition);
        mousePosition.x = Mathf.CeilToInt(mousePosition.x / m_CellSize);
        mousePosition.z = Mathf.CeilToInt(mousePosition.z / m_CellSize);
        Debug.Log(mousePosition);

        if (mousePosition.x - 1 < 0 || mousePosition.z - 1 < 0 || mousePosition.x - 1 >= grid.GetLength(0) || mousePosition.z - 1 >= grid.GetLength(1))
        {
            return false;
        }
        return grid[(int)mousePosition.x - 1, (int)mousePosition.z - 1];
    }
}
