using System.Collections.Generic;
using UnityEngine;

public class PlacedBuildings
{
    private List<Building> buildings = new List<Building>();

    public int AddBuilding(Building building)
    {
        buildings.Add(building);
        return buildings.Count - 1;
    }

    public Building GetBuilding(int buildingIndex)
    {
        return buildings[buildingIndex];
    }
}
