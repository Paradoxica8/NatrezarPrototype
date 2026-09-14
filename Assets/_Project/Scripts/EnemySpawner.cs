using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy")]
    [SerializeField] private GameObject enemyPrefab;

    [Header("Spawn")]
    [SerializeField] private float respawnDelay = 3f;

    private GameObject currentEnemy;
    private float respawnTimer;

    private void Start()
    {
        SpawnEnemy();
    }

    private void Update()
    {
        if (currentEnemy != null)
            return;

        respawnTimer -= Time.deltaTime;

        if (respawnTimer <= 0f)
        {
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        currentEnemy = Instantiate(
            enemyPrefab,
            transform.position,
            Quaternion.identity
        );

        respawnTimer = respawnDelay;

        Debug.Log("Enemy spawned!");
    }
}