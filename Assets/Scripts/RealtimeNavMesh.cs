using Unity.AI.Navigation;
using UnityEngine;

public class RealtimeNavMesh : MonoBehaviour
{
    [SerializeField] private NavMeshSurface m_NavMeshSurface;

    public void UpdateNavMesh()
    {
        m_NavMeshSurface.BuildNavMesh();
    }
}
