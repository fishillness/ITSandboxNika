using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIMatch3LevelPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textLevelNumber;
    [SerializeField] private TextMeshProUGUI textMoves;
    [SerializeField] private GameObject taskGroup;
    [SerializeField] private UITaskInfo taskInfoPrefab;

    private List<UITaskInfo> uiTaskInfos;

    private void Start()
    {
        uiTaskInfos = new List<UITaskInfo>();
    }

    public void SetProperties(int levelNumber, int startMoves)
    {
        textLevelNumber.text = levelNumber.ToString();
        textMoves.text = startMoves.ToString();
    }

    public void UpdateMoves(int moves)
    {
        Debug.Log($"UpdateMoves: {moves}");
        textMoves.text = moves.ToString();
    }

    public void AddTaskInfo(TaskInfo taskInfo)
    {
        UITaskInfo uiTaskInfo = Instantiate(taskInfoPrefab, taskGroup.transform);
        uiTaskInfo.SetProperties(taskInfo.Sprite, taskInfo.Count, taskInfo);
        uiTaskInfos.Add(uiTaskInfo);
    }

    public void UpdateTaskInfo(TaskInfo taskInfo)
    {
        foreach (var uiTaskInfo in uiTaskInfos)
        {
            if (uiTaskInfo.IsThisUITaskInfo(taskInfo))
            {
                uiTaskInfo.UpdateNumberText(taskInfo.CurrentCount);
            }
        }
    }
}
