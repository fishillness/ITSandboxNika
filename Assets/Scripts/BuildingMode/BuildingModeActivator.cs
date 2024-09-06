using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingModeActivator : MonoBehaviour
{
    [SerializeField] private GameObject m_BuildingGrid;
    [SerializeField] private Canvas m_BuildingModeCanvas;
    [SerializeField] private Indicator m_Indicator;

    private bool buildingMode;

    private void Start()
    {
        BuildingModeDeactivate();
    }

    private void BuildingModeActivate()
    {
        buildingMode = true;
        m_BuildingGrid.gameObject.SetActive(true);
        m_BuildingModeCanvas.enabled = true;
        m_Indicator.IndicatorEnabled();
    }
    private void BuildingModeDeactivate()
    {
        buildingMode = false;        
        m_BuildingModeCanvas.enabled = false;
        m_BuildingGrid.gameObject.SetActive(false);
        m_Indicator.IndicatorDisabled();
    }

    public void SwitchingBuildingMode()
    {
        if (buildingMode == true)
        {
            BuildingModeDeactivate();
        }
        else
        {
            BuildingModeActivate();
        }
    }
}
