using UnityEngine;

public class ConstructionModeActivator : MonoBehaviour,
    IDependency<InputController>
{
    [SerializeField] private GameObject m_BuildingGrid;
    [SerializeField] private Canvas m_BuildingModeCanvas;
    [SerializeField] private Indicator m_Indicator;
    [SerializeField] private Canvas m_MainCanvas; 

    private InputController m_InputController;

    #region Constructs
    public void Construct(InputController m_InputController) => this.m_InputController = m_InputController;
    #endregion

    private void Start()
    {
        ConstructionModeDeactivate();
    }

    public void ConstructionModeActivate()
    {
        m_InputController.SetInputControllerMode(InputControllerModes.ConstructionMode);
        m_MainCanvas.enabled = false;
        m_BuildingGrid.gameObject.SetActive(true);
        m_BuildingModeCanvas.enabled = true;
        m_Indicator.IndicatorEnabled();
    }
    public void ConstructionModeDeactivate()
    {
        m_InputController.SetInputControllerMode(InputControllerModes.CityMode);
        m_MainCanvas.enabled = true;
        m_BuildingModeCanvas.enabled = false;
        m_BuildingGrid.gameObject.SetActive(false);
        m_Indicator.IndicatorDisabled();
    }
}
