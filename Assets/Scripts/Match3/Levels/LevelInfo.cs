using UnityEngine;

[CreateAssetMenu]
public class LevelInfo : ScriptableObject
{
    [SerializeField] private GameObject levelPrefabs;
    [SerializeField] private int costInEnergy;
    //[SerializeField] private NumberResourcesByType[] numberResourcesByTypes;

    [Header("Resources received")]
    [SerializeField] private int coins;
    [SerializeField] private int boards;
    [SerializeField] private int bricks;
    [SerializeField] private int nails;

    public GameObject LevelPrefab => levelPrefabs;
    public int CostInEnergy => costInEnergy;
    //public NumberResourcesByType[] NumberResourcesByTypes => numberResourcesByTypes;

    public int Coins => coins;
    public int Boards => boards;
    public int Bricks => bricks;
    public int Nails => nails;
}
