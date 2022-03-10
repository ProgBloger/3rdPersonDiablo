using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenDevice : MonoBehaviour
{
    // Start is called before the first frame update
    
    [SerializeField] Vector3 dPos;
    [SerializeField] GameObject door;
    private bool open;

    public void Operate(){
        if(open)
        {   
            Vector3 pos = door.transform.position - dPos;
            door.transform.position = pos;
        }
        else
        {
            Vector3 pos = door.transform.position + dPos;
            door.transform.position = pos;
        }
        open = !open;
    }
}
