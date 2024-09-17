using TMPro;
using UnityEngine;

public class CardDisplay : MonoBehaviour
{
    [SerializeField] private CardRandom cardRandom;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI descText;

    void Start()
    {
        cardRandom.OnCardUpdated.AddListener(UpdateTexts);
    }

    private void OnDestroy()
    {
        cardRandom.OnCardUpdated.RemoveListener(UpdateTexts);
    }

    public void UpdateTexts(Card selectCard)
    {
        nameText.text = selectCard.name;
        descText.text = selectCard.description;
    }
}
