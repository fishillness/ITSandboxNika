using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[CustomPropertyDrawer(typeof(DialogAction))]
public class DialogActionEditor : PropertyDrawer
{
    
    private SerializedProperty dialogActionTypeProp;
    private SerializedProperty CharactersNamesProp;
    private SerializedProperty ATalkingCharacterNameProp;
    private SerializedProperty SentenceProp;

    

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        
        property.isExpanded = EditorGUI.Foldout(position, property.isExpanded, "Action", true);

        if (property.isExpanded)
        {
            dialogActionTypeProp = property.FindPropertyRelative("DialogActionType");
            CharactersNamesProp = property.FindPropertyRelative("CharactersNames");
            ATalkingCharacterNameProp = property.FindPropertyRelative("ATalkingCharacterName");
            SentenceProp = property.FindPropertyRelative("Sentence");


            EditorGUILayout.PropertyField(dialogActionTypeProp);
            DialogActionType type = (DialogActionType)dialogActionTypeProp.enumValueIndex;

            if (type == DialogActionType.CharacterSays)
            {
                EditorGUILayout.PropertyField(ATalkingCharacterNameProp);
                EditorGUILayout.PropertyField(SentenceProp);
            }

            if (type == DialogActionType.DeactivateCharacters || type == DialogActionType.ActivateCharacters)
            {
                EditorGUILayout.PropertyField(CharactersNamesProp);
            }
        }
    }
}


