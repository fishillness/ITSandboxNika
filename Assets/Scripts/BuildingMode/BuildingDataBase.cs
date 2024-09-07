using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BuildingDataBase : ScriptableObject
{
    [SerializeField] private List<Building> m_Building = new List<Building>();

    public Building GetBuilding(int ID)
    {
        for (int i = 0; i < m_Building.Count; i++)
        {
            if (m_Building[i].BuildingID == ID)
            {
                return m_Building[i];
            }
            else
            {
                continue;
            }
        }
        return null; 
    }
}
