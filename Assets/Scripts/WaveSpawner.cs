using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [Header("Configurações do Spawner")]
    public GameObject bearPrefab;
    public float spawnsPerSecond = 2f;
    public float spawnRadius = 12f;

    // NOVA PARTE: Limites de inimigos
    public int maxBears = 20; // Quantidade máxima a ser criada
    private int bearsSpawned = 0; // Contador de quantos já nasceram

    private Transform playerTransform;
    private float nextSpawnTime;

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            playerTransform = playerObj.transform;
        }
    }

    void Update()
    {
        
        if (bearsSpawned < maxBears && Time.time >= nextSpawnTime && playerTransform != null)
        {
            SpawnBear();
            bearsSpawned++; // Aumenta o contador (+1)
            nextSpawnTime = Time.time + (1f / spawnsPerSecond);
        }
    }

    void SpawnBear()
    {
        float randomAngle = Random.Range(0f, 360f);
        float angleRad = randomAngle * Mathf.Deg2Rad;
        Vector2 spawnDirection = new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
        Vector2 spawnPosition = (Vector2)playerTransform.position + (spawnDirection * spawnRadius);

        Instantiate(bearPrefab, spawnPosition, Quaternion.identity);
    }
}