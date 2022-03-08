using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitCamera : MonoBehaviour
{
    [SerializeField] Transform target;
    public float rotSpeed = 1.5f;

    private float rotY;
    private Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        rotY = transform.eulerAngles.y;
        offset = target.position - transform.position;
    }
    
    void LateUpdate()
    {
        rotY -= Input.GetAxis("Horizontal") * rotSpeed;
        Quaternion rotation = Quaternion.Euler(0, rotY, 0);
        transform.position = target.position - (rotation * offset);
        transform.LookAt(target);
    }

    public void RotateLeft()
    {
        rotY -= 5f * rotSpeed;
        Quaternion rotation = Quaternion.Euler(0, rotY, 0);
        transform.position = target.position - (rotation * offset);
        transform.LookAt(target);
    }

    
    public void RotateRight()
    {
        rotY -= -5f * rotSpeed;
        Quaternion rotation = Quaternion.Euler(0, rotY, 0);
        transform.position = target.position - (rotation * offset);
        transform.LookAt(target);
    }
}
