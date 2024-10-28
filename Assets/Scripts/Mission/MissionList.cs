using UnityEngine;

[CreateAssetMenu]
public class MissionList : ScriptableObject
{
    [SerializeField] private MissionInfo[] missionInfos;

    public MissionInfo GetMissionInfoById(int id)
    {
        for (int i = 0; i < missionInfos.Length; i++)
        {
            if (missionInfos[i].Id == id)
                return missionInfos[i];
        }

        return null;
    }
}
