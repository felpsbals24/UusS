using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public GameObject[] prefabsInimigos; // 0:Verde, 1:Warrior, 2:Shield, 3:Bomber

    public int maxBears = 20;
    public int bearsSpawned = 0;
    private int nivelDaOndaAtual = 1;

    public float tempoEntreSpawns = 1.5f;
    private float tempoAtual = 0f;
    public float raioDeSpawn = 10f;
    private Transform player;

    void Start()
    {
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null) player = p.transform;
    }

    void Update()
    {
        if (bearsSpawned < maxBears && player != null)
        {
            tempoAtual += Time.deltaTime;
            if (tempoAtual >= tempoEntreSpawns)
            {
                SpawnBear();
                tempoAtual = 0f;
            }
        }
    }

    public void IniciarNovaOnda(int quantidadeDeUrsos, int onda)
    {
        maxBears = quantidadeDeUrsos;
        bearsSpawned = 0;
        tempoAtual = 0f;
        nivelDaOndaAtual = onda;
    }

    void SpawnBear()
    {
        if (prefabsInimigos.Length < 4 || player == null) return;

        int inimigoSorteado = 0;

        // LÓGICA DE PROGRESSÃO:
        if (nivelDaOndaAtual == 1)
            inimigoSorteado = 0; // Só Simples
        else if (nivelDaOndaAtual == 2)
            inimigoSorteado = 1; // Só Warrior
        else if (nivelDaOndaAtual == 3)
            inimigoSorteado = Random.Range(2, 4); // Shield (2) ou Bomber (3)
        else
            inimigoSorteado = Random.Range(0, prefabsInimigos.Length); // Todos Aleatórios

        Vector2 direcaoAleatoria = Random.insideUnitCircle.normalized;
        Vector2 posicaoAleatoria = (Vector2)player.position + (direcaoAleatoria * raioDeSpawn);

        Instantiate(prefabsInimigos[inimigoSorteado], posicaoAleatoria, Quaternion.identity);
        bearsSpawned++;
    }
}