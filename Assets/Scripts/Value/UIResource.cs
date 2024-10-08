using TMPro;
using UnityEngine;

public class UIResource : MonoBehaviour,
    IDependency<ValueManager>
{
    [SerializeField] private TMP_Text m_UIResourceText;
    [SerializeField] private ValueType m_ValueType;

    private ValueManager m_ResourceManager;

    #region Constructs
    public void Construct(ValueManager m_ResourceManager) => this.m_ResourceManager = m_ResourceManager;
    #endregion

    private void Start()
    {
        m_ResourceManager.GetEventOnValueChangeByType(m_ValueType).AddListener(UpdateUIValues);
        UpdateUIValues(m_ResourceManager.GetValueByType(m_ValueType));
    }

    private void OnDestroy()
    {
        m_ResourceManager.GetEventOnValueChangeByType(m_ValueType).RemoveListener(UpdateUIValues);
    }

    public void UpdateUIValues(int value)
    {
        m_UIResourceText.text = value.ToString();
    }
}
