using UnityEngine;

[CreateAssetMenu]
public class LevelList : ScriptableObject
{
    [SerializeField] private LevelInfo[] levels;

    public LevelInfo[] Levels => levels;
}
