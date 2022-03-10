using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    [SerializeField] string itemName;
    [SerializeField] int id;
        float baseColor = 0.0f;
        float duration = 1.5f;
        private float t = 0;
        bool isReset = false;

    void Update()
    {
        if(t < 1)
        {
            t += Time.deltaTime/duration;
        }
        Color random = Color.Lerp(Color.green,
        Color.red, t);
        GetComponent<Renderer>().material.color = random;
    }
    void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Item collected: {itemName}");
        Managers.Inventory.AddItem(itemName);
        Destroy(this.gameObject);
    }
}
