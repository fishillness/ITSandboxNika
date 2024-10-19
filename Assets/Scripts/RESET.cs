using UnityEngine;

public class RESET : MonoBehaviour
{
    public void RESETDATA()
    {
        FileHandler.Reset("Value");
        FileHandler.Reset("PlacedBuildings");
        FileHandler.Reset("Level");
        FileHandler.Reset("EnergyTime12");
        FileHandler.Reset("CurrentMissions");
        FileHandler.Reset("Plot");
    }
}
