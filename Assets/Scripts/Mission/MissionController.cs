using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static PlacedBuildings;

public class MissionController : MonoBehaviour, 
    IDependency<ValueManager>, IDependency<Match3LevelManager>
{
    [HideInInspector]
    public UnityEvent<Mission> OnMissionAdd;
    [HideInInspector]
    public UnityEvent<Mission> OnMissionUpdate;

    public const string Filename = "CurrentMissions";

    [Serializable]
    public class MissionData
    {
        public int id;
        public int value;
        public bool isFinish;

        public MissionData(int id, int value, bool isFinish)
        {
            this.id = id;
            this.value = value;
            this.isFinish = isFinish;
        }
    }

    [SerializeField] private MissionList missionList;

    private ValueManager valueManager;
    private Match3LevelManager levelManager;

    private List<Mission> currentMissions = new List<Mission>();
    private List<MissionData> currentMissionData = new List<MissionData>();


    [Header("Debug")]
    [SerializeField] private MissionInfo mission1;
    [SerializeField] private bool add1;
    [SerializeField] private MissionInfo mission2;
    [SerializeField] private bool add2;

    #region Constructs
    public void Construct(ValueManager valueManager) => this.valueManager = valueManager;
    public void Construct(Match3LevelManager levelManager) => this.levelManager = levelManager;
    #endregion

    public List<Mission> GetAllCurrentMissions => currentMissions;

    private void Start()
    {
        LoadData();
        levelManager.OnLevelUp.AddListener(OnLevelUp);
    }

    private void Update()
    {
        if (add1)
        {
            add1 = false;
            AddNewMission(mission1);
        }
        if (add2)
        {
            add2 = false;
            AddNewMission(mission2);
        }
    }

    private void OnDestroy()
    {
        levelManager.OnLevelUp.RemoveListener(OnLevelUp);
    }

    public void AddNewMission(MissionInfo missionInfo)
    {
        foreach(MissionData data in currentMissionData)
        {
            if (data.id == missionInfo.Id)
            {
                Debug.Log("Миссия уже есть в списке");
                return;
            }
        }

        Mission mission = new Mission(missionInfo, 0, false);
        MissionData missionData = new MissionData(missionInfo.Id, 0, false);

        currentMissions.Add(mission);
        currentMissionData.Add(missionData);

        OnMissionAdd?.Invoke(mission);

        Saver<List<MissionData>>.Save(Filename, currentMissionData);
    }

    public void OnMissionFinishClick(int id)
    {
        foreach (Mission mission in currentMissions)
        {
            if (mission.missionInfo.Id == id)
            { 
                if (mission.isFinish == false)
                {
                    Debug.Log("Миссия не была готова к завершению");
                    return;
                }

                currentMissions.Remove(mission);
                break;
            }
        }
        foreach (MissionData mission in currentMissionData)
        {
            if (mission.id == id)
            {
                currentMissionData.Remove(mission);
                break;
            }
        }

        Saver<List<MissionData>>.Save(Filename, currentMissionData);
    }

    private void LoadData()
    {
        List<MissionData> uploadedMissionData = new List<MissionData>();

        if (Saver<List<MissionData>>.TryLoad(Filename, ref uploadedMissionData) == true)
        {
            foreach (MissionData data in uploadedMissionData)
            {
                currentMissionData.Add(data);
                Mission mission = new Mission(missionList.GetMissionInfoById(data.id), data.value, data.isFinish);
                currentMissions.Add(mission);
            }
        }
        else
        {
            Debug.Log("Сохранений нет");
        }
    }

    private void OnLevelUp(int level)
    {
        foreach(Mission mission in currentMissions)
        {
            if (mission.missionInfo.Type == MissionType.CompleteMatch3)
            {
                mission.value = 1;
                mission.isFinish = true;
                ChangeMissionDataById(mission.missionInfo.Id, mission.value, mission.isFinish);

                OnMissionUpdate?.Invoke(mission);
            }
        }
    }

    private void ChangeMissionDataById(int id, int value, bool isFinish)
    {
        foreach(MissionData mission in currentMissionData)
        {
            if (mission.id == id)
            {
                mission.value = value;
                mission.isFinish = isFinish;
            }
        }

        Saver<List<MissionData>>.Save(Filename, currentMissionData);
    }
}
