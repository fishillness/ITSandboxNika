using UnityEngine;
using UnityEngine.Events;

public class TaskInfoCounter : MonoBehaviour,
    IDependency<PieceCounter>, IDependency<UIMatch3LevelPanel>
{
    [HideInInspector]
    public UnityEvent OnAllTaskComplite;

    private PieceCounter pieceCounter;
    private UIMatch3LevelPanel levelPanel;


    #region Constructs
    public void Construct(PieceCounter pieceCounter) => this.pieceCounter = pieceCounter;
    public void Construct(UIMatch3LevelPanel levelPanel) => this.levelPanel = levelPanel;
    #endregion

    private TaskInfo[] taskInfos;
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
        this.taskInfos = taskInfos;
        levelPanel.InitUITaskInfo();

        foreach (var taskInfo in this.taskInfos)
        {
            taskInfo.SetProperties();
            levelPanel.AddTaskInfo(taskInfo);
        }

        taskCount = this.taskInfos.Length;
    }

    private void OnPieceRemoved(Piece piece)
    {
        if (isAllTaskComplite) return;

        foreach (var taskInfo in taskInfos)
        {
            if (taskInfo.Type == piece.Type && taskInfo.TaskType == TaskInfoType.ByType)
            {
                RemovePiece(taskInfo);
            }
            else if (taskInfo.Type == piece.Type && taskInfo.TaskType == TaskInfoType.ByColor
                && piece.IsColorable)
            {
                if (piece.Colorable.Color == taskInfo.ColorType)
                {
                    RemovePiece(taskInfo);
                }
            }
        }

        if (taskCount == 0)
        {
            isAllTaskComplite = true;
            OnAllTaskComplite?.Invoke();
        }

    }

    private void RemovePiece(TaskInfo taskInfo)
    {
        bool isLastPiece = taskInfo.RemovePiece();
        levelPanel.UpdateTaskInfo(taskInfo);

        if (isLastPiece)
            taskCount--;
    }
}
