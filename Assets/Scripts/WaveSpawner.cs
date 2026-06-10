using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class ConfiguracaoDaOnda
{
    public string nomeDaOnda;
    public GameObject[] inimigosDestaOnda;

    [Header("Configuração de Boss")]
    public bool eHordaDoBoss = false;
    public GameObject prefabDoBoss;
}

public class WaveSpawner : MonoBehaviour
{
    [Header("Configurações de Hordas")]
    public List<ConfiguracaoDaOnda> hordasCustomizadas;

    [Header("Inimigos para Ondas Infinitas")]
    public GameObject[] todosOsInimigos;

    [Header("Configurações de Spawn")]
    public float tempoEntreSpawns = 1.5f;
    public float raioDeSpawn = 10f;

    [Header("Limites do Mapa (Clamp)")]
    public float limiteEsquerdo = -20f;
    public float limiteDireito = 20f;
    public float limiteBaixo = -15f;
    public float limiteCima = 15f;

    [Header("Ajustes Finos")]
    [Tooltip("Espaço extra da parede para o urso não nascer dentro dela e bugar")]
    public float margemDeSeguranca = 2f;
    [Tooltip("Marca isso se quiser que eles nasçam cravados no Grid (ex: 10, 11) em vez de números quebrados (10.5)")]
    public bool alinharNoGrid = true;

    private int maxBears;
    private int bearsSpawned;
    private int nivelDaOndaAtual = 1;
    private float tempoAtual = 0f;
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
        nivelDaOndaAtual = onda;
        int indiceDaHorda = nivelDaOndaAtual - 1;

        if (indiceDaHorda < hordasCustomizadas.Count && hordasCustomizadas[indiceDaHorda].eHordaDoBoss)
        {
            maxBears = quantidadeDeUrsos + 1;

            GameObject boss = hordasCustomizadas[indiceDaHorda].prefabDoBoss;
            if (boss != null && player != null)
            {
                Vector2 direcaoAleatoria = Random.insideUnitCircle.normalized;
                Vector2 posAleatoria = (Vector2)player.position + (direcaoAleatoria * raioDeSpawn);

                posAleatoria = AplicarLimitesEGrid(posAleatoria);

                Instantiate(boss, posAleatoria, Quaternion.identity);
                bearsSpawned = 1;
            }
        }
        else
        {
            maxBears = quantidadeDeUrsos;
            bearsSpawned = 0;
        }

        tempoAtual = 0f;
    }

    void SpawnBear()
    {
        if (player == null) return;

        GameObject prefabParaInstanciar = null;
        int indiceDaHorda = nivelDaOndaAtual - 1;

        if (indiceDaHorda < hordasCustomizadas.Count)
        {
            GameObject[] listaDaOnda = hordasCustomizadas[indiceDaHorda].inimigosDestaOnda;
            if (listaDaOnda.Length > 0) prefabParaInstanciar = listaDaOnda[Random.Range(0, listaDaOnda.Length)];
        }
        else
        {
            if (todosOsInimigos.Length > 0) prefabParaInstanciar = todosOsInimigos[Random.Range(0, todosOsInimigos.Length)];
        }

        if (prefabParaInstanciar != null)
        {
            Vector2 direcaoAleatoria = Random.insideUnitCircle.normalized;
            Vector2 posAleatoria = (Vector2)player.position + (direcaoAleatoria * raioDeSpawn);

            posAleatoria = AplicarLimitesEGrid(posAleatoria);

            Instantiate(prefabParaInstanciar, posAleatoria, Quaternion.identity);
            bearsSpawned++;
        }
    }

    // --- NOVA FUNÇÃO QUE FILTRA A POSIÇÃO ---
    Vector2 AplicarLimitesEGrid(Vector2 posicaoOriginal)
    {
        // 1. Aplica o Clamp já recuando o valor com a margem de segurança
        float xSeguro = Mathf.Clamp(posicaoOriginal.x, limiteEsquerdo + margemDeSeguranca, limiteDireito - margemDeSeguranca);
        float ySeguro = Mathf.Clamp(posicaoOriginal.y, limiteBaixo + margemDeSeguranca, limiteCima - margemDeSeguranca);

        // 2. Trava no grid exato se a caixinha estiver marcada
        if (alinharNoGrid)
        {
            xSeguro = Mathf.Round(xSeguro);
            ySeguro = Mathf.Round(ySeguro);
        }

        return new Vector2(xSeguro, ySeguro);
    }
}