using UnityEngine;
using UnityEngine.Events;

public class Match3Level : MonoBehaviour,
    IDependency<FieldController>, IDependency<TaskInfoCounter>, IDependency<UIMatch3LevelPanel>,
    IDependency<Match3LevelManager>, IDependency<ValueManager>
{
    [HideInInspector]
    public UnityEvent OnStopMoves;
    [HideInInspector]
    public UnityEvent<bool> OnLevelResult;

    [SerializeField] private int moves;
    [SerializeField] private TaskInfo[] taskInfos;

    private FieldController field;
    private TaskInfoCounter taskInfoCounter;
    private UIMatch3LevelPanel levelPanel;
    private Match3LevelManager levelManager;
    private ValueManager valueManager;

    #region Constructs
    public void Construct(FieldController field) => this.field = field;
    public void Construct(TaskInfoCounter taskInfoCounter) => this.taskInfoCounter = taskInfoCounter;
    public void Construct(UIMatch3LevelPanel levelPanel) => this.levelPanel = levelPanel;
    public void Construct(Match3LevelManager levelManager) => this.levelManager = levelManager;
    public void Construct(ValueManager valueManager) => this.valueManager = valueManager;
    #endregion

    private int remainingMoves;
    private bool areTasksComplite = false;
    private bool areMovesComplite = false;
    private bool isLevelEnd = false;

    private void Awake()
    {
        if (levelManager == null || valueManager == null)
            GlobalGameDependenciesContainer.Instance.Rebind(this);
    }

    private void Start()
    {
        remainingMoves = moves;
        field.OnMove.AddListener(OnMove);
        field.OnDropEnd.AddListener(OnDropEnd);

        taskInfoCounter.InitTasks(taskInfos);
        taskInfoCounter.OnAllTaskComplite.AddListener(OnAllTaskComplite);

        levelPanel.SetMoves(moves);
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
        OnLevelResult?.Invoke(false);
    }

    private void Win()
    {
        Debug.Log("Win");
        valueManager.AddResources(levelManager.CurrentLevelInfo.Coins, levelManager.CurrentLevelInfo.Boards,
            levelManager.CurrentLevelInfo.Bricks, levelManager.CurrentLevelInfo.Nails);
        Debug.Log("Resourcesa added");
        levelManager.LevelUp();
        Debug.Log("Levelup");
        OnLevelResult?.Invoke(true);
    }

    private void DetermineWinOrLose()
    {
        if (isLevelEnd) return;
        OnStopMoves?.Invoke();

        if (field.IsDroppingContinue == true) return;

        if (areTasksComplite)
        {
            isLevelEnd = true;
            Win();
        }
        else if (areMovesComplite)
        {
            isLevelEnd = true;
            Lose();
        }
    }
}
