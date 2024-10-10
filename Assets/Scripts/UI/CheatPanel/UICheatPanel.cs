using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICheatPanel : MonoBehaviour,
    IDependency<ValueManager>, IDependency<Match3LevelManager>
{
    [SerializeField] private GameObject cheatPanel;
    [SerializeField] private Button cheatButton;
    [SerializeField] private Button restartButton;

    [Header("Resources")]
    [SerializeField] private int value;
    [SerializeField] private TextMeshProUGUI addValueText;
    [Header("Add resources")]
    [SerializeField] private Button coinsAddButton;
    [SerializeField] private Button boardsAddButton;
    [SerializeField] private Button bricksAddButton;
    [SerializeField] private Button nailsAddButton;
    [SerializeField] private Button energyAddButton;
    [Header("Remove resources")]
    [SerializeField] private Button coinsRemoveButton;
    [SerializeField] private Button boardsRemoveButton;
    [SerializeField] private Button bricksRemoveButton;
    [SerializeField] private Button nailsRemoveButton;
    [SerializeField] private Button energyRemoveButton;

    [Header("Level")]
    [SerializeField] private UILevelButton levelButtonPrefab;
    [SerializeField] private Transform levelButtonsGroup;

    private Match3LevelManager levelManager;
    private ValueManager valueManager;
    private List<UILevelButton> levelButtons;

    #region Constructs
    public void Construct(Match3LevelManager levelManager)
    {
        this.levelManager = levelManager;
        CreateLevelButtons();
    }
    public void Construct(ValueManager valueManager) => this.valueManager = valueManager;
    #endregion

    private void Awake()
    {
        addValueText.text = $" Добавить +{value}";
        cheatPanel.SetActive(false);

        cheatButton.onClick.AddListener(OpenPanel);
        restartButton.onClick.AddListener(RestartButton);
        
        coinsAddButton.onClick.AddListener(CoinsAdd);
        boardsAddButton.onClick.AddListener(BoardsAdd);
        bricksAddButton.onClick.AddListener(BricksAdd);
        nailsAddButton.onClick.AddListener(NailsAdd);
        energyAddButton.onClick.AddListener(EnergyAdd);

        coinsRemoveButton.onClick.AddListener(CoinsRemove);
        boardsRemoveButton.onClick.AddListener(BoardsRemove);
        bricksRemoveButton.onClick.AddListener(BricksRemove);
        nailsRemoveButton.onClick.AddListener (NailsRemove);
        energyRemoveButton.onClick.AddListener(EnergyRemove);
    }

    private void OnDestroy()
    {
        foreach(UILevelButton button in levelButtons)
        {
            button.OnLevelButtonClick -= OnLevelButtonCLick;
        }
    }

    private void OpenPanel()
    {
        cheatPanel.SetActive(true);
        cheatButton.gameObject.SetActive(false);
    }

    private void RestartButton()
    {
        SceneController.Restart();
    }

    private void CreateLevelButtons()
    {
        levelButtons = new List<UILevelButton>();

        for (int i = 0;  i < levelManager.LevelList.Levels.Length; i++)
        {
            UILevelButton button = Instantiate(levelButtonPrefab, levelButtonsGroup);

            button.SetProperties(i);
            button.OnLevelButtonClick += OnLevelButtonCLick;
            levelButtons.Add(button);
        }
    }

    private void OnLevelButtonCLick(int levelNumber)
    {
        levelManager.SetLevel(levelNumber);
    }

    private void CoinsAdd()
    {
        valueManager.AddResources(value, 0, 0, 0, 0);
    }
    private void BoardsAdd()
    {
        valueManager.AddResources(0, value, 0, 0, 0);
    }
    private void BricksAdd()
    {
        valueManager.AddResources(0, 0, value, 0, 0);
    }
    private void NailsAdd()
    {
        valueManager.AddResources(0, 0, 0, value, 0);
    }
    private void EnergyAdd()
    {
        valueManager.AddResources(0, 0, 0, 0, value);
    }

    private void CoinsRemove()
    {
        valueManager.DeleteResources(valueManager.CoinsCount, 0, 0, 0, 0);
    }
    private void BoardsRemove()
    {
        valueManager.DeleteResources(0, valueManager.BoardsCount, 0, 0, 0);
    }
    private void BricksRemove()
    {
        valueManager.DeleteResources(0, 0, valueManager.BricksCount, 0, 0);
    }
    private void NailsRemove()
    {
        valueManager.DeleteResources(0, 0, 0, valueManager.NailsCount, 0);
    }
    private void EnergyRemove()
    {
        valueManager.DeleteResources(0, 0, 0, 0, valueManager.EnergyCount);
    }
}
