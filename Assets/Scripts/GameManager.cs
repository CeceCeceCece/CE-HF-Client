using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public static Dictionary<int, PlayerManager> players = new Dictionary<int, PlayerManager>();
    public static Dictionary<int, ItemSpawner> itemSpawners = new Dictionary<int, ItemSpawner>();
    public static Dictionary<int, FireballManager> fireballs = new Dictionary<int, FireballManager>();
    public static Dictionary<int, BasicAttackManager> basicAttacks = new Dictionary<int, BasicAttackManager>();



    public GameObject localPlayerPrefab;
    public GameObject playerPrefab;
    public GameObject itemSpawnerPrefab;
    public GameObject fireballPrefab;
    public GameObject basicAttackPrefab;
    public GameObject blastWavePrefab;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    public void SpawnPlayer(int _id, string _username, string _class, Vector3 _position, Quaternion _rotation)
    {
        GameObject _player;
        if (_id == Client.instance.myId)
        {
            _player = Instantiate(localPlayerPrefab, _position, _rotation);
        }
        else
        {
            _player = Instantiate(playerPrefab, _position, _rotation);
        }

        _player.GetComponent<PlayerManager>().Initialize(_id, _username, _class);
        players.Add(_id, _player.GetComponent<PlayerManager>());
    }
    public void CreateItemSpawner(int _spawnerId, Vector3 _position, bool _hasItem)
    {
        GameObject _spawner = Instantiate(itemSpawnerPrefab, _position, itemSpawnerPrefab.transform.rotation);
        _spawner.GetComponent<ItemSpawner>().Initialize(_spawnerId, _hasItem);
        itemSpawners.Add(_spawnerId, _spawner.GetComponent<ItemSpawner>());
    }
    public void SpawnFireball(int _id, Vector3 _position)
    {
        GameObject _fireball = Instantiate(fireballPrefab, _position, Quaternion.identity);
        _fireball.GetComponent<FireballManager>().Initialize(_id);
        fireballs.Add(_id, _fireball.GetComponent<FireballManager>());
    }

    public void SpawnBasicAttack(int _id, Vector3 _position)
    {
        GameObject _basic = Instantiate(basicAttackPrefab, _position, Quaternion.identity);
        _basic.GetComponent<BasicAttackManager>().Initialize(_id);
        basicAttacks.Add(_id, _basic.GetComponent<BasicAttackManager>());
    }

    public void BlastWave(Vector3 position)
    {
        var effect = Instantiate(blastWavePrefab, new Vector3(position.x, position.y - 0.5f, position.z), Quaternion.identity);
        Destroy(effect, 5f);

    }

    /*public void SpawnEnemy(int _id, Vector3 _position)
    {
        GameObject _enemy = Instantiate(enemyPrefab, _position, Quaternion.identity);
        _enemy.GetComponent<EnemyManager>().Initialize(_id);
        enemies.Add(_id, _enemy.GetComponent<EnemyManager>());
    }*/
}