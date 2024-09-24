using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CardRandom : MonoBehaviour
{
    [HideInInspector]
    public UnityEvent<Card> OnCardUpdated;

    [SerializeField] private List<Card> listHints;
    [SerializeField] private float timeToCahngeCard;

    private Card selectCard;
    private int rangeRandom;
    private int rnd => listHints.Count;
    // Start is called before the first frame update
    public void Awake()
    {
        Random.seed = System.DateTime.Now.Millisecond;
        InvokeRepeating("randomNumber", 0, timeToCahngeCard);
    }

    public void randomNumber()
    {
        int random = Random.Range(0, rnd);
        selectCard = listHints[random];
        OnCardUpdated?.Invoke(selectCard);
    }
}
