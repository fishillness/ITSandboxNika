using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildModeUI : MonoBehaviour
{
    [SerializeField] private RectTransform SpawnButton;
    [SerializeField] private RectTransform CancelButton;
    [SerializeField] private RectTransform SetButton;

    public void StartPlacement()
    {
        CancelButton.gameObject.SetActive(true);
        SetButton.gameObject.SetActive(true);
        SpawnButton.gameObject.SetActive(false);
    }
    public void EndPlacement()
    {
        CancelButton.gameObject.SetActive(false);
        SetButton.gameObject.SetActive(false);
        SpawnButton.gameObject.SetActive(true);
    }

    public void DisablingPlacement()
    {
        SetButton.gameObject.SetActive(false);
    }

    public void EnablingPlacement()
    {
        SetButton.gameObject.SetActive(true);
    }
}
