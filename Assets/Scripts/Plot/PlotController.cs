using System;
using UnityEngine;

public class PlotController : MonoBehaviour
{
    public const string Filename = "Plot";

    [SerializeField] private PlotList plotList;

    [SerializeField] private MissionController missionController;
    [SerializeField] private DialogController dialogController;
    [SerializeField] private InputController inputController;

    private int currentAction;
    private bool isDialogActive;
    private bool haveUncomplitedPlotAction => currentAction < plotList.PlotActions.Length - 1;

    [Serializable]
    public class PlotData
    {
        public int currentAction;
    }

    private void Start()
    {
        missionController.OnMissionEnd.AddListener(GoToNextAction);
        inputController.OnInputControllerModeChanges += OnInputControllerModeChanges;
        dialogController.OnDialogEnd += OnDialogEnd;
        LoadPlotData();
    }

    private void OnDestroy()
    {
        missionController.OnMissionEnd.RemoveListener(GoToNextAction);
        inputController.OnInputControllerModeChanges -= OnInputControllerModeChanges;
        dialogController.OnDialogEnd -= OnDialogEnd;
    }

    private void GoToNextAction()
    {
        if (!haveUncomplitedPlotAction) return;

        currentAction++;

        if (plotList.PlotActions[currentAction].ActionType == PlotActionType.Dialog)
        {

        }
        else if (plotList.PlotActions[currentAction].ActionType == PlotActionType.Mission)
        {
            missionController.AddNewMission(plotList.PlotActions[currentAction].Mission);
        }

        SavePlotData();
    }

    private void SavePlotData()
    {
        PlotData data = new PlotData();

        data.currentAction = currentAction;

        Saver<PlotData>.Save(Filename, data);
    }

    private void LoadPlotData()
    {
        PlotData data = new PlotData();

        if (Saver<PlotData>.TryLoad(Filename, ref data) == false)
        {
            currentAction = 0;
            if (plotList.PlotActions[currentAction].ActionType == PlotActionType.Dialog)
            {

            }
            else if (plotList.PlotActions[currentAction].ActionType == PlotActionType.Mission)
            {
                missionController.AddNewMission(plotList.PlotActions[currentAction].Mission);
            }

            SavePlotData();
        }
        else
        {
            currentAction = data.currentAction;

            ActivateDialog();
        }
    }

    #region Dialog activation

    private void OnInputControllerModeChanges(InputControllerModes controllerModes)
    {
        ActivateDialog();
    }

    private void ActivateDialog()
    {
        if (plotList.PlotActions[currentAction].ActionType != PlotActionType.Dialog) return;
        if (SceneController.GetActiveScene() != SceneController.CitySceneTitle) return;
        if (inputController.InputControllerMode != InputControllerModes.CityMode) return;
        if (isDialogActive) return;

        dialogController.StartDialogue(plotList.PlotActions[currentAction].Dialog);
        isDialogActive = true;
    }

    private void OnDialogEnd()
    {
        if (plotList.PlotActions[currentAction].ActionType != PlotActionType.Dialog) return;

        isDialogActive = false;
        GoToNextAction();
    }

    #endregion
}
