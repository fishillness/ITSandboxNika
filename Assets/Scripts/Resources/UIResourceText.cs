using TMPro;
using UnityEngine;

public class UIResourceText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI valueText;   

    public void UpdateText(int value)
    {
        valueText.text = $"{value}";
    }
}
