using UnityEngine;
using UnityEngine.UI;

public class UIMissionController : MonoBehaviour
{
    [SerializeField] private GameObject missionPanel;
    [SerializeField] private Button openButton;
    [SerializeField] private Button closeButton;

    private void Start()
    {
        ClosePanel();

        openButton.onClick.AddListener(OpenPanel);
        closeButton.onClick.AddListener(ClosePanel);
    }

    private void OnDestroy()
    {
        openButton.onClick.RemoveListener(OpenPanel);
        closeButton.onClick.RemoveListener(ClosePanel);
    }

    private void OpenPanel()
    {
        missionPanel.SetActive(true);
    }

    private void ClosePanel()
    {
        missionPanel.SetActive(false);
    }
}
