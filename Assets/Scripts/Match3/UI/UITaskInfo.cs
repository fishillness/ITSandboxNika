using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UITaskInfo : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI textNumber;

    public void SetProperties(Sprite sprite, int number)
    {
        image.sprite = sprite;
        textNumber.text = number.ToString();
    }

    public void UpdateNumberText(int number)
    {
        textNumber.text = number.ToString();
    }
}
