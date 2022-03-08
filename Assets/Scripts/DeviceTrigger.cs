using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceTrigger : MonoBehaviour
{
    [SerializeField] GameObject[] targets;
    
    public bool requreKey;
    void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Equipped item {Managers.Inventory.EquippedItem}");
        if(requreKey && Managers.Inventory.EquippedItem != "Key")
        {
            return;
        }
        foreach(GameObject target in targets)
        {
            target.SendMessage("Activate");
        }
    }

    void OnTriggerExit(Collider other)
    {
        foreach(GameObject target in targets)
        {
            target.SendMessage("Deactivate");
        }
    }
}
