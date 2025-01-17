using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ClientSend : MonoBehaviour
{
    private static void SendTCPData(Packet _packet)
    {
        //thise WriteLength function put the length int at the start of the bytes array whic is important to read the data
        _packet.WriteLength();
        Client.instance.tcp.SendData(_packet);
    }

    #region packets

    public static void WelcomeReceived()
    {
        using (Packet _packet = new Packet((int)ClientPackets.welcomeReceived))
        {
            _packet.Write(Client.instance.myId);
            _packet.Write(UIManager.instance.userNameField.text);

            SendTCPData(_packet);
        }
    }

    public static void Play(int _choiceIndex)
    {
        using (Packet _packet = new Packet((int)ClientPackets.play))
        {
            _packet.Write(UIManager.instance.userNameField.text);
            _packet.Write(_choiceIndex);

            SendTCPData(_packet);
        }
    }


    public static void RestartGameOnServer()
    {
        using (Packet _packet = new Packet((int)ClientPackets.restart))
        {

            SendTCPData(_packet);
        }
    }
    #endregion
}
