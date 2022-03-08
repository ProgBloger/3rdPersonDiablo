using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class InventoryPopup : MonoBehaviour
{
    [SerializeField] Image[] itemIcons;
    [SerializeField] TMP_Text[] itemLabels;

    [SerializeField] TMP_Text curItemLabel;
    [SerializeField] Button equipButton;
    [SerializeField] Button useButton;

    private string curItem;
    
    public void Refresh(){
        Debug.Log($"Inventroy manager is null {Managers.Inventory == null}");
        List<string> itemList = Managers.Inventory.GetItemList();
        Debug.Log("1");
        int len = itemIcons.Length;
        for(int i = 0; i < len; i++)
        {
            Debug.Log("2");
            if(i < itemList.Count)
            {
                Debug.Log("3");
                itemIcons[i].gameObject.SetActive(true);
                itemLabels[i].gameObject.SetActive(true);

                string item = itemList[i];

                Sprite sprite = Resources.Load<Sprite>($"Icons/{item}");

                itemIcons[i].sprite = sprite;
                // itemIcons[i].SetNativeSize();
Debug.Log("4");
                int count = Managers.Inventory.GetItemCount(item);
                string message = $"x{count}";
                if(item == Managers.Inventory.EquippedItem){
                    message = "Equipped\n" + message;
                }
                itemLabels[i].text = message;

                EventTrigger.Entry entry = new EventTrigger.Entry();
                entry.eventID = EventTriggerType.PointerClick;
                entry.callback.AddListener((BaseEventData data) => {
                    OnItem(item);
                });

                EventTrigger trigger = itemIcons[i].GetComponent<EventTrigger>();
                trigger.triggers.Clear();
                trigger.triggers.Add(entry);
                Debug.Log("5");
            }
            else{
                Debug.Log("6");
                itemIcons[i].gameObject.SetActive(false);
                itemLabels[i].gameObject.SetActive(false);
            }
        }
Debug.Log("7");
        if(!itemList.Contains(curItem)){
            curItem = null;
        }
        if(curItem == null)
        {
            curItemLabel.gameObject.SetActive(false);
            equipButton.gameObject.SetActive(false);
            useButton.gameObject.SetActive(false);
        }
        else{
            curItemLabel.gameObject.SetActive(true);
            equipButton.gameObject.SetActive(true);
            if(curItem == "Health"){
                useButton.gameObject.SetActive(true);
            }
            else{
                useButton.gameObject.SetActive(false);
            }

            curItemLabel.text = $"{curItem}:";
        }
    }

    public void OnItem(string item){
        curItem = item;
        Refresh();
    }

    public void OnEquip()
    {
        Managers.Inventory.EquipItem(curItem);
        Refresh();
    }

    public void OnUse(){
        Managers.Inventory.ConsumeItem(curItem);
        if(curItem == "Health"){
            Managers.Player.ChangeHealth(25);
        }
        Refresh();
    }
}
