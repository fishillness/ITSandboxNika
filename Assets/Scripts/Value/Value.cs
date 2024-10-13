using UnityEngine;
using UnityEngine.Events;

public class Value : MonoBehaviour
{
    [HideInInspector]
    public UnityEvent<int> OnValueChange;
    public int CurrentValue => currentValue;
    public int MaxValue => m_MaxValue;
        
    [SerializeField] protected int m_MaxValue;

    protected int currentValue;

    public virtual void AddValue(int value)
    {
        currentValue += value;

        if (currentValue >= m_MaxValue)
            currentValue = m_MaxValue;

        OnValueChange?.Invoke(currentValue);
    }

    public virtual void DeleteValue(int value)
    {  
        currentValue -= value;
        if (currentValue  < 0)
        {
            currentValue = 0;
        }

        OnValueChange?.Invoke(currentValue);
    }    

    public virtual void SetCurrentValue(int value)
    {
        currentValue = value;

        OnValueChange?.Invoke(currentValue);
    }
}