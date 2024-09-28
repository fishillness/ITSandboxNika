using TMPro;
using UnityEngine;

public class UIResource : MonoBehaviour
{
    [SerializeField] private TMP_Text m_UIResourceText;

    public void UpdateUIValues(int value)
    {
        m_UIResourceText.text = value.ToString();
    }
}
