using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingsGrid : MonoBehaviour
{
    [Serializable]
    private class Line
    {
        public int[] number;
    }

    [SerializeField] private Line[] m_Lines;
    [SerializeField] private float m_CellSize;
    [SerializeField] private SpriteRenderer m_CellPrefab;

    private int[,] grid;
    private void Start()
    {
        grid = new int[m_Lines[0].number.Length, m_Lines.Length];

        for (int i = 0; i < m_Lines.Length; i++)
        {
            if (i >= grid.GetLength(1)) return;
            for (int j = 0; j < m_Lines[i].number.Length; j++)
            {
                if (j >= grid.GetLength(0)) continue;
                grid[j, i] = m_Lines[i].number[j];
            }
        }

        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                if (grid[i, j] == 1)
                {
                    SpriteRenderer Cell = Instantiate(m_CellPrefab, transform);
                    Cell.transform.position = new Vector3(transform.position.x + (m_CellSize / 2) + i * m_CellSize, transform.position.y, transform.position.z + (m_CellSize / 2) + j * m_CellSize);
                    Cell.transform.localScale = Vector3.one * m_CellSize;
                }
                
            }
        }
        
    }
}
