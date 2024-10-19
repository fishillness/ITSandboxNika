using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : Value
{
    [SerializeField] private int m_StartValue;
    [SerializeField] private UIResource m_UIResource;    

    public override void AddValue(int value)
    {
        base.AddValue(value);
        m_UIResource.UpdateUIValues(currentValue);
    }

    public override void DeleteValue(int value)
    {
        base.DeleteValue(value);
        m_UIResource.UpdateUIValues(currentValue);
    }
    public override void SetCurrentValue(int value)
    {
        base.SetCurrentValue(value);
        m_UIResource.UpdateUIValues(currentValue);
    }

    public void StartValue()
    {
        SetCurrentValue(m_StartValue);
    }

    public void UpdateTime(string time)
    {
        m_UIResource.UpdateUITimes(time);
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
