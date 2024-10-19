using System;
using UnityEngine;

public enum DialogActionType
{
    ActivateCharacters,
    DeactivateCharacters,
    CharacterSays
}

[Serializable]
public class DialogAction
{
    public DialogActionType DialogActionType;

    public String[] CharactersNames;

    public String ATalkingCharacterName;
    [TextArea(3, 10)] public string Sentence;
}

[CreateAssetMenu]
public class Dialog : ScriptableObject
{     
    [SerializeField] private DialogAction[] m_DialogActions;

    private int sentenceIndex;

    public DialogAction GetNextDialogAction()
    {
        if (sentenceIndex >= m_DialogActions.Length)
        {
            return null;
        }
        return m_DialogActions[sentenceIndex++];
    }
    public void DialogReset()
    {
        sentenceIndex = 0;
    }
}
