using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMatch3LevelInfoPanel : MonoBehaviour,
    IDependency<Match3LevelManager>, IDependency<ValueManager>,
    IDependency<InputController>
{
    [SerializeField] private Button openPanelButton;
    [SerializeField] private TextMeshProUGUI openPanelButtonText;
    [SerializeField] private Button closePanelButton;

    [SerializeField] private GameObject panel;
    [SerializeField] private TextMeshProUGUI numberLevelText;
    [SerializeField] private TextMeshProUGUI energyCostText;
    [SerializeField] private GameObject energyWarning;
    [SerializeField] private Button playbutton;

    private Match3LevelManager levelManager;
    private ValueManager valueManager;
    private InputController inputController;

    #region Constructs
    public void Construct(Match3LevelManager levelManager) => this.levelManager = levelManager;
    public void Construct(ValueManager valueManager) => this.valueManager = valueManager;
    public void Construct(InputController inputController) => this.inputController = inputController;
    #endregion

    private void Start()
    {   
        openPanelButton.onClick.AddListener(OpenPanel);
        closePanelButton.onClick.AddListener(ClosePanel);
        playbutton.onClick.AddListener(OpenMatch3);
        valueManager.GetEventOnValueChangeByType(ValueType.Energy).AddListener(UpdateEnergy);

        panel.SetActive(false);
        SetParameters();
    }

    private void OnDestroy()
    {
        openPanelButton.onClick.RemoveListener(OpenPanel);
        closePanelButton.onClick.RemoveListener(ClosePanel);
        playbutton.onClick.RemoveListener(OpenMatch3);
        valueManager.GetEventOnValueChangeByType(ValueType.Energy).RemoveListener(UpdateEnergy);
    }

    private void OpenPanel()
    {
        inputController.SetInputControllerMode(InputControllerModes.NotepadMode);
        panel.SetActive(true);
    }

    private void ClosePanel()
    {
        inputController.SetInputControllerMode(InputControllerModes.CityMode);
        panel.SetActive(false);
    }

    private void OpenMatch3()
    {
        valueManager.DeleteResources(0, 0, 0, 0, levelManager.CurrentLevelInfo.CostInEnergy);
        SceneController.LoadMatch3();
    }


    private void SetParameters()
    {
        openPanelButtonText.text = levelManager.CurrentLevel.ToString();
        numberLevelText.text = $"Уровень {levelManager.CurrentLevel}";
        energyCostText.text = $"-{levelManager.CurrentLevelInfo.CostInEnergy}";
        UpdateEnergy(0);
    }

    private void UpdateEnergy(int value)
    {
        playbutton.interactable = valueManager.EnergyCount >= levelManager.CurrentLevelInfo.CostInEnergy;
        energyWarning.SetActive(valueManager.EnergyCount < levelManager.CurrentLevelInfo.CostInEnergy);
    }
}
