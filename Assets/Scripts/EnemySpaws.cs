using System.Collections;
using UnityEngine;

public class SimpleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int amountToSpawn = 5;
    [SerializeField] private float spawnDelay = 1f;

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < amountToSpawn; i++)
        {
            Vector2 spawnPosition = new Vector2(
                Random.Range(-8f, 8f),
                Random.Range(-4f, 4f)
            );

            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

            yield return new WaitForSeconds(spawnDelay);
        }
    }
}