using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CardDisplay : MonoBehaviour
{
    //public Card card;
    private CardRandom cardRandom;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descText;
    // Start is called before the first frame update
    void Start()
    {
        nameText.text = cardRandom.selectCard.name;
        descText.text = cardRandom.selectCard.description;
    }

    private void Update()
    {
        //nameText.text = cardRandom.selectCard.name;
        //descText.text = cardRandom.selectCard.description;
    }
}
