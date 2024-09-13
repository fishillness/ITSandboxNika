using System;
using System.Collections.Generic;
using UnityEngine;


public class PlacedBuildings
{
    [Serializable]
    public class BuildingData   
    {
        public Vector2 OccupiedCell;
        public int BuildingID;
        public int BuildingIndex;
    }

    public const string Filename = "PlacedBuildings";

    private List<Building> buildings = new List<Building>();
    private List<BuildingData> buildingsInfo = new List<BuildingData>();
    private int buildingIndex;
    public void AddBuilding(Building building)
    {
        buildings.Add(building);
        AddBuildingInfo(building);

        Saver<List<BuildingData>>.Save(Filename, buildingsInfo);        
    }

    public int GetBuildIndex()
    {
        buildingIndex++;
        return buildingIndex;
    }

    private void AddBuildingInfo(Building building)
    {        
        BuildingData buildingInfo = new BuildingData();
        buildingInfo.OccupiedCell = building.OccupiedCell;
        buildingInfo.BuildingID = building.BuildingID;
        buildingInfo.BuildingIndex = building.BuildingIndex;
        buildingsInfo.Add(buildingInfo);
    }
    public Building GetBuilding(int buildingIndex)
    {
        for (int i = 0; i < buildings.Count; i++)
        {
            if (buildings[i].BuildingIndex == buildingIndex)
            {
                return buildings[i];
            }
        }
        return null;
    }

    public void RemoveBuilding(int buildingIndex)
    {
        for (int i = 0; i < buildings.Count; i++)
        {
            if (buildings[i].BuildingIndex == buildingIndex)
            {
                buildings.Remove(buildings[i]);
            }            
        }

        for (int i = 0; i < buildingsInfo.Count; i++)
        {
            if (buildingsInfo[i].BuildingIndex == buildingIndex)
            {
                buildingsInfo.Remove(buildingsInfo[i]);
            }
        }   

        Saver<List<BuildingData>>.Save(Filename, buildingsInfo);
    }

    public List<BuildingData> Load()
    {
        List<BuildingData> uploadedBuildingsInfo = new List<BuildingData>();
        Saver<List<BuildingData>>.TryLoad(Filename, ref uploadedBuildingsInfo);
        return uploadedBuildingsInfo;
    }
}
