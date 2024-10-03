using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalGameDependenciesContainer : Dependency
{
    private static GlobalGameDependenciesContainer instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        FindAllObjectToBind();
    }

    protected override void BindAll(MonoBehaviour monoBehaviourInScene)
    {
        //Bind<Match3LevelManager>(property, monoBehaviourInScene);
    }
}
