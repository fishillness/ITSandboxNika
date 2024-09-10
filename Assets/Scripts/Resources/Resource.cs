using UnityEngine;
using UnityEngine.Events;

public class Resource : MonoBehaviour
{
    [HideInInspector]
    public UnityEvent<int> OnResourceValueUpdate = new UnityEvent<int>();

    [SerializeField] private ResourceType resourceType;
    [SerializeField] private int maxValue;

    private int currentValue;

    public int CurrentValue;

    private void Start()
    {
        currentValue = 0;
    }

    public void AddCoin(int value)
    {
        currentValue += value;

        if (currentValue >= maxValue)
            currentValue = maxValue;

        OnResourceValueUpdate?.Invoke(currentValue);
    }

    public bool DeleteCoin(int value)
    {
        if (currentValue - value < 0) return false;

        currentValue -= value;
        OnResourceValueUpdate?.Invoke(currentValue);
        return true;
    }
}