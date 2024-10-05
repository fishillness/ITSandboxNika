using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GUICityPanel : MonoBehaviour,
    IDependency<Match3LevelManager>
{
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button match3Button;
    [SerializeField] private TextMeshProUGUI match3ButtonText;

    private Match3LevelManager levelManager;

    #region Constructs
    public void Construct(Match3LevelManager levelManager) => this.levelManager = levelManager;
    #endregion

    private void Start()
    {
        match3Button.onClick.AddListener(OpenMatch3);
        mainMenuButton.onClick.AddListener(OpenMainMenu);
        
        match3Button.interactable = levelManager.HaveUnCompletedLevels;
        match3ButtonText.text = levelManager.CurrentLevel.ToString();

        if (levelManager.HaveUnCompletedLevels == false)
            match3ButtonText.text = "";
    }

    private void OnDestroy()
    {
        match3Button.onClick.RemoveListener(OpenMatch3);
        mainMenuButton.onClick.RemoveListener(OpenMainMenu);
    }

    private void OpenMatch3()
    {
        SceneController.LoadMatch3();
    }

    private void OpenMainMenu()
    {
        SceneController.LoadMainMenu();
    }
}
