using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoinText : MonoBehaviour
{
    public Text Text;

    private void Awake()
    {
        SaveMoney.SendCoinText.AddListener(coins =>
        {
            Text.text = $"Монеты: {coins}";
        });
    }
}
