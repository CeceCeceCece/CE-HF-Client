using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientSend : MonoBehaviour
{
    private static void SendTCPData(Packet _packet)
    {
        _packet.WriteLength();
        Client.instance.tcp.SendData(_packet);
    }

    private static void SendUDPData(Packet _packet)
    {
        _packet.WriteLength();
        Client.instance.udp.SendData(_packet);
    }

    #region Packets
    public static void WelcomeReceived()
    {
        using (Packet _packet = new Packet((int)ClientPackets.welcomeReceived))
        {
            _packet.Write(Client.instance.myId);
            _packet.Write(UIManager.instance.usernameField.text);

            SendTCPData(_packet);
        }
    }

    public static void PlayerMovement(bool[] _inputs)
    {
        using (Packet _packet = new Packet((int)ClientPackets.playerMovement))
        {
            _packet.Write(_inputs.Length);
            foreach (bool _input in _inputs)
            {
                _packet.Write(_input);
            }
            _packet.Write(GameManager.players[Client.instance.myId].transform.rotation);

            SendUDPData(_packet);
        }
    }

    internal static void Spell3()
    {
        using (Packet _packet = new Packet((int)ClientPackets.spell3))
        {
            SendTCPData(_packet);
        }
    }

    public static void Special(Vector3 forward)
    {
        using (Packet _packet = new Packet((int)ClientPackets.specialAttack))
        {
            _packet.Write(forward);
            SendTCPData(_packet);
        }
    }

    internal static void Spell2()
    {
        using (Packet _packet = new Packet((int)ClientPackets.spell2))
        {
            SendTCPData(_packet);
        }
    }

    public static void BasicAttack(Vector3 _facing)
    {
        using (Packet _packet = new Packet((int)ClientPackets.basicAttack))
        {
            _packet.Write(_facing);
            SendTCPData(_packet);
        }
    }

    public static void Spell1(Vector3 _facing)
    {
        using (Packet _packet = new Packet((int)ClientPackets.spell1))
        {
            _packet.Write(_facing);

            SendTCPData(_packet);
        }
    }
    #endregion
}