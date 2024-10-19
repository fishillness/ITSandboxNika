using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMissionInfo : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI missionText;
    [SerializeField] private TextMeshProUGUI progressText;
    [SerializeField] private TextMeshProUGUI buttonText;
    [SerializeField] private Button missionButton;

    private int id;
    private string inProgress = "Перейти";
    private string finish = "Выполнить";
    private UIMissionController uiMissionController;

    public int Id => id;

    private void Start()
    {
        missionButton.onClick.AddListener(OnButtonClick);
    }

    private void OnDestroy()
    {
        missionButton.onClick.RemoveListener(OnButtonClick);
    }

    public void SetProperties(int id, string missionText, int maxValue, int currentValue, bool isFinish, UIMissionController uiMissionController)
    {
        this.id = id;
        this.missionText.text = missionText;
        this.progressText.text = $"{currentValue}/{maxValue}";
        this.uiMissionController = uiMissionController;

        if (isFinish)
            this.buttonText.text = finish;
        else
            this.buttonText.text = inProgress;
    }

    public void UpdateTexts(int maxValue, int currentValue, bool isFinish)
    {
        this.progressText.text = $"{currentValue}/{maxValue}";

        if (isFinish)
            this.buttonText.text = finish;
        else
            this.buttonText.text = inProgress;
    }

    public void OnButtonClick()
    {
        Debug.Log($"buttonText: {missionText.text}, finish:{finish}, =? {buttonText.text == finish}");
        if (buttonText.text == finish)
        {
            uiMissionController.OnMissionFinishButtonClick(id);
        }
        else
        {
            Debug.Log("Перейти не реализовано");
        }
    }
}
