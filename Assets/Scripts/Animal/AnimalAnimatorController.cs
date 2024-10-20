using UnityEngine;

public class AnimalAnimatorController : MonoBehaviour
{
    private AnimalAI animalAI;
    private Animator animator;
    private bool isRest = false;

    private string LieTrigger = "Lie";
    private string SitTrigger = "Sit";
    private string StopRestTrigger = "StopRest";

    private void Awake()
    {
        animalAI = GetComponentInParent<AnimalAI>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        CheckAnimalMignitud();
    }

    // быстрое решение проблемы. Времени сделать лучше не хватает. После сдачи проекта нужно переделать
    private void CheckAnimalMignitud()
    {
        if (animalAI.AgentVelocityMagnitude == 0 && isRest == false)
            Rest();

        if (animalAI.AgentVelocityMagnitude > 0 && isRest == true)
            StopRest();
    }

    private void Rest()
    {
        int random = Random.Range(0, 2);

        if (random == 0)
            animator.SetTrigger(LieTrigger);
        else if (random == 1)
            animator.SetTrigger(SitTrigger);

        isRest = true;    
    }

    private void StopRest()
    {
        animator.SetTrigger(StopRestTrigger);
        isRest = false; 
    }
}
