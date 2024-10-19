using UnityEngine;
using UnityEngine.UI;

public class UIMissionController : MonoBehaviour,
    IDependency<InputController>
{
    [SerializeField] private GameObject missionPanel;
    [SerializeField] private Button openButton;
    [SerializeField] private Button closeButton;

    private InputController m_InputController;

    #region Constructs
    public void Construct(InputController m_InputController) => this.m_InputController = m_InputController;
    #endregion

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
        m_InputController.SetInputControllerMode(InputControllerModes.NotepadMode);
        missionPanel.SetActive(true);
    }

    private void ClosePanel()
    {
        m_InputController.SetInputControllerMode(InputControllerModes.CityMode);
        missionPanel.SetActive(false);
    }
}
