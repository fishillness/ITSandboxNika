using UnityEngine;
using UnityEngine.UI;

public class UIShopContoller : MonoBehaviour
{
    [SerializeField] private GameObject shopPanel;
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
        shopPanel.SetActive(true);
    }

    private void ClosePanel()
    {
        shopPanel.SetActive(false);
    }
}
