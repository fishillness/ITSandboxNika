using UnityEngine;
using UnityEngine.UI;

public class UIShelterCharacteristic : MonoBehaviour
{
    [SerializeField] private Slider m_UIShelterCharacteristicSlider;

    public void SetMaxValue(int maxValue)
    {
        m_UIShelterCharacteristicSlider.maxValue = maxValue;
    }

    public  void UpdateUIValues(int value)
    {
        m_UIShelterCharacteristicSlider.value = value;
    }
}
