using System;
using TMPro;
using UnityEngine;

public class UIResource : MonoBehaviour
{
    [SerializeField] private TMP_Text m_UIResourceText;
    [SerializeField] private TMP_Text m_UIEnergyTime;

    public void UpdateUIValues(int value)
    {
        m_UIResourceText.text = value.ToString();
    }

    public void UpdateUITimes(string time)
    {
        m_UIEnergyTime.text = time;
    }
}
