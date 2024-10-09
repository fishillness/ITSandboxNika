using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DialogUI : MonoBehaviour
{
    [SerializeField] private Canvas m_DialogCanvas;

    [SerializeField] private TMP_Text m_NameText;
    [SerializeField] private TMP_Text m_SentenceText;
    [SerializeField] private Color m_InactiveCharacterColor;
    [SerializeField][Range(0, 1)] private float m_InactiveCharacterSizeFactor;
    [SerializeField] private float m_SpeedText;

    private Coroutine coroutine;

    public void StartDialogue()
    {
        m_DialogCanvas.enabled = true;
    }
    public void EndDialogue()
    {
        m_DialogCanvas.enabled = false;
    }

    public bool CheckingTheTextForCompliance(string sentence)
    {
        return m_SentenceText.text == sentence;
    }

    public void WriteTheSentenceText(string sentence)
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        m_SentenceText.text = sentence;
    }
    public void WriteTheNameText(string name)
    {
        m_NameText.text = name;
    }
    public void StartSentenceTextOutput(string sentence)
    {
        m_SentenceText.text = string.Empty;
        coroutine = StartCoroutine(SentenceTextOutput(sentence));
    }

    public void HidingCharacter(DialogCharacter dialogCharacter)
    {
        dialogCharacter.HidingCharacter(m_InactiveCharacterColor, m_InactiveCharacterSizeFactor);
    }
    public void CharacterSelection(DialogCharacter dialogCharacter)
    {
        dialogCharacter.CharacterSelection();
    }

    IEnumerator SentenceTextOutput(string sentence)
    {
        foreach (char c in sentence.ToCharArray())
        {
            m_SentenceText.text += c;
            yield return new WaitForSeconds(m_SpeedText);
        }
    }
}
