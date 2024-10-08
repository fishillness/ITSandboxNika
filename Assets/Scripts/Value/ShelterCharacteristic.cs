using UnityEngine;

public class ShelterCharacteristic : Value
{
    //[SerializeField] private UIShelterCharacteristic m_UIShelterCharacteristic;
    
    public override void AddValue(int value)
    {
        base.AddValue(value);

        //if (m_UIShelterCharacteristic != null)
        //    m_UIShelterCharacteristic.UpdateUIValues(currentValue);
    }

    public override void DeleteValue(int value)
    {
        base.DeleteValue(value);

        //if (m_UIShelterCharacteristic != null)
        //    m_UIShelterCharacteristic.UpdateUIValues(currentValue);
    }
    public override void SetCurrentValue(int value)
    {        
        base.SetCurrentValue(value);

        //if (m_UIShelterCharacteristic != null)
        //    m_UIShelterCharacteristic.UpdateUIValues(currentValue);
    }

    public void SetMaxValue()
    {
        SetCurrentValue(m_MaxValue);

        //if (m_UIShelterCharacteristic != null)
        //    m_UIShelterCharacteristic.SetMaxValue(m_MaxValue);
    }
}
