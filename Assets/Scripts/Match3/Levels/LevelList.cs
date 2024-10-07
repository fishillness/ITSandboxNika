using UnityEngine;

[CreateAssetMenu]
public class LevelList : ScriptableObject
{
    [SerializeField] private LevelInfo[] levels;
    [SerializeField] private bool replayable;

    public LevelInfo[] Levels => levels;
    public bool Replayable => replayable;
}
