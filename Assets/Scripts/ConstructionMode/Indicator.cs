using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Indicator : MonoBehaviour
{
    public event UnityAction<Vector2> CellSelected;

    [SerializeField] private ConstructionGrid m_Grid;
    [SerializeField] private InputManager m_InputManager;
    [SerializeField] private SpriteRenderer m_SpriteRenderer;

    private bool indicatorVisualization = true;
    private bool indicatoEnabled = true;
    private void Start()
    {
        m_InputManager.Click += SelectingACell;
    }
    private void OnDestroy()
    {
        m_InputManager.Click -= SelectingACell;
    }

    private void SelectingACell()
    {
        if (indicatoEnabled == false) return;

        Vector2 localCellPosition = m_Grid.ConvertWorldPositionToLocalCellPosition(m_InputManager.GetMousePosition(m_Grid.transform));
        
        if (m_Grid.CheckingCellActivity(localCellPosition) == false)
        {            
            m_SpriteRenderer.enabled = false;
            return;
        }
        else
        {
            if (indicatorVisualization == true)
            {
                m_SpriteRenderer.enabled = true;
            }
            
            SetPosition();
            CellSelected?.Invoke(localCellPosition);
        }
        
    }

    private void SetPosition()
    {
        transform.position = m_Grid.ConvertWorldPositionToCellWorldPosition(m_InputManager.GetMousePosition(m_Grid.transform));
    }

    public void IndicatorVisualization(bool value)
    {
        indicatorVisualization = value;
        m_SpriteRenderer.enabled = value;
    }

    public void BuildingSelect(Vector2 buildSize, Vector2 buildPosition)
    {
        transform.localScale = buildSize;
        Vector2 indicatorPosition = new Vector2();
        for (int i = 0; i < buildSize.x; i++)
        {
            indicatorPosition.x += buildPosition.x + i;
        }
        for (int i = 0; i < buildSize.y; i++)
        {            
            indicatorPosition.y += buildPosition.y + i;
        }
        indicatorPosition.x /= buildSize.x;
        indicatorPosition.y /= buildSize.y;
        m_SpriteRenderer.transform.position = m_Grid.ConvertCellLocalPositionToCellWorldPosition(indicatorPosition);
    }
    public void BuildingUnselect()
    {
        transform.localScale = Vector3.one * m_Grid.CellSize;
        m_SpriteRenderer.transform.localPosition = Vector3.zero;
    }

    private void IndicatorReset()
    {
        m_SpriteRenderer.enabled = true;
        transform.position = m_Grid.ConvertWorldPositionToCellWorldPosition(m_InputManager.GetCameraPosition(m_Grid.transform));
        CellSelected?.Invoke(m_Grid.ConvertWorldPositionToLocalCellPosition(m_InputManager.GetCameraPosition(m_Grid.transform)));
    }

    public void IndicatorDisabled()
    {
        indicatoEnabled = false;
        m_SpriteRenderer.enabled = false;
    }

    public void IndicatorEnabled()
    {
        indicatoEnabled = true;
        IndicatorReset();
    }
}
