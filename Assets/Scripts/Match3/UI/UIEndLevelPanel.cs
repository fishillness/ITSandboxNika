using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIEndLevelPanel : MonoBehaviour
{
    [SerializeField] private Match3Level level;
    [SerializeField] private GameObject endLevelPanel;
    [SerializeField] private TextMeshProUGUI resultText;
    [SerializeField] private Button returnButton;

    private string winText = "Победа";
    private string loseText = "Проигрыш";

    private void Start()
    {
        endLevelPanel.SetActive(false);

        level.OnLevelResult.AddListener(OnLevelResult);
        returnButton.onClick.AddListener(OpenCity);
    }

    private void OnDestroy()
    {
        level.OnLevelResult.RemoveListener(OnLevelResult);
        returnButton.onClick.RemoveListener(OpenCity);
    }

    private void OnLevelResult(bool result)
    {
        endLevelPanel.SetActive(true);

        if (result)
            resultText.text = winText;
        else 
            resultText.text = loseText;
    }

    private void OpenCity()
    {
        SceneController.LoadSceneCity();
    }
}
