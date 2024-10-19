using System;
using UnityEngine;
using UnityEngine.UI;

public class DialogCharacter : MonoBehaviour
{
    public string CharacterName => m_CharacterName;

    [SerializeField] private string m_CharacterName;
    [SerializeField] private Image m_CharacterImage;
    [SerializeField] private RectTransform m_RectTransform;

    private Vector2 defaultSize;

    private void Start()
    {
        defaultSize = m_RectTransform.sizeDelta;
    }

    public void HidingCharacter(Color inactiveCharacterColor, float inactiveCharacterSizeFactor)
    {
        m_CharacterImage.color = inactiveCharacterColor;
        m_RectTransform.sizeDelta *= inactiveCharacterSizeFactor;
    }
    public void CharacterSelection()
    {
        m_CharacterImage.color = Color.white;
        m_RectTransform.sizeDelta = defaultSize;
    }
    public void ActivateCharacters()
    {
        m_CharacterImage.enabled = true;
    }
    public void DeactivateCharacters()
    {
        m_CharacterImage.enabled = false;
    }
}
