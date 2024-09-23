using UnityEngine;

public class RESET : MonoBehaviour
{
    public void RESETDATA()
    {
        FileHandler.Reset("Value");
        FileHandler.Reset("PlacedBuildings");
    }
}
