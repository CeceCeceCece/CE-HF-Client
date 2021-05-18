using UnityEngine;

public class Billboard : MonoBehaviour
{
   private Camera cam;


    private void Start()
    {
        cam = GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<Camera>();
    }
    private void LateUpdate()
    {
        transform.LookAt(transform.position + cam.transform.forward);
    }
}

