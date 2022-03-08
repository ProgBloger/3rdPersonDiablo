using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] TMP_Text healthLabel;
    [SerializeField] InventoryPopup popup;
    [SerializeField] TMP_Text levelEnding;
    [SerializeField] TMP_Text anotherHealthLabel;

    void OnEnable()
    {
        Messenger.AddListener(GameEvent.HEALTH_UPDATED, OnHealthUpdated);
        Messenger.AddListener(GameEvent.LEVEL_COMPLETE, OnLevelComplete);
        Messenger.AddListener(GameEvent.LEVEL_FAILED, OnLevelFailed);
        Messenger.AddListener(GameEvent.GAME_COMPLETE, OnGameComplete);
    }

    void OnDisable()
    {
        Messenger.RemoveListener(GameEvent.HEALTH_UPDATED, OnHealthUpdated);
        Messenger.RemoveListener(GameEvent.LEVEL_COMPLETE, OnLevelComplete);
        Messenger.RemoveListener(GameEvent.LEVEL_FAILED, OnLevelFailed);
        Messenger.RemoveListener(GameEvent.GAME_COMPLETE, OnGameComplete);
    }

    private void OnGameComplete(){
        levelEnding.gameObject.SetActive(true);
        levelEnding.text = "You Finished the Game!";
    }

    void Start(){
        OnHealthUpdated();

        levelEnding.gameObject.SetActive(false);
        popup.gameObject.SetActive(false);
    }

    public void SaveGame(){
        Managers.Data.SaveGameState();
    }

    public void LoadGame(){
        Managers.Data.LoadGameState();
    }

    private void OnLevelFailed(){
        StartCoroutine(FailLevel());
    }

    private IEnumerator FailLevel(){
        Debug.Log("Failed level entered");
        levelEnding.text = "Level Failed";
        levelEnding.gameObject.SetActive(true);

        yield return new WaitForSeconds(2);

        Managers.Player.Reaspawn();
        Managers.Mission.RestartCurrent();
    }

    private void OnLevelComplete(){
        StartCoroutine(CompleteLevel());
    }

    private IEnumerator CompleteLevel(){
        Debug.Log("Complete level entered");
        levelEnding.text = "Level Complete!";
        levelEnding.gameObject.SetActive(true);

        yield return new WaitForSeconds(2);

        Managers.Mission.GoToNext();
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.M)){
            bool isShowing = popup.gameObject.activeSelf;
            popup.gameObject.SetActive(!isShowing);
            popup.Refresh();
        }
    }

    public void OpenInventory(){
            bool isShowing = popup.gameObject.activeSelf;
            popup.gameObject.SetActive(!isShowing);
            popup.Refresh();
    }

    private void OnHealthUpdated()
    {
        string message = $"Health: {Managers.Player.health}/{Managers.Player.maxHealth}";
        healthLabel.text = message;
    }
}
