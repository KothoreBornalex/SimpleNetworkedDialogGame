using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("User Interfaces")]
    public GameObject startMenu;
    public GameObject gameMenu;

    public InputField gameAttackChoice;
    public InputField userNameField;

    public Text gameStatus;

    [Header("Server Interfaces")]
    public Text serverNameText;
    public Text playersCountText;
    public Text playerNameText;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Instance already Exist!");
            Destroy(this);
        }
    }

    public void ConnectToServer()
    {
        startMenu.SetActive(false);
        userNameField.interactable = false;
        playerNameText.text = userNameField.text;

        Client.instance.ConnectToServer();
    }

    public void PlayRound(int _choice)
    {
        Client.instance.PlayGame((PlayChoice)_choice);
    }

    public void CallRestartGame()
    {
        ClientSend.RestartGameOnServer();
    }

}
