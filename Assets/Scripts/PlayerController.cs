using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Transform camTransform;
    private void FixedUpdate()
    {
        
    }

    private void SendInputToServer()
    {
        bool[] _inputs = new bool[]
        {
            Input.GetKey(KeyCode.W),
            Input.GetKey(KeyCode.S),
            Input.GetKey(KeyCode.A),
            Input.GetKey(KeyCode.D),
            Input.GetKey(KeyCode.Space)
        };

        ClientSend.PlayerMovement(_inputs);
    }

    private void Update()
    {
        SendInputToServer();
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            ClientSend.BasicAttack(camTransform.forward);
            Debug.Log("Basic");
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ClientSend.Spell1(camTransform.forward);
            Debug.Log("Spell1");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
            ClientSend.Spell2();

        if (Input.GetKeyDown(KeyCode.Alpha2))
            Debug.Log("Spell 3");
        if (Input.GetKeyDown(KeyCode.Alpha2))
            Debug.Log("Spell 4");

        if (Input.GetKeyDown(KeyCode.F4))
            Application.Quit();
    }
}
