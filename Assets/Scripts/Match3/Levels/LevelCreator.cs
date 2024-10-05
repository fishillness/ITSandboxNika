using UnityEngine;

public class LevelCreator : MonoBehaviour,
    IDependency<Match3LevelManager>
{
    private Match3LevelManager levelManager;

    #region Constructs
    public void Construct(Match3LevelManager levelManager)
    {
        this.levelManager = levelManager;
    } 
    #endregion

    private void Start()
    {
        Instantiate(levelManager.LevelList.Levels[levelManager.CurrentLevel].LevelPrefab);
    }
}
