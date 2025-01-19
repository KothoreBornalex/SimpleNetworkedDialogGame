using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(menuName = "Dialogue/New Dialogue")]
[System.Serializable]
public class DialogueContainerSO : ScriptableObject
{

}
[System.Serializable]
public class LanguageGeneric<T>
{
    public LanguageType languageType;
    public T LanguageGenericType;
}
[System.Serializable]
public class DialogueNodePort
{
    public string InputGuid;
    public string OutputGuid;
    public Port MyPort;
    public TextField textField;
    public List<LanguageGeneric<string>> TextLanguage = new List<LanguageGeneric<string>>();
}