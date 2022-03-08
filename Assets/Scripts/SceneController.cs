using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    private List<GameObject> enemies = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    { 
        var coroutine = MakeMoreEnemies();
        StartCoroutine(coroutine);

        print("Before WaitAndPrint Finishes " + Time.time);
    }

    // every 2 seconds perform the print()
    private IEnumerator MakeMoreEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(5);
            
            var enemy = Instantiate(enemyPrefab) as GameObject;
            enemy.transform.position = new Vector3(0,2,0);
            float angle = Random.Range(0, 360);
            enemy.transform.Rotate(0, angle, 0);
            enemies.Add(enemy);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
