using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIEndLevelPanel : MonoBehaviour,
    IDependency<Match3Level>,
    IDependency<Match3LevelManager>
{
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private Button[] returnButtons;
    [SerializeField] private Button retryButton;

    [Header("ResourcesText")]
    [SerializeField] private TextMeshProUGUI coinsText;
    [SerializeField] private TextMeshProUGUI boardsText;
    [SerializeField] private TextMeshProUGUI bricksText;
    [SerializeField] private TextMeshProUGUI nailsText;

    private Match3Level level;
    private Match3LevelManager levelManager;

    #region Constructs
    public void Construct(Match3Level level) => this.level = level;
    public void Construct(Match3LevelManager levelManager) => this.levelManager = levelManager;
    #endregion

    private void Awake()
    {
        if (levelManager == null)
            GlobalGameDependenciesContainer.Instance.Rebind(this);

        SetReceivedResource(levelManager.CurrentLevelInfo.Coins, levelManager.CurrentLevelInfo.Boards,
            levelManager.CurrentLevelInfo.Bricks, levelManager.CurrentLevelInfo.Nails);
    }

    private void Start()
    {
        winPanel.SetActive(false);
        losePanel.SetActive(false);

        level.OnLevelResult.AddListener(OnLevelResult);
        retryButton.onClick.AddListener(RetryLevel);

        foreach (var button in returnButtons)
        {
            button.onClick.AddListener(OpenCity);
        }
    }

    private void OnDestroy()
    {
        level.OnLevelResult.RemoveListener(OnLevelResult);
        retryButton.onClick.RemoveListener(RetryLevel);

        foreach (var button in returnButtons)
        {
            button.onClick.RemoveListener(OpenCity);
        }
    }

    private void SetReceivedResource(int coins, int boards, int bricks, int nails)
    {
        coinsText.text = coins.ToString();
        boardsText.text = boards.ToString();
        bricksText.text = bricks.ToString();
        nailsText.text = nails.ToString();
    }

    private void OnLevelResult(bool result)
    {
        if (result)
        {
            winPanel.SetActive(true);
        }
        else
        {
            losePanel.SetActive(true);
        }
    }

    private void RetryLevel()
    {
        //TODO: отнять энергию
        SceneController.Restart();
    }

    private void OpenCity()
    {
        SceneController.LoadSceneCity();
    }
}
