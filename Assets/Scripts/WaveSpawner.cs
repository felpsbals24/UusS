using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class ConfiguracaoDaOnda
{
    public string nomeDaOnda;  
    public GameObject[] inimigosDestaOnda; 
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
        maxBears = quantidadeDeUrsos;
        bearsSpawned = 0;
        nivelDaOndaAtual = onda;
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

            if (listaDaOnda.Length > 0)
            {
                prefabParaInstanciar = listaDaOnda[Random.Range(0, listaDaOnda.Length)];
            }
        }
        else
        {
             
            if (todosOsInimigos.Length > 0)
            {
                prefabParaInstanciar = todosOsInimigos[Random.Range(0, todosOsInimigos.Length)];
            }
        }

        if (prefabParaInstanciar != null)
        {
            Vector2 direcaoAleatoria = Random.insideUnitCircle.normalized;
            Vector2 posicaoAleatoria = (Vector2)player.position + (direcaoAleatoria * raioDeSpawn);
            Instantiate(prefabParaInstanciar, posicaoAleatoria, Quaternion.identity);
            bearsSpawned++;
        }
    }
}