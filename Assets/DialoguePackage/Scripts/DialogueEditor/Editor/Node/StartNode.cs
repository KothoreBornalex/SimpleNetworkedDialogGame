using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class StartNode : BaseNode
{
    public StartNode()
    {

    }
    public StartNode(Vector2 position, DialogueEditorWindow _editorWindow, DialogueGraphView _graphView)
    {
        editorWindow = _editorWindow;
        dialogueGraphView = _graphView;

        title = "Start";
        SetPosition(new Rect(position, defaultNodeSize));
        NodeGuid = Guid.NewGuid().ToString();

        AddOutputPort("Output", Port.Capacity.Single);
        //Tells the system we changed the node and to update it.
        RefreshExpandedState();
        RefreshPorts();
    }
}
