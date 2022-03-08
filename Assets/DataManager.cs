using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status {get; private set;}

    private const string Inventory = "Inventory";
    private const string Health = "Health";
    private const string MaxHealth = "MaxHealth";
    private const string CurLevel = "CurLevel";
    private const string MaxLevel = "MaxLevel";
    private string filename;

    private NetworkService network;

    public void Startup(NetworkService service){
        Debug.Log("Data manager starting...");

        network = service;

        filename = Path.Combine(Application.persistentDataPath, "game.dat");

        status = ManagerStatus.Started;
    }

    public void SaveGameState(){
        Dictionary<string, object> gamestate = new Dictionary<string, object>();
        gamestate.Add(Inventory, Managers.Inventory.GetData());
        gamestate.Add(Health, Managers.Player.health);
        gamestate.Add(MaxHealth, Managers.Player.maxHealth);
        gamestate.Add(CurLevel, Managers.Mission.curLevel);
        gamestate.Add(MaxLevel, Managers.Mission.maxLevel);

        using(FileStream stream = File.Create(filename))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, gamestate);
        }
    }

    public void LoadGameState(){
        if(!File.Exists(filename)){
            Debug.Log("No saved game");
            return;
        }

        Dictionary<string, object> gamestate;

        using(FileStream stream = File.Open(filename, FileMode.Open)){
            BinaryFormatter formatter = new BinaryFormatter();
            gamestate = formatter.Deserialize(stream) as Dictionary<string ,object>;
        }

        Managers.Inventory.UpdateData((Dictionary<string, int>)gamestate[Inventory]);
        Managers.Player.UpdateData((int)gamestate[Health], (int)gamestate[MaxHealth]);
        Managers.Mission.UpdateData((int)gamestate[CurLevel], (int)gamestate[MaxLevel]);
        Managers.Mission.RestartCurrent();
    }
}
