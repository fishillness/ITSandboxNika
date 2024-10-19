using UnityEngine;

[CreateAssetMenu]
public class LevelList : ScriptableObject
{
    [SerializeField] private bool replayable;
    [SerializeField] private LevelInfo[] levels;

    public LevelInfo[] Levels => levels;
    public bool Replayable => replayable;
}
