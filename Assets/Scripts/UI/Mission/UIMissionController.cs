using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMissionController : MonoBehaviour,
    IDependency<InputController>, IDependency<MissionController>
{
    [SerializeField] private GameObject missionPanel;
    [SerializeField] private Transform missionGroup;
    [SerializeField] private UIMissionInfo missionPrefab;
    [SerializeField] private Button openButton;
    [SerializeField] private Button closeButton;

    private List<UIMissionInfo> missionInfos = new List<UIMissionInfo>();
    private InputController inputController;
    private MissionController missionController;

    #region Constructs
    public void Construct(InputController inputController) => this.inputController = inputController;
    public void Construct(MissionController missionController) => this.missionController = missionController;
    #endregion

    private void Start()
    {
        ClosePanel();
        CreateAllCurrentMission();

        openButton.onClick.AddListener(OpenPanel);
        closeButton.onClick.AddListener(ClosePanel);

        missionController.OnMissionAdd.AddListener(MissionAdd);
        missionController.OnMissionUpdate.AddListener(MissionUpdate);
    }

    private void OnDestroy()
    {
        openButton.onClick.RemoveListener(OpenPanel);
        closeButton.onClick.RemoveListener(ClosePanel);

        missionController.OnMissionAdd.RemoveListener(MissionAdd);
        missionController.OnMissionUpdate.RemoveListener(MissionUpdate);
    }

    private void OpenPanel()
    {
        inputController.SetInputControllerMode(InputControllerModes.NotepadMode);
        missionPanel.SetActive(true);
    }

    private void ClosePanel()
    {
        inputController.SetInputControllerMode(InputControllerModes.CityMode);
        missionPanel.SetActive(false);
    }

    private void CreateAllCurrentMission()
    {
        List<Mission> missions = missionController.GetAllCurrentMissions;

        foreach (Mission mission in missions)
        {
            MissionAdd(mission);
        }
    }

    private void MissionAdd(Mission mission)
    {
        UIMissionInfo uiMissionInfo = Instantiate(missionPrefab, missionGroup);
        uiMissionInfo.SetProperties(mission.missionInfo.Id, mission.missionInfo.Text, mission.value, mission.missionInfo.Value, mission.isFinish, this);

        missionInfos.Add(uiMissionInfo);
    }

    private void MissionUpdate(Mission mission)
    {
        foreach(var missionInfo in missionInfos)
        {
            if (missionInfo.Id == mission.missionInfo.Id)
            {
                missionInfo.UpdateTexts(mission.missionInfo.Value, mission.value, mission.isFinish);

                return;
            }
        }
    }

    public void OnMissionFinishButtonClick(int id)
    {
        missionController.OnMissionFinishClick(id);
    }
}
