using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientHandle : MonoBehaviour
{
    public static void Welcome(Packet _packet)
    {
        string msg = _packet.ReadString();
        int _myId = _packet.ReadInt();

        Debug.Log($"Message from the server: {msg}");
        Client.instance.myId = _myId;

        ClientSend.WelcomeReceived();
    }

    public static void UpdateServerDataOnClients(Packet _packet)
    {
        string _serverName = _packet.ReadString();
        int _currentPlayers = _packet.ReadInt();

        Debug.Log($"The Server Name Is: {_serverName}");
        Debug.Log($"There is currently {_currentPlayers} connected on the Server");

        UIManager.instance.serverNameText.text = _serverName;
        UIManager.instance.playersCountText.text = _currentPlayers.ToString() + " " + "Players";


    }


    public static void ResultReceived(Packet _packet)
    {
        int _result = _packet.ReadInt();

        Debug.Log($"Result Is: {_result}");

        GameResult Result = (GameResult)_result;

        switch (Result)
        {
            case GameResult.Equality:
                UIManager.instance.gameStatus.text = "Equality";
                break;
            case GameResult.Victory:
                UIManager.instance.gameStatus.text = "Victory";
                break;
            case GameResult.Defeat:
                UIManager.instance.gameStatus.text = "Defeat";
                break;
        }

        //UIManager.instance.serverNameText.text = _serverName;
        //UIManager.instance.playersCountText.text = _currentPlayers.ToString() + " " + "Players";

    }



    public static void RestartDoneReceived(Packet _packet)
    {

        UIManager.instance.gameStatus.text = "Waiting for others";
        //UIManager.instance.serverNameText.text = _serverName;
        //UIManager.instance.playersCountText.text = _currentPlayers.ToString() + " " + "Players";

    }
}
