using UnityEngine;

public class Value : MonoBehaviour
{
    public int CurrentValue => currentValue;        
        
    [SerializeField] protected int m_MaxValue;

    protected int currentValue;    

    public virtual void AddValue(int value)
    {
        currentValue += value;

        if (currentValue >= m_MaxValue)
            currentValue = m_MaxValue;

    }

    public virtual void DeleteValue(int value)
    {
        if (currentValue - value < 0) return;

        currentValue -= value;
    }    

    public virtual void SetCurrentValue(int value)
    {
        currentValue = value;
    }
}