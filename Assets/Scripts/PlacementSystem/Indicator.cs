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
    [SerializeField] private Vector2 m_StartPosition;

    private bool enablet = true;
    private void Start()
    {
        transform.position = m_Grid.ConvertCellLocalPositionToCellWorldPosition(m_StartPosition);
        CellSelected?.Invoke(m_StartPosition);

        transform.localScale = Vector3.one * m_Grid.CellSize;
        m_InputManager.Click += SelectingACell;
    }
    private void OnDestroy()
    {
        m_InputManager.Click -= SelectingACell;
    }

    private void SelectingACell()
    {
        Vector2 localCellPosition = m_Grid.ConvertMousePositionToLocalCellPosition(m_InputManager.GetMousePosition(m_Grid.transform));
        
        if (m_Grid.CheckingCellActivity(localCellPosition) == false)
        {            
            m_SpriteRenderer.enabled = false;
            return;
        }
        else
        {
            if (enablet == true)
            {
                m_SpriteRenderer.enabled = true;
            }
            
            SetPosition();
            CellSelected?.Invoke(localCellPosition);
        }
        
    }

    private void SetPosition()
    {
        transform.position = m_Grid.ConvertMousePositionToCellWorldPosition(m_InputManager.GetMousePosition(m_Grid.transform));
    }

    public void IndicatorEnabled(bool value)
    {
        enablet = value;
        m_SpriteRenderer.enabled = value;
    }

    public void BuildingSelect(Vector2 buildSize, List<Vector2> cells)
    {
        transform.localScale = buildSize;
        Vector2 indicatorPosition = new Vector2();
        for (int i = 0; i < cells.Count; i++)
        {
            indicatorPosition.x += cells[i].x;
            indicatorPosition.y += cells[i].y;
        }
        indicatorPosition.x /= cells.Count;
        indicatorPosition.y /= cells.Count;

        m_SpriteRenderer.transform.position = m_Grid.ConvertCellLocalPositionToCellWorldPosition(indicatorPosition);
    }
    public void BuildingUnselect()
    {
        transform.localScale = Vector3.one * m_Grid.CellSize;
        m_SpriteRenderer.transform.localPosition = Vector3.zero;
    }
}
