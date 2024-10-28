using UnityEngine;
using UnityEngine.UI; 

public class UISettingController : MonoBehaviour,
    IDependency<InputController>
{
    [SerializeField] private GameObject settingPanel;
    [SerializeField] private Button openButton;
    [SerializeField] private Button closeButton;

    private InputController inputController;

    #region Constructs
    public void Construct(InputController inputController) => this.inputController = inputController;
    #endregion

    private void Start()
    {
        settingPanel.SetActive(false);
        
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
        inputController.SetInputControllerMode(InputControllerModes.NotepadMode);
        settingPanel.SetActive(true);
    }

    private void ClosePanel()
    {
        inputController.SetInputControllerMode(InputControllerModes.CityMode);
        settingPanel?.SetActive(false);
    }
}
