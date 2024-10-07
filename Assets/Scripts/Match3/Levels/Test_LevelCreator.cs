using UnityEngine;

public class Test_LevelCreator : MonoBehaviour
{
    [SerializeField] private GameObject levelPrefabs;
    private void Start()
    {
        Instantiate(levelPrefabs);
    }
}
