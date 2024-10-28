using UnityEngine;

[CreateAssetMenu]
public class PlotAction : ScriptableObject
{
    [SerializeField] private PlotActionType actionType;

    [Header("If dialog")]
    [SerializeField] private Dialog dialog;

    [Header("If mission")]
    [SerializeField] private MissionInfo mission;

    public PlotActionType ActionType => actionType;
    public Dialog Dialog => dialog;
    public MissionInfo Mission => mission;
}
