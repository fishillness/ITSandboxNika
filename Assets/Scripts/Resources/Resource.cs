using UnityEngine;
using UnityEngine.Events;

public class Resource : MonoBehaviour
{
    public int CurrentValue => currentValue;        
    public event UnityAction OnResourceValueUpdate;

    [SerializeField] private int maxValue;

    private int currentValue;
    private void Awake()
    {
        currentValue = maxValue;
    }

    public void AddValue(int value)
    {
        currentValue += value;

        if (currentValue >= maxValue)
            currentValue = maxValue;

        OnResourceValueUpdate?.Invoke();
    }

    public bool DeleteValue(int value)
    {
        if (currentValue - value < 0) return false;

        currentValue -= value;
        OnResourceValueUpdate?.Invoke();
        return true;
    }
}