using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TaskInfoCounter : MonoBehaviour
{
    [HideInInspector]
    public UnityEvent OnAllTaskComplite;

    [SerializeField] private PieceCounter pieceCounter;

    private Dictionary<PieceType, int> pieceCount;
    private int taskCount;
    private bool isAllTaskComplite = false;

    private void Start()
    {
        pieceCounter.OnPieceRemoved.AddListener(OnPieceRemoved);
    }

    private void OnDestroy()
    {
        pieceCounter.OnPieceRemoved.RemoveListener(OnPieceRemoved);
    }

    public void InitTasks(TaskInfo[] taskInfos)
    {
        pieceCount = new Dictionary<PieceType, int>();

        foreach (var taskInfo in taskInfos)
        {
            if (!pieceCount.ContainsKey(taskInfo.type))
            {
                pieceCount.Add(taskInfo.type, taskInfo.count);
            }
        }

        taskCount = pieceCount.Count;
    }

    private void OnPieceRemoved(PieceType type)
    {
        if (isAllTaskComplite) return;
        if (!pieceCount.ContainsKey(type)) return;
        if (pieceCount[type] == 0) return;

        pieceCount[type] -= 1;

        if (pieceCount[type] == 0)
            taskCount--;


        if (taskCount == 0)
        {
            isAllTaskComplite = true;
            OnAllTaskComplite?.Invoke();
        }
    }
}
