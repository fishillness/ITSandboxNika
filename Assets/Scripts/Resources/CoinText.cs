using TMPro;
using UnityEngine;

public class CoinText : MonoBehaviour
{
    public TextMeshProUGUI Text;

    private void Start()
    {
        SaveMoney.SendCoinText.AddListener(UpdateText);
    }

    private void OnDestroy()
    {
        SaveMoney.SendCoinText.RemoveListener(UpdateText);
    }

    void UpdateText(int coins)
    {
        Text.text = $"{coins}";
    }
}
