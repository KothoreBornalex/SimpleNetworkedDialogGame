using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTalkZone : MonoBehaviour
{
    //[SerializeField] private GameObject speechBubble;
    [SerializeField] private KeyCode talkKey = KeyCode.E;
    //[SerializeField] private Text keyInputText;

    private DialogueTalk DialogueTalk;

    private void Awake()
    {
        //speechBubble.SetActive(true);
        //keyInputText.text = talkKey.ToString();

        DialogueTalk = GetComponent<DialogueTalk>();
    }

    void Update()
    {
        if (Input.GetKeyDown(talkKey) /*&& speechBubble.activeSelf */ && DialogueTalk != null)
        {
            DialogueTalk.StartDialogue();
        }
    }
}