using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indicator : MonoBehaviour
{
    [SerializeField] private BuildingGrid m_Grid;
    [SerializeField] private InputManager m_InputManager;

    private Vector3 indicatorPosition;
    private void Start()
    {
        transform.localScale = Vector3.one * m_Grid.CellSize;
    }
    private void Update()
    {
        indicatorPosition = m_Grid.GetCellPosition(m_InputManager.GetMousePosition());
        
        if (m_Grid.CheckCell(m_InputManager.GetMousePosition()) == false)
        {
            return;
        }
        
        transform.position = indicatorPosition;
    }
}
