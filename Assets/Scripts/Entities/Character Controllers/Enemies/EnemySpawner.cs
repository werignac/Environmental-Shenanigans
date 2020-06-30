using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyType;
    public float spawnTime;
    public int enemyNumber;
    private List<GameObject> enemies;
    private float count;
    // Start is called before the first frame update
    void Start()
    {
        enemies = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < enemies.Count; ++i)
        {
            if(enemies[i] == null)
            {
                enemies.RemoveAt(i);
                --i;
            }
        }
        if(enemies.Count < enemyNumber || enemyNumber < 0)
        {
            count += Time.deltaTime;
            if(count > spawnTime)
            {
                count = 0;
                enemies.Add(Instantiate(enemyType, transform));
                enemies[enemies.Count - 1].transform.position = transform.position;
                enemies[enemies.Count - 1].transform.rotation = transform.rotation;
            }
        }
    }
}
