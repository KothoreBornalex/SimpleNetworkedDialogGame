using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class DialogueGraphView : GraphView
{
    private string styleSheetsName = "GraphViewStyleSheet";
    private DialogueEditorWindow dialogueEditorWindow;

    public DialogueGraphView(DialogueEditorWindow _editorWindow) 
    {
        dialogueEditorWindow = _editorWindow;

        StyleSheet tmpStyleSheet = Resources.Load<StyleSheet>(styleSheetsName);
        styleSheets.Add(tmpStyleSheet);

        SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);
        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());
        this.AddManipulator(new FreehandSelector());
        GridBackground grid = new GridBackground();
        Insert(0, grid);

        grid.StretchToParentSize();
    }
    public StartNode CreateStartNode(Vector2 _pos)
    {
        StartNode tmp = new StartNode(_pos, dialogueEditorWindow, this);
        return tmp;
    }

    public DialogueNode CreateDialogueNode(Vector2 _pos)
    {
        DialogueNode tmp = new DialogueNode(_pos, dialogueEditorWindow, this);
        return tmp;
    }

    public EventNode CreateEventNode(Vector2 _pos)
    {
        EventNode tmp = new EventNode(_pos, dialogueEditorWindow, this);
        return tmp;
    }

    public EndNode CreateEndNode(Vector2 _pos)
    {
        EndNode tmp = new EndNode(_pos, dialogueEditorWindow, this);
        return tmp;
    }
}
