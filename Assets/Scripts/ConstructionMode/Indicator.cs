using UnityEngine;
using UnityEngine.Events;

public class Indicator : MonoBehaviour
{
    public event UnityAction<Vector2> CellSelected;

    [SerializeField] private ConstructionGrid m_Grid;
    [SerializeField] private InputController m_InputController;
    [SerializeField] private SpriteRenderer m_SpriteRenderer;
    [SerializeField] private Camera m_Camera;

    private Vector3 worldPosition;
    private bool indicatorVisualization = true;
    private bool indicatoEnabled = true;
    private void Start()
    {
        m_InputController.ClickEventInConstructionMode += SelectingACell;
    }
    private void OnDestroy()
    {
        m_InputController.ClickEventInConstructionMode -= SelectingACell;
    }

    private void SelectingACell()
    {
        if (indicatoEnabled == false) return;

        Vector2 localCellPosition = m_Grid.ConvertWorldPositionToLocalCellPosition(GetMousePosition(m_Grid.transform));
        
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
        transform.position = m_Grid.ConvertWorldPositionToCellWorldPosition(GetMousePosition(m_Grid.transform));
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
        transform.position = m_Grid.ConvertWorldPositionToCellWorldPosition(GetCameraPosition(m_Grid.transform));
        CellSelected?.Invoke(m_Grid.ConvertWorldPositionToLocalCellPosition(GetCameraPosition(m_Grid.transform)));
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

    public Vector3 GetMousePosition(Transform targetPlane)
    {
        Plane plane = new Plane(targetPlane.up, targetPlane.position);
        Ray ray = m_Camera.ScreenPointToRay(Input.mousePosition);
        if (plane.Raycast(ray, out float position))
        {
            worldPosition = ray.GetPoint(position);
        }
        return worldPosition;
    }

    public Vector3 GetCameraPosition(Transform targetPlane)
    {
        Plane plane = new Plane(targetPlane.up, targetPlane.position);
        Ray ray = new Ray(m_Camera.transform.position, m_Camera.transform.forward);
        if (plane.Raycast(ray, out float position))
        {
            worldPosition = ray.GetPoint(position);
        }
        return worldPosition;
    }
}
