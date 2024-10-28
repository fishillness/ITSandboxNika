using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class AnimalAI : MonoBehaviour
{
    public event UnityAction TheGoalHasBeenAchievedEvent;

    public float AgentVelocityMagnitude => m_Agent.velocity.magnitude;

    [SerializeField] private float m_MinWaitingTime;
    [SerializeField] private float m_MaxWaitingTime;
    [SerializeField] private int m_WalkingRadius;
    [SerializeField] private int m_MinWalkingRadius;
    [SerializeField] private NavMeshAgent m_Agent;

    private Coroutine coroutine;
    private WalkArea walkArea;

    private enum BehaviorYype
    {
        Walk,
        MovingTowardsTheGoal
    }

    private BehaviorYype behaviorType = BehaviorYype.Walk;
    private bool agentReachedDestination;

    private void Start()
    {
        SetNewPath(walkArea.GetRandomPoint());
    }

    private void Update()
    {
        if (agentReachedDestination == true) return;

        if (AgentReachedDestination() == true)
        {
            agentReachedDestination = true;

            if (behaviorType == BehaviorYype.Walk)
            {
                coroutine = StartCoroutine(Waiting(Random.Range(m_MinWaitingTime, m_MaxWaitingTime)));
            }
            else if (behaviorType == BehaviorYype.MovingTowardsTheGoal)
            {
                TheGoalHasBeenAchievedEvent?.Invoke();
            }
                        
        }
    }

    IEnumerator Waiting(float waitingTime)
    {        
        yield return new WaitForSeconds(waitingTime);
        coroutine = null;
        SetNewPath(walkArea.GetRandomPoint());
    }
    
    private void SetNewPath(Vector3 position)
    {   
        agentReachedDestination = false;
        test = position;
        m_Agent.SetDestination(position);
    }

    
    private bool AgentReachedDestination()
    {
        if (m_Agent.pathPending == false)
        {
            if (m_Agent.remainingDistance <= m_Agent.stoppingDistance)
            {
                if (m_Agent.hasPath == false || m_Agent.velocity.sqrMagnitude == 0.0f)
                {                
                    return true;
                }
                return false;
            }
            return false;
        }
        return false;
    }

    public void ReturningHome(List<Building> buildingsWithAnEntry)
    {            

        float minDistance = Mathf.Infinity;
        Vector3 entryPosition = new Vector3();
        for (int i = 0; i < buildingsWithAnEntry.Count; i++)
        {            
            Transform entryPoint = buildingsWithAnEntry[i].GetEntryPoint();

            float distance = (entryPoint.position - transform.position).magnitude;

            if (distance < minDistance)
            {
                minDistance = distance;
                entryPosition = entryPoint.position;
            }
        }
        MoveTowardsTheGoal(entryPosition);
    }

    private void MoveTowardsTheGoal(Vector3 position)
    {
        Debug.Log("MovingTowardsTheGoal");
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }

        Debug.Log("coroutine == nulll");
        SetNewPath(position);
        behaviorType = BehaviorYype.MovingTowardsTheGoal;
    }

    public void Walk()
    {
        Debug.Log("Walk");
        if (coroutine != null)
        {
            return;
        }

        Debug.Log("coroutine == nulll");
        behaviorType = BehaviorYype.Walk;
        SetNewPath(walkArea.GetRandomPoint());
    }

    public void SetWalkArea(WalkArea walkArea)
    {
        this.walkArea = walkArea;
    }

    private Vector3 test;
#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(test, 1);
    }
#endif
}
