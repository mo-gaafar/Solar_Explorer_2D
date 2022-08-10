using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Enemy {
    public string Name;
    public GameObject Prefab;
    [Range (0f, 100f)] public float Chance = 100f;
    [HideInInspector] public double _weight;

}

public class EnemySpawner : MonoBehaviour {
    [SerializeField] public float SpawnInterval = 1f;
    [SerializeField] public static int MaxEnemies = 10;
    [SerializeField] public static int EnemyCounter= 0;


    [SerializeField] public float SpawnRadius = 10f;
    [HideInInspector] private float LastSpawnTime = 0f;

    [SerializeField] private Enemy[] enemies;

    [SerializeField] private Vector2 CenterPoint;

    private double accumulatedWeights;
    private System.Random rand = new System.Random ();

    public void DecreaseCounter()
    {
        EnemyCounter--;
    }
    private void Awake () {
        CenterPoint = transform.position;
        CalculateWeights ();
    }

    // Start is called before the first frame update
    void Start () {
        //for testing purposes
        //for (int i = 0; i < MaxEnemies; i++) {
        //    SpawnRandomEnemy ();
        //    EnemyCounter++;
        //}

    }

    private void SpawnRandomEnemy () {
        LastSpawnTime = Time.time;
        Vector2 spawnPosition = new Vector2 (CenterPoint.x+Random.Range (-SpawnRadius, SpawnRadius), CenterPoint.y + Random.Range (-SpawnRadius, SpawnRadius));
        GameObject enemyPrefabs = enemies[GetRandomEnemyIndex ()].Prefab;

        Instantiate (enemyPrefabs, spawnPosition, Quaternion.identity);
    }

    private int GetRandomEnemyIndex () {
        double randomNumber = rand.NextDouble () * accumulatedWeights;
        for (int i = 0; i < enemies.Length; i++) {
            if (enemies[i]._weight >= randomNumber) {
                return i;
            }
        }
        return 0;
    }

    private void CalculateWeights () {
        accumulatedWeights = 0f;
        foreach (Enemy enemy in enemies) {
            accumulatedWeights += enemy.Chance;
            enemy._weight = accumulatedWeights;
        }
    }

    // Update is called once per frame
    void Update () {

        if (SpawnInterval + LastSpawnTime < Time.time) {
            //TODO: add check for currently alive enemies and max enemies
            if (EnemyCounter < MaxEnemies)
            {
                SpawnRandomEnemy ();
                EnemyCounter++;
            }
        }

    }

}