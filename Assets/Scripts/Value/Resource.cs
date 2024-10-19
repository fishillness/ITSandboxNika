using UnityEngine;
using UnityEngine.Events;

public class Resource : Value
{
    [HideInInspector]
    public UnityEvent<string> OnTimeTextUpdated;

    [SerializeField] private int m_StartValue;
    //[SerializeField] private UIResource m_UIResource;    

    public override void AddValue(int value)
    {
        base.AddValue(value);

        //if (m_UIResource != null )
        //    m_UIResource.UpdateUIValues(currentValue);
    }

    public override void DeleteValue(int value)
    {
        base.DeleteValue(value);

        //if (m_UIResource != null)
        //    m_UIResource.UpdateUIValues(currentValue);
    }
    public override void SetCurrentValue(int value)
    {
        base.SetCurrentValue(value);

        //if (m_UIResource != null)
        //    m_UIResource.UpdateUIValues(currentValue);
    }

    public void StartValue()
    {
        SetCurrentValue(m_StartValue);
    }

    public void UpdateTime(string time)
    {
        //m_UIResource.UpdateUITimes(time);
        OnTimeTextUpdated?.Invoke(time);
    }

    public override int SetMaxValueResource()
    {
        return base.SetMaxValueResource();
    }

    public override int SetCur()
    {
        return base.SetCur();
    }
}
