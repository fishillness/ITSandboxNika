using System;
using UnityEngine;

[Serializable]
public class PlotAction : MonoBehaviour
{
    public PlotActionType ActionType;
    
    // If dialog
    public Dialog Dialog;

    // If OpenMission
    public MissionInfo Mission;
}
