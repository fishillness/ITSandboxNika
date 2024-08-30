using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
        Text.text = $"Монеты: {coins}";
    }
}
