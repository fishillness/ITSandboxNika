using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Indicator : MonoBehaviour
{
    public event UnityAction<Vector2> CellSelected;
    [SerializeField] private BuildingGrid m_Grid;
    [SerializeField] private InputManager m_InputManager;
    [SerializeField] private SpriteRenderer m_SpriteRenderer;

    private Vector3 indicatorPosition;
    private void Start()
    {
        transform.position = m_Grid.ConvertCellLocalPositionToCellWorldPosition(Vector2.one);
        transform.localScale = Vector3.one * m_Grid.CellSize;
        m_InputManager.Click += SetPosition;
    }
    private void OnDestroy()
    {
        m_InputManager.Click -= SetPosition;
    }
    private void SetPosition()
    {
        indicatorPosition = m_Grid.ConvertMousePositionToCellWorldPosition(m_InputManager.GetMousePosition());

        Vector2 localCellPosition = m_Grid.ConvertMousePositionToLocalCellPosition(m_InputManager.GetMousePosition());
        if (m_Grid.CheckingCellActivity(localCellPosition) == false)
        {
            CellSelected?.Invoke(localCellPosition);
            m_SpriteRenderer.enabled = false;
            return;
        }
        else
        {
            m_SpriteRenderer.enabled = true;
            transform.position = indicatorPosition;
        }   
    }

    
}
