using UnityEngine;

public class ShelterCharacteristic : Value
{
    [SerializeField] private UIShelterCharacteristic m_UIShelterCharacteristic;
    

    public override void AddValue(int value)
    {
        base.AddValue(value);
        m_UIShelterCharacteristic.UpdateUIValues(currentValue);
    }

    public override void DeleteValue(int value)
    {
        base.DeleteValue(value);
        m_UIShelterCharacteristic.UpdateUIValues(currentValue);
    }
    public override void SetCurrentValue(int value)
    {        
        base.SetCurrentValue(value);
        m_UIShelterCharacteristic.UpdateUIValues(currentValue);
    }

    public void SetMaxValue()
    {
        m_UIShelterCharacteristic.SetMaxValue(m_MaxValue);
    }
}
