using UnityEngine;

public class BasicAttackManager : MonoBehaviour
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
        GameManager.basicAttacks.Remove(id);
        Destroy(gameObject);
        Destroy(effect, 5f);
    }

}

