using UnityEngine;

public class LevelCreator : MonoBehaviour
{
    [SerializeField] private LevelInfo levelInfo;

    private void Awake()
    {
        Instantiate(levelInfo.LevelPrefab);
    }
}
