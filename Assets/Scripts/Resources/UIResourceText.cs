using TMPro;
using UnityEngine;

public class UIResourceText : MonoBehaviour
{
    [SerializeField] private Resource resource;
    [SerializeField] private TextMeshProUGUI valueText;

    private void Start()
    {
        resource.OnResourceValueUpdate.AddListener(UpdateText);
    }

    private void OnDestroy()
    {
        resource.OnResourceValueUpdate.RemoveListener(UpdateText);
    }

    void UpdateText(int value)
    {
        valueText.text = $"{value}";
    }
}
