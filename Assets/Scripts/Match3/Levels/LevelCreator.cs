using UnityEngine;

public class LevelCreator : MonoBehaviour
{
    [SerializeField] private Match3LevelManager levelManager;

    private void Awake()
    {
        Instantiate(levelManager.LevelList.Levels[levelManager.CurrentLevel].LevelPrefab);
    }
}
