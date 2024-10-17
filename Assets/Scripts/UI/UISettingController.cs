using UnityEngine;
using UnityEngine.UI;

public class UISettingController : MonoBehaviour
{
    [SerializeField] private GameObject settingPanel;
    [SerializeField] private Button[] openCloseButtons;

    private void Start()
    {
        settingPanel.SetActive(false);

        foreach (var button in openCloseButtons)
        {
            button.onClick.AddListener(ChangePanelActive);
        }
    }

    private void OnDestroy()
    {
        foreach (var button in openCloseButtons)
        {
            button.onClick.RemoveListener(ChangePanelActive);
        }
    }

    private void ChangePanelActive()
    {
        settingPanel.SetActive(!settingPanel.activeSelf);
    }
}
