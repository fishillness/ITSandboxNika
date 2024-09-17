using UnityEngine;

public class Resource : MonoBehaviour
{
    public int CurrentValue => currentValue;        

    [SerializeField] private UIResourceText m_UIResourceText;
    [SerializeField] private int maxValue;

    private int currentValue;
    

    public void AddValue(int value)
    {
        currentValue += value;

        if (currentValue >= maxValue)
            currentValue = maxValue;

        m_UIResourceText.UpdateText(currentValue);
    }

    public void DeleteValue(int value)
    {
        if (currentValue - value < 0) return;

        currentValue -= value;
        m_UIResourceText.UpdateText(currentValue);
    }

    public void MaxValue()
    {
        SetCurrentValue(maxValue);
    }

    public void SetCurrentValue(int value)
    {
        currentValue = value;
        m_UIResourceText.UpdateText(currentValue);
    }
}