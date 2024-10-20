using UnityEngine;
using UnityEngine.UI;

public class UISecretPanel : MonoBehaviour
{
    [SerializeField] private GameObject secretPanel;
    [SerializeField] private Button openButton;
    [SerializeField] private Button closeButton;

    private void Awake()
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
        secretPanel.SetActive(true);
    }

    private void ClosePanel()
    {
        secretPanel.SetActive(false);
    }
}
