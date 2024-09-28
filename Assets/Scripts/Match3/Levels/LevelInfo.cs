using UnityEngine;

[CreateAssetMenu]
public class LevelInfo : ScriptableObject
{
    [SerializeField] private GameObject levelPrefabs;
    [SerializeField] private int costInEnergy;
    [SerializeField] private NumberResourcesByType[] numberResourcesByTypes;

    public GameObject LevelPrefab => levelPrefabs;
    public int CostInEnergy => costInEnergy;
    public NumberResourcesByType[] NumberResourcesByTypes => numberResourcesByTypes;
}
