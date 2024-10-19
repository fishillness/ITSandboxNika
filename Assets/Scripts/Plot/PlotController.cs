using System;
using UnityEngine;

public class PlotController : MonoBehaviour
{
    public const string Filename = "Plot";

    [SerializeField] private PlotList plotList;
    [SerializeField] private MissionController missionController;

    private int currentAction;
    private bool haveUncomplitedPlotAction => currentAction < plotList.PlotActions.Length - 1;

    [Serializable]
    public class PlotData
    {
        public int currentAction;
    }

    private void Start()
    {
        LoadPlotData();
        missionController.OnMissionEnd.AddListener(GoToNextAction);
    }

    private void OnDestroy()
    {
        missionController.OnMissionEnd.RemoveListener(GoToNextAction);
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
        }
    }
}
