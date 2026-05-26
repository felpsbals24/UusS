using UnityEngine;
using System.Collections.Generic;

public class PocaAcidoGelatina : MonoBehaviour
{
    [Header("Configurações do Ácido")]
    public int danoPorTick = 2;
    public float intervaloDano = 0.4f; // Dá dano a cada 0.4 segundos
    public float tempoDeVida = 3.0f; // Dura 3 segundos no chão

    private List<EnemyHealth> inimigosNaPoca = new List<EnemyHealth>();
    private float cronometroTick;

    void Start()
    {
        // Se autodestrói após 3 segundos
        Destroy(gameObject, tempoDeVida);
    }

    void Update()
    {
        cronometroTick += Time.deltaTime;
        if (cronometroTick >= intervaloDano)
        {
            cronometroTick = 0f;
            AplicarDanoContinuo();
        }
    }

    void AplicarDanoContinuo()
    {
        // Limpa referências nulas de ursos que morreram enquanto estavam na poça
        inimigosNaPoca.RemoveAll(item => item == null);

        foreach (EnemyHealth inimigo in inimigosNaPoca)
        {
            inimigo.TomarDano(danoPorTick);
        }
    }

    // Detecta ursos que entraram na poça de gelatina
    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyHealth hpInimigo = collision.GetComponent<EnemyHealth>();
        if (hpInimigo != null && !inimigosNaPoca.Contains(hpInimigo))
        {
            inimigosNaPoca.Add(hpInimigo);
        }
    }

    // Detecta ursos que saíram da poça de gelatina
    private void OnTriggerExit2D(Collider2D collision)
    {
        EnemyHealth hpInimigo = collision.GetComponent<EnemyHealth>();
        if (hpInimigo != null)
        {
            inimigosNaPoca.Remove(hpInimigo);
        }
    }
}