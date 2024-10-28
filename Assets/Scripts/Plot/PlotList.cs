using UnityEngine;

[CreateAssetMenu]
public class PlotList : ScriptableObject
{
    [SerializeField] private PlotAction[] plotActions;

    public PlotAction[] PlotActions => plotActions;
}
