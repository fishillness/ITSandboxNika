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
    }

    private void OnDestroy()
    {
        level.OnLevelResult.RemoveListener(OnLevelResult);
    }

    private void OnLevelResult(bool result)
    {
        endLevelPanel.SetActive(true);

        if (result)
            resultText.text = winText;
        else 
            resultText.text = loseText;
    }
}
