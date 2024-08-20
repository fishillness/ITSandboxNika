using System.Collections.Generic;

public class PlacedBuildings
{
    private List<int> buildings = new List<int>();

    public int AddBuilding(int buildingID)
    {
        buildings.Add(buildingID);
        return buildings.Count - 1;
    }
}
