using UnityEngine;
using UnityEngine.Events;

public class Match3Level : MonoBehaviour
{
    [HideInInspector]
    public UnityEvent OnLevelEnd;

    [SerializeField] private int moves;
    [SerializeField] private TaskInfo[] taskInfos;

    [SerializeField] private FieldController field;
    [SerializeField] private TaskInfoCounter taskInfoCounter;
    [SerializeField] private UIMatch3LevelPanel levelPanel;

    private int remainingMoves;
    private bool areTasksComplite = false;
    private bool areMovesComplite = false;

    private void Start()
    {
        remainingMoves = moves;
        field.OnMove.AddListener(OnMove);
        field.OnDropEnd.AddListener(OnDropEnd);

        taskInfoCounter.InitTasks(taskInfos);
        taskInfoCounter.OnAllTaskComplite.AddListener(OnAllTaskComplite);

        levelPanel.SetProperties(-1, moves);
    }

    private void OnDestroy()
    {
        field.OnMove.RemoveListener(OnMove);
        field.OnDropEnd.RemoveListener(OnDropEnd);
        taskInfoCounter.OnAllTaskComplite.RemoveListener(OnAllTaskComplite);
    }

    private void OnMove()
    {
        if (remainingMoves == 0) return;

        remainingMoves--;
        levelPanel.UpdateMoves(remainingMoves);

        if (remainingMoves == 0)
        {
            areMovesComplite = true;

            DetermineWinOrLose();
        }
    }

    private void OnAllTaskComplite()
    {
        areTasksComplite = true;

        DetermineWinOrLose();
    }

    private void OnDropEnd()
    {
        if (areTasksComplite || areMovesComplite)
            DetermineWinOrLose();
    }

    private void Lose()
    {
        Debug.Log("Lose");
    }

    private void Win()
    {
        Debug.Log("Win");
    }

    private void DetermineWinOrLose()
    {
        OnLevelEnd?.Invoke();

        if (field.IsDroppingContinue == true) return;

        if (areTasksComplite)
            Win();
        else if (areMovesComplite)
            Lose();
    }
}
