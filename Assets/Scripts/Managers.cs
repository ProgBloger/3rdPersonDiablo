using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerManager))]
[RequireComponent(typeof(InventoryManager))]
[RequireComponent(typeof(MissionManager))]
[RequireComponent(typeof(DataManager))]
public class Managers : MonoBehaviour
{
    public static MissionManager Mission {get; private set;}
    public static PlayerManager Player {get; private set;}
    public static InventoryManager Inventory {get; private set;}
    public static DataManager Data {get; private set;}

    private List<IGameManager> startSequence;
    void Awake(){
        DontDestroyOnLoad(gameObject);

        Debug.Log("Awake managers");
        Data = GetComponent<DataManager>();
        Player = GetComponent<PlayerManager>();
        Inventory = GetComponent<InventoryManager>();
        Mission = GetComponent<MissionManager>();


        startSequence = new List<IGameManager>();
        startSequence.Add(Player);
        startSequence.Add(Inventory);
        startSequence.Add(Mission);
        // Data Manager uses other managers thus goes to the bottom
        startSequence.Add(Data);

        StartCoroutine(StartupManagers());
    }

    private IEnumerator StartupManagers(){
        Debug.Log("Startup managers");
        var service = new NetworkService();
        foreach(IGameManager manager in startSequence){
            manager.Startup(service);
        }

        yield return null;
        
        int numModules = startSequence.Count;
        int numReady = 0;

        while(numReady < numModules)
        {
            int lastReady = numReady;
            numReady = 0;

            foreach(IGameManager manager in startSequence)
            {
                if(manager.status == ManagerStatus.Started){
                    numReady++;
                }
            }

            if(numReady > lastReady)
            {
                Debug.Log($"Progress: {numReady}/{numModules}");
                Messenger<int, int>.Broadcast(StartupEvent.MANAGERS_PROGRESS, numReady, numModules);
            }
            yield return null;
        }

        Debug.Log("All managers started up");
        Messenger.Broadcast(StartupEvent.MANAGERS_STARTED);
    }
    
}
