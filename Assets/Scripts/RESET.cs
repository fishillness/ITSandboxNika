using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RESET : MonoBehaviour
{
    public void RESETDATA()
    {
        FileHandler.Reset("Store");
        FileHandler.Reset("PlacedBuildings");
    }
}
