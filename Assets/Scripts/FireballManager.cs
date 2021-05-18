using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballManager : MonoBehaviour
{
    public int id;
    public GameObject explosionPrefab;

    public void Initialize(int _id)
    {
        id = _id;
    }

    public void Explode(Vector3 _position)
    {
        transform.position = _position;
        var effect = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        GameManager.fireballs.Remove(id);
        Destroy(gameObject);
        Destroy(effect, 5f);
    }
}

