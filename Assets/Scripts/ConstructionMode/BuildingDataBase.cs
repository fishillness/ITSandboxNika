using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BuildingDataBase : ScriptableObject
{
    [SerializeField] private List<BuildingInfo> m_BuildingsInfo = new List<BuildingInfo>();

    public BuildingInfo GetBuildingInfo(int ID)
    {
        for (int i = 0; i < m_BuildingsInfo.Count; i++)
        {
            if (m_BuildingsInfo[i].Building.BuildingID == ID)
            {
                return m_BuildingsInfo[i];
            }
            else
            {
                continue;
            }
        }
        return null;

        
    }

    public Building GetBuilding(int ID)
    {
        BuildingInfo buildingInfo = GetBuildingInfo(ID);

        if (buildingInfo != null)
        {
            return buildingInfo.Building;
        }
        return null;
    }

   
}
