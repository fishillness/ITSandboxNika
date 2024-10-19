using System;
using UnityEngine;

[Serializable]
public class Mission 
{
    public MissionInfo missionInfo;
    public int value;
    public bool isFinish;

    public Mission(MissionInfo missionInfo, int value, bool isFinish)
    {
        this.missionInfo = missionInfo;
        this.value = value;
        this.isFinish = isFinish;
    }
}
