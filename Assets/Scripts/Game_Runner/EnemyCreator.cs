using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class EnemyCreator : MonoBehaviour
{
    public static EnemyCreator instance;

    [SerializeField]
    private float spawnDelay = 3f;
    [SerializeField]
    private float spawnRadius = 3f;
    [SerializeField]
    private FlyingEnemy[] enemyPrefabs;
    [SerializeField]
    private int maxEnemyCount;
    [SerializeField]
    private Transform[] spawnPoints;

    private List<FlyingEnemy> enemies = new List<FlyingEnemy>();
    private Transform player;


    private IEnumerator Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        player = Camera.main.transform;

        while (true)
        {
            yield return new WaitForSeconds(spawnDelay);

            if (enemies.Count < maxEnemyCount)
            {
                Vector3 randomPos = Random.insideUnitSphere * spawnRadius;
                randomPos.z = Mathf.Abs(randomPos.z);
                randomPos.y *= 0.1f;
                FlyingEnemy enemy = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], player.position + randomPos, Quaternion.identity);
                //FlyingEnemy enemy = Instantiate(enemyPrefab, spawnPoints[Random.Range(0, spawnPoints.Length)].position, Quaternion.identity);
                enemies.Add(enemy);
            }
        }
    }

    public void EnemyRemove(FlyingEnemy enemy)
    {
        enemies.Remove(enemy);
    }
}
