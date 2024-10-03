using UnityEngine;

public class GlobalSceneDependenciesContainer : Dependency
{
    [SerializeField] private Match3LevelManager levelManager;
    [SerializeField] private ValueManager valueManager;

    private void Awake()
    {
        FindAllObjectToBind();
    }

    protected override void BindAll(MonoBehaviour monoBehaviourInScene)
    {
        Bind<Match3LevelManager>(levelManager, monoBehaviourInScene);
        Bind<ValueManager>(valueManager, monoBehaviourInScene);
    }
}
