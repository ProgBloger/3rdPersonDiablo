using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour, IGameManager
{
    private NetworkService network;
    public ManagerStatus status {get; private set;}
    public Dictionary<string, int> items;
    public string EquippedItem {get; private set;}

    public bool EquipItem(string name){
        if(items.ContainsKey(name) && EquippedItem != name)
        {
            EquippedItem = name;
            Debug.Log($"Equipped {name}");
            return true;
        }

        EquippedItem = null;
        Debug.Log("Unequipped");
        return false;
    }

    public bool ConsumeItem(string name)
    {
        if(items.ContainsKey(name))
        {
            items[name]--;
            if(items[name] == 0)
            {
                items.Remove(name);
            }
        }
        else
        {
            Debug.Log($"Cannot consume {name}");
            return false;
        }

        DisplayItems();
        return true;
    }

    public void Startup(NetworkService service){
        Debug.Log("Inventory manager starting...");
        
        network = service;

        UpdateData(new Dictionary<string, int>());
        
        status = ManagerStatus.Started;
    }

    public void UpdateData(Dictionary<string, int> items){
        this.items = items;
    }

    public Dictionary<string, int> GetData(){
        return items;
    }

    private void DisplayItems(){
        string itemDisplay = "Items: ";
        foreach(var item in items)
        {
            itemDisplay += $"{item.Key}({item.Value})";
        }
        Debug.Log(itemDisplay);
    }

    public void AddItem(string name)
    {
        if(items.ContainsKey(name))
        {
            items[name] += 1;
        }
        else
        {
            items[name] = 1;
        }

        DisplayItems();
    }

    public List<string> GetItemList(){
        List<string> list = new List<string>(items.Keys);

        return list;
    }

    public int GetItemCount(string name){
        if(items.ContainsKey(name)){
            return items[name];
        }

        return 0;
    }
}
