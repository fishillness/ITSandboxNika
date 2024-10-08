using UnityEngine;
using UnityEngine.UI;

public class UIShelterCharacteristic : MonoBehaviour,
    IDependency<ValueManager>
{
    [SerializeField] private Slider m_UIShelterCharacteristicSlider;
    [SerializeField] private ValueType m_ValueType;

    private ValueManager m_ShelterCharacteristicManager;

    #region Constructs
    public void Construct(ValueManager m_ShelterCharacteristicManager) => this.m_ShelterCharacteristicManager = m_ShelterCharacteristicManager;
    #endregion

    private void Start()
    {
        m_ShelterCharacteristicManager.GetEventOnValueChangeByType(m_ValueType).AddListener(UpdateUIValues);
        UpdateUIValues(m_ShelterCharacteristicManager.GetValueByType(m_ValueType));
    }

    private void OnDestroy()
    {
        m_ShelterCharacteristicManager.GetEventOnValueChangeByType(m_ValueType).RemoveListener(UpdateUIValues);
    }

    public void SetMaxValue(int maxValue)
    {
        m_UIShelterCharacteristicSlider.maxValue = maxValue;
    }

    public  void UpdateUIValues(int value)
    {
        m_UIShelterCharacteristicSlider.value = value;
    }
}
