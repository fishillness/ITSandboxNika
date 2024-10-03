using UnityEngine;

public abstract class Dependency : MonoBehaviour
{
    protected void FindAllObjectToBind()
    {
        MonoBehaviour[] allMonoInScene = FindObjectsOfType<MonoBehaviour>();

        for (int i = 0; i < allMonoInScene.Length; i++)
        {
            BindAll(allMonoInScene[i]);
        }
    }

    protected virtual void BindAll(MonoBehaviour monoBehaviourInScene) { }

    protected void Bind<T>(MonoBehaviour bindObject, MonoBehaviour target)
        where T : class
    {
        if (target is IDependency<T>)
            (target as IDependency<T>).Construct(bindObject as T);
    }
}
