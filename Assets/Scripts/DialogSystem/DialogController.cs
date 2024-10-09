using System.Collections;
using TMPro;
using UnityEngine;

public class DialogController : MonoBehaviour
{    
    [SerializeField] private InputController m_InputController;
    [SerializeField] private DialogCharacter[] m_DialogCharacters;
    [SerializeField] private DialogUI m_DialogUI;

    private Dialog dialog;
    private DialogCharacter currentCharacter;
    private DialogAction dialogAction;
    
    private void Start()
    {
        m_InputController.ClickEventInDialogMode += NextSentence;
    }
    private void OnDestroy()
    {
        m_InputController.ClickEventInDialogMode -= NextSentence;
    }

    public void StartDialogue(Dialog dialog)
    {
        m_InputController.SetInputControllerMode(InputControllerModes.DialogMode);
        m_DialogUI.StartDialogue();
        DialogInitializing(dialog);
        PerformingDialogAction();
    }

    private void DialogInitializing(Dialog dialog)
    {
        this.dialog = dialog;
        this.dialog.DialogReset();
        currentCharacter = null;
        dialogAction = null;
    }
    private void EndDialogue()
    {
        m_InputController.SetInputControllerMode(InputControllerModes.CityMode);
        m_DialogUI.EndDialogue();
    }

    private void NextSentence()
    {
        if (m_DialogUI.CheckingTheTextForCompliance(dialogAction.Sentence))
        {
            PerformingDialogAction();
        }
        else
        {
            m_DialogUI.WriteTheSentenceText(dialogAction.Sentence);
        }
        
    }

    private void PerformingDialogAction()
    {
        if (dialog == null) return;

        dialogAction = dialog.GetNextDialogAction();

        if (dialogAction == null)
        {
            EndDialogue();
            return;
        }

        DialogCharacter dialogCharacter;

        if (dialogAction.DialogActionType == DialogActionType.CharacterSays)
        {
            dialogCharacter = GetCharacterOnName(dialogAction.ATalkingCharacterName);

            if (dialogCharacter == null) return;

            if (dialogCharacter != currentCharacter)
            {
                if (currentCharacter != null)
                {
                    m_DialogUI.HidingCharacter(currentCharacter);
                }
                currentCharacter = dialogCharacter;
                m_DialogUI.CharacterSelection(currentCharacter);
            }
            m_DialogUI.StartSentenceTextOutput(dialogAction.Sentence);
            m_DialogUI.WriteTheNameText(currentCharacter.CharacterName);
        }

        else if (dialogAction.DialogActionType == DialogActionType.ActivateCharacters)
        {
            for (int i = 0; i < dialogAction.CharactersNames.Length; i++)
            {
                dialogCharacter = GetCharacterOnName(dialogAction.CharactersNames[i]);
                if (dialogCharacter == null) return;
                m_DialogUI.HidingCharacter(dialogCharacter);
                dialogCharacter.ActivateCharacters();
            }
            PerformingDialogAction();
        }

        else if (dialogAction.DialogActionType == DialogActionType.DeactivateCharacters)
        {
            for (int i = 0; i < dialogAction.CharactersNames.Length; i++)
            {
                dialogCharacter = GetCharacterOnName(dialogAction.CharactersNames[i]);
                if (dialogCharacter == null) return;
                dialogCharacter.DeactivateCharacters();
            }
            PerformingDialogAction();
        }
    }

    


    private DialogCharacter GetCharacterOnName(string name)
    {
        for (int i = 0; i < m_DialogCharacters.Length; i++)
        {
            if (m_DialogCharacters[i].CharacterName == name)
            {
                return m_DialogCharacters[i];
            }
        }
        return null;
    }
}
