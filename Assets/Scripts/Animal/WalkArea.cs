using UnityEngine;
using UnityEngine.AI;

public class WalkArea : MonoBehaviour
{
    [SerializeField] private Vector3 m_Size;
    public Vector3 GetRandomPoint()
    {
        int index = 100;
        while (true)
        {            
            NavMeshHit navMeshHit;
            if (NavMesh.SamplePosition(transform.TransformPoint(new Vector3(Random.Range(-m_Size.x / 2, m_Size.x / 2), 0, Random.Range(-m_Size.z / 2, m_Size.z / 2))), out navMeshHit, 1.0f, NavMesh.AllAreas) == true)
            {
                return navMeshHit.position;
            }
            index--;
            if (index == 0)
            {
                return Vector3.zero;
            }
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawWireCube(Vector3.zero, m_Size);
    }
#endif
}
