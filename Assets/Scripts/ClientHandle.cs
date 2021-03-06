using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class ClientHandle : MonoBehaviour
{
    public static void Welcome(Packet _packet)
    {
        string _msg = _packet.ReadString();
        int _myId = _packet.ReadInt();

        Debug.Log($"Message from server: {_msg}");
        Client.instance.myId = _myId;
        ClientSend.WelcomeReceived();

        Client.instance.udp.Connect(((IPEndPoint)Client.instance.tcp.socket.Client.LocalEndPoint).Port);
    }

    public static void SpawnPlayer(Packet _packet)
    {
        int _id = _packet.ReadInt();
        string _username = _packet.ReadString();
        string _class = _packet.ReadString();
        Vector3 _position = _packet.ReadVector3();
        Quaternion _rotation = _packet.ReadQuaternion();

        GameManager.instance.SpawnPlayer(_id, _username, _class, _position, _rotation);
    }

    public static void PlayerPosition(Packet _packet)
    {
        int _id = _packet.ReadInt();
        Vector3 _position = _packet.ReadVector3();

        GameManager.players[_id].transform.position = _position;
    }

    public static void PlayerRotation(Packet _packet)
    {
        int _id = _packet.ReadInt();
        Quaternion _rotation = _packet.ReadQuaternion();

        GameManager.players[_id].transform.rotation = _rotation;
    }

    public static void PlayerDisconnected(Packet _packet)
    {
        int _id = _packet.ReadInt();
        Destroy(GameManager.players[_id].gameObject);
        GameManager.players.Remove(_id);
    }
    public static void PlayerHealth(Packet _packet)
    {
        int _id = _packet.ReadInt();
        float _health = _packet.ReadFloat();
        GameManager.players[_id].SetHealth(_health);

    }

    public static void PlayerRespawned(Packet _packet)
    {
        int _id = _packet.ReadInt();
        GameManager.players[_id].Respawn();
    }

    public static void CreateItemSpawner(Packet _packet)
    {
        int _spawnerId = _packet.ReadInt();
        Vector3 _spawnerPosition = _packet.ReadVector3();
        bool _hasItem = _packet.ReadBool();

        GameManager.instance.CreateItemSpawner(_spawnerId, _spawnerPosition, _hasItem);
    }

    public static void ItemSpawned(Packet _packet)
    {
        int _spawnerId = _packet.ReadInt();

        GameManager.itemSpawners[_spawnerId].ItemSpawned();
    }

    public static void ItemPickedUp(Packet _packet)
    {
        int _spawnerId = _packet.ReadInt();
        int _byPlayer = _packet.ReadInt();
        Debug.Log(_byPlayer);

        foreach(int key in GameManager.players.Keys)
        {
            Debug.Log($"{key}: {GameManager.players[key]}");
        }
      

        GameManager.itemSpawners[_spawnerId].ItemPickedUp();
        GameManager.players[_byPlayer].itemCount++;
    }

    public static void SpawnFireball(Packet _packet)
    {
        int _fireballId = _packet.ReadInt();
        Vector3 _position = _packet.ReadVector3();
        int _thrownByPlayer = _packet.ReadInt();

        GameManager.instance.SpawnFireball(_fireballId, _position);
        GameManager.players[_thrownByPlayer].itemCount--;
    }

    public static void FireballPosition(Packet _packet)
    {
        int _fireballId = _packet.ReadInt();
        Vector3 _position = _packet.ReadVector3();

        if (GameManager.fireballs.TryGetValue(_fireballId, out FireballManager _projectile))
        {
            _projectile.transform.position = _position;
        }
    }

    public static void FireballExploded(Packet _packet)
    {
        int fireballId = _packet.ReadInt();
        Vector3 _position = _packet.ReadVector3();

        GameManager.fireballs[fireballId].Explode(_position);
    }

    public static void SpawnBasicAttack(Packet _packet)
    {
        int _basicId = _packet.ReadInt();
        Vector3 _position = _packet.ReadVector3();
        int _thrownByPlayer = _packet.ReadInt();

        GameManager.instance.SpawnBasicAttack(_basicId, _position);
        GameManager.players[_thrownByPlayer].itemCount--;
    }

    public static void BasicAttackPosition(Packet _packet)
    {
        int _basicId = _packet.ReadInt();
        Vector3 _position = _packet.ReadVector3();

        if (GameManager.basicAttacks.TryGetValue(_basicId, out BasicAttackManager _projectile))
        {
            _projectile.transform.position = _position;
        }
    }

    public static void BasicAttackHit(Packet _packet)
    {
        int _basicId = _packet.ReadInt();
        Vector3 _position = _packet.ReadVector3();

        GameManager.basicAttacks[_basicId].Explode(_position);
    }

    public static void BlastWaveCasted(Packet _packet)
    {
        Vector3 _position = _packet.ReadVector3();
        GameManager.instance.BlastWave(_position);
        
    }

   public static void IceBlockCasted(Packet _packet)
    {
        int playerID = _packet.ReadInt();
        GameManager.players[playerID].IceBlockCasted();
    }

    public static void IceBlockEnded(Packet _packet)
    {
        int playerID = _packet.ReadInt();
        GameManager.players[playerID].IceBlockEnded();
    }

    /*public static void SpawnEnemy(Packet _packet)
    {
        int _enemyId = _packet.ReadInt();
        Vector3 _position = _packet.ReadVector3();

        GameManager.instance.SpawnEnemy(_enemyId, _position);
    }

    public static void EnemyPosition(Packet _packet)
    {
        int _enemyId = _packet.ReadInt();
        Vector3 _position = _packet.ReadVector3();

        if (GameManager.enemies.TryGetValue(_enemyId, out EnemyManager _enemy))
        {
            _enemy.transform.position = _position;
        }
    }

    public static void EnemyHealth(Packet _packet)
    {
        int _enemyId = _packet.ReadInt();
        float _health = _packet.ReadFloat();

        GameManager.enemies[_enemyId].SetHealth(_health);*/
}
