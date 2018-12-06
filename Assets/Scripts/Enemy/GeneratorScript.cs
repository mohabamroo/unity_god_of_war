using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorScript : MonoBehaviour
{

    private float time;
    public Transform[] enemies;
    public int interval = 70;
    public int enemiesLimit = 20;

    // Use this for initialization
    void Start()
    {
        time = 50;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (time > this.interval)
        {
            SpawnWave();
            time = 0;
        }
    }

    void SpawnWave()
    {
        int y = 0;
        for (var i = 0; i < 5; i++)
        {
            var currentEnemies = GameObject.FindGameObjectsWithTag("Enemy");
            if (currentEnemies.Length < this.enemiesLimit)
            {
                var randomXPostion = Random.Range(0, 50);
                var randomZPostion = Random.Range(0, 50);
                var enemyType = Random.Range(0, enemies.Length);

                Instantiate(enemies[enemyType], new Vector3(randomXPostion, y, randomZPostion), Quaternion.identity);
            }

        }
    }
}
