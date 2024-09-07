using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingModeUI : MonoBehaviour
{
    [SerializeField] private RectTransform m_SpawnButton;
    [SerializeField] private RectTransform m_CancelButton;
    [SerializeField] private RectTransform m_SetButton;
    [SerializeField] private RectTransform m_DeleteButton;
    [SerializeField] private RectTransform m_ReplacementButton;
    [SerializeField] private RectTransform m_BuildingModeButton;

    public void StartPlacement()
    {
        DisablingReplacement();
        m_CancelButton.gameObject.SetActive(true);
        m_SetButton.gameObject.SetActive(true);
        m_SpawnButton.gameObject.SetActive(false);
        m_BuildingModeButton.gameObject.SetActive(false);
    }
    public void EndPlacement()
    {
        m_CancelButton.gameObject.SetActive(false);
        m_SetButton.gameObject.SetActive(false);
        m_SpawnButton.gameObject.SetActive(true);
        m_BuildingModeButton.gameObject.SetActive(true);
    }

    public void DisablingPlacement()
    {
        m_SetButton.gameObject.SetActive(false);
        m_SpawnButton.gameObject.SetActive(false);
        m_BuildingModeButton.gameObject.SetActive(false);
    }

    public void EnablingPlacement()
    {
        m_SetButton.gameObject.SetActive(true);
        m_SpawnButton.gameObject.SetActive(false);
        m_BuildingModeButton.gameObject.SetActive(false);
    }
    public void DisablingReplacement()
    {
        m_DeleteButton.gameObject.SetActive(false);
        m_ReplacementButton.gameObject.SetActive(false);
    }

    public void EnablingReplacement()
    {
        m_DeleteButton.gameObject.SetActive(true);
        m_ReplacementButton.gameObject.SetActive(true);
    }
}
