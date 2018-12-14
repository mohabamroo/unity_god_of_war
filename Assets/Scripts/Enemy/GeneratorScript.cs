using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorScript : MonoBehaviour
{

    private float time;
    public Transform[] enemies;
    public int interval = 70;
    public int enemiesLimit = 20;
    public int maxWaves = 3;
    public int enemiesLimitPerWave = 4;
    public int currentWave;

    // Use this for initialization
    void Start()
    {
        this.currentWave = 0;
        time = 50;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (time > this.interval && this.currentWave < this.maxWaves)
        {
            SpawnWave();
            time = 0;
        }
    }

    void SpawnWave()
    {
        this.currentWave++;
        print("Loading wave: " + this.currentWave.ToString());
        int y = 0;
        for (var i = 1; i < this.enemiesLimitPerWave; i++)
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
