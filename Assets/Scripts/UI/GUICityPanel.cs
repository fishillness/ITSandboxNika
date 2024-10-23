using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GUICityPanel : MonoBehaviour,
    IDependency<Match3LevelManager>, IDependency<ValueManager>
{
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button match3Button;
    [SerializeField] private TextMeshProUGUI match3LevelNumberText;
    [SerializeField] private TextMeshProUGUI match3EnergyCostText;

    private Match3LevelManager levelManager;
    private ValueManager valueManager;

    #region Constructs
    public void Construct(Match3LevelManager levelManager) => this.levelManager = levelManager;
    public void Construct(ValueManager valueManager) => this.valueManager = valueManager;
    #endregion

    private void Start()
    {
        match3Button.onClick.AddListener(OpenMatch3);

        if (mainMenuButton != null)
            mainMenuButton.onClick.AddListener(OpenMainMenu);

        valueManager.GetEventOnValueChangeByType(ValueType.Energy).AddListener(SetMatch3ButtonParameter);

        SetMatch3ButtonParameter(0);
    }

    private void OnDestroy()
    {
        match3Button.onClick.RemoveListener(OpenMatch3);

        if (mainMenuButton != null)
            mainMenuButton.onClick.RemoveListener(OpenMainMenu);
    }

    private void OpenMatch3()
    {
        valueManager.DeleteResources(0, 0, 0, 0, levelManager.CurrentLevelInfo.CostInEnergy);
        SceneController.LoadMatch3();
    }

    private void OpenMainMenu()
    {
        SceneController.LoadMainMenu();
    }

    private void SetMatch3ButtonParameter(int value)
    {
        match3Button.interactable = levelManager.HaveUnCompletedLevels;

        if (levelManager.HaveUnCompletedLevels == false)
        {
            match3LevelNumberText.text = "";
            match3EnergyCostText.text = "";
        }
        else
        {
            match3Button.interactable = valueManager.EnergyCount >= levelManager.CurrentLevelInfo.CostInEnergy;

            match3LevelNumberText.text = levelManager.CurrentLevel.ToString();
            match3EnergyCostText.text = $"-{levelManager.CurrentLevelInfo.CostInEnergy}";
        }
    }
}
