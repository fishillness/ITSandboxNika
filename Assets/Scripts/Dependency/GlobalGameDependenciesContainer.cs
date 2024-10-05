using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalGameDependenciesContainer : Dependency
{
    private static GlobalGameDependenciesContainer instance;

    [SerializeField] private Match3LevelManager levelManager;
    [SerializeField] private ValueManager valueManager;

    public static GlobalGameDependenciesContainer Instance => instance;

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
        Bind<Match3LevelManager>(levelManager, monoBehaviourInScene);
        Bind<ValueManager>(valueManager, monoBehaviourInScene);
    }

    public void Rebind(MonoBehaviour monoBehaviour)
    {
        BindAll(monoBehaviour);
    }
}
