using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Animal : MonoBehaviour
{
    public event UnityAction<Animal> TheGoalHasBeenAchievedEvent;

    [SerializeField] private AnimalAI m_AnimalAI;

    private void Start()
    {
        m_AnimalAI.TheGoalHasBeenAchievedEvent += TheGoalHasBeenAchieved;
    }
    public void ReturningHome(List<Building> buildingsWithAnExit)
    {
        m_AnimalAI.ReturningHome(buildingsWithAnExit);
    }

    private void TheGoalHasBeenAchieved()
    {
        TheGoalHasBeenAchievedEvent?.Invoke(this);
    }

    public void SetWalkArea(WalkArea walkArea)
    {
        m_AnimalAI.SetWalkArea(walkArea);
    }

    public void Walk()
    {
        m_AnimalAI.Walk();
    }
}
