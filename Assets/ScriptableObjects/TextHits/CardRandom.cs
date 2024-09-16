using System.Collections.Generic;
using UnityEngine;

public class CardRandom : MonoBehaviour
{
    public List<Card> listHints;
    public Card selectCard;
    public int rangeRandom;
    private int rnd;
    // Start is called before the first frame update
    public void Awake()
    {
        InvokeRepeating("randomNumber", 0, 5);
    }

    public void randomNumber()
    {
        rnd = Random.Range(0, rnd);
        selectCard = listHints[rnd];
    }
}
