using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UITaskInfo : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI textNumber;

    private TaskInfo taskInfo;

    public void SetProperties(Sprite sprite, int number, TaskInfo taskInfo)
    {
        image.sprite = sprite;
        textNumber.text = number.ToString();
        this.taskInfo = taskInfo;
    }

    public void UpdateNumberText(int number)
    {
        textNumber.text = number.ToString();
    }

    public bool IsThisUITaskInfo(TaskInfo taskInfo)
    {
        if (this.taskInfo == taskInfo)
            return true;

        return false;
    }
}
