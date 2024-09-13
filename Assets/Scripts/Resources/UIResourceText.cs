using TMPro;
using UnityEngine;

public class UIResourceText : MonoBehaviour
{
    [SerializeField] private Resource resource;
    [SerializeField] private TextMeshProUGUI valueText;

    private void Start()
    {
        UpdateText();
        resource.OnResourceValueUpdate += UpdateText;
    }

    private void OnDestroy()
    {
        resource.OnResourceValueUpdate -= UpdateText;
    }

    void UpdateText()
    {
        valueText.text = $"{resource.CurrentValue}";
    }
}
