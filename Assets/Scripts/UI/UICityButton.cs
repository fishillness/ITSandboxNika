using UnityEngine;
using UnityEngine.UI;

public class UICityButton : MonoBehaviour
{
    [SerializeField] private GameObject warningPanel;
    [SerializeField] private Button confirmButton;
    [SerializeField] private Button refuseButton;

    private Button cityButton;

    private void Start()
    {
        cityButton = GetComponent<Button>();

        cityButton.gameObject.SetActive(SceneController.GetActiveScene() == SceneController.Match3Title);

        if (warningPanel != null)
            warningPanel.SetActive(false);

        cityButton.onClick.AddListener(OpenWarningPanel);

        if (confirmButton != null)
            confirmButton.onClick.AddListener(Confirm);
        if (refuseButton != null)
            refuseButton.onClick.AddListener(Refuse);
    
    }

    private void OnDestroy()
    {
        cityButton.onClick.RemoveListener(OpenWarningPanel);

        if (confirmButton != null)
            confirmButton.onClick.RemoveListener(Confirm);
        if (refuseButton != null)
            refuseButton.onClick.RemoveListener(Refuse);
    }

    private void OpenWarningPanel()
    {
        warningPanel.SetActive(true);
    }

    private void Confirm()
    {
        SceneController.LoadSceneCity();
    }

    private void Refuse()
    {
        warningPanel.SetActive(false);
    }
}
