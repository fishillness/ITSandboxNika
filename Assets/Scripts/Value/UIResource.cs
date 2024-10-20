using TMPro;
using UnityEngine;

public class UIResource : MonoBehaviour,
    IDependency<ValueManager>
{
    [SerializeField] private TMP_Text m_UIResourceText;
    [SerializeField] private TMP_Text m_UIEnergyTime;
    [SerializeField] private ValueType m_ValueType;

    private ValueManager m_ResourceManager;

    #region Constructs
    public void Construct(ValueManager m_ResourceManager)
    {
        this.m_ResourceManager = m_ResourceManager;
        Subscribe();
    }
    #endregion

    private void Start()
    {
        if (m_ResourceManager == null)
            GlobalGameDependenciesContainer.Instance.Rebind(this);
    }

    private void OnDestroy()
    {
        if (m_ResourceManager != null)
        {
            m_ResourceManager.GetEventOnValueChangeByType(m_ValueType).RemoveListener(UpdateUIValues);
            m_ResourceManager.Energy.OnTimeTextUpdated.RemoveListener(UpdateUITimes);
        }
    }

    private void Subscribe()
    {
        m_ResourceManager.GetEventOnValueChangeByType(m_ValueType).AddListener(UpdateUIValues);
        UpdateUIValues(m_ResourceManager.GetValueByType(m_ValueType));
        m_ResourceManager.Energy.OnTimeTextUpdated.AddListener(UpdateUITimes);
    }

    public void UpdateUIValues(int value)
    {
        if (m_UIResourceText != null)
            m_UIResourceText.text = value.ToString();
    }

    public void UpdateUITimes(string time)
    {
        if (m_UIEnergyTime != null)
            m_UIEnergyTime.text = time;
    }
}
