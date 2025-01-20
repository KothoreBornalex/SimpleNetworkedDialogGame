using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(menuName = "Dialogue/New Dialogue")]
[System.Serializable]
public class DialogueContainerSO : ScriptableObject
{
    public List<NodeLinkData> nodeLinkDatas = new List<NodeLinkData>();

    public List<DialogueNodeData> dialogueNodeDatas = new List<DialogueNodeData>();
    public List<EndNodeData> endNodeDatas = new List<EndNodeData>();
    public List<StartNodeData> startNodeDatas = new List<StartNodeData>();
    public List<EventNodeData> eventNodeDatas = new List<EventNodeData>();

    public List<BaseNodeData> AllNodes
    {
        get
        {
            List<BaseNodeData> tmp = new List<BaseNodeData>();
            tmp.AddRange(dialogueNodeDatas);
            tmp.AddRange(endNodeDatas);
            tmp.AddRange(startNodeDatas);
            tmp.AddRange(eventNodeDatas);
            return tmp;
        }
    }
}

#region data

[System.Serializable]
public class NodeLinkData
{
    public string baseNodeGuid;
    public string targetNodeGuid;

}
[System.Serializable]
public class BaseNodeData
{
    public string nodeguid;
    public Vector2 position;
}


[System.Serializable]
public class DialogueNodeData : BaseNodeData
{
    public List<DialogueNodePort> dialogueNodePorts;
    public Sprite sprite;
    public DialogueSpriteType dialoguefaceimagetype;
    // public AudioClip audioclips; Implementing this later for now i want my code to work
    public string name;
    public string key;
    //public List<LanguageGeneric<string>> textType;

}

[System.Serializable]
public class EndNodeData : BaseNodeData 
{
    public EndNodeTypes endNodeTypes;
}

[System.Serializable]
public class StartNodeData :BaseNodeData
{
}

[System.Serializable]
public class EventNodeData : BaseNodeData
{
    public DialogueEventSO DialogueEventSo;
}

#endregion data
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
    public string Key;
}