using UnityEngine;
using System.Collections;

public class BigBoss : MonoBehaviour
{
    [Header("Status do Boss")]
    public int vidaMaxima = 1000;
    private int vidaAtual;

    [Header("Movimentação Aleatória")]
    public float velocidade = 3f;
    public float tempoTrocaDirecao = 2f;
    private Vector2 direcaoAtual;
    private Transform player;

    [Header("Ataque / Tiro")]
    public GameObject projetilPrefab;
    public Transform pontoDeTiro;
    public float tempoEntreTiros = 1.5f;

    [Header("Barra de Vida Visual")]
    public SpriteRenderer barraDeVidaRenderer;
    [Tooltip("Coloque os Sprites do VAZIO (0) ao CHEIO (Último)")]
    public Sprite[] spritesDeVida;

    [Header("Loot / Recompensas")]
    public GameObject moedaPrefab;
    public int quantidadeMoedas = 12;
    public int xpDoBoss = 100;

    [Header("Efeito Visuais")]
    public SpriteRenderer bossRenderer;
    public Color corDeDano = Color.red;

    void Start()
    {
        vidaAtual = vidaMaxima;

        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null) player = p.transform;

        StartCoroutine(RotinaMovimento());
        StartCoroutine(RotinaTiro());
        AtualizarBarraDeVida();
    }

    void Update()
    {
        // O Boss se move baseado na decisão que a RotinaMovimento tomou
        transform.Translate(direcaoAtual * velocidade * Time.deltaTime);
    }

    // --- INTELIGÊNCIA DE MOVIMENTO ---
    IEnumerator RotinaMovimento()
    {
        while (true)
        {
            if (player != null)
            {
                int sorteio = Random.Range(0, 100);
                if (sorteio > 40)
                {
                    // 60% de chance de ir na direção do Ailone
                    direcaoAtual = (player.position - transform.position).normalized;
                }
                else
                {
                    // 40% de chance de dar um "passo pro lado" ou andar aleatório
                    direcaoAtual = Random.insideUnitCircle.normalized;
                }
            }
            yield return new WaitForSeconds(tempoTrocaDirecao);
        }
    }

    // --- INTELIGÊNCIA DE TIRO ---
    IEnumerator RotinaTiro()
    {
        while (true)
        {
            yield return new WaitForSeconds(tempoEntreTiros);
            Atirar();
        }
    }

    void Atirar()
    {
        if (player != null && projetilPrefab != null && pontoDeTiro != null)
        {
            // Cria o tiro e calcula a direção pro Ailone
            GameObject tiro = Instantiate(projetilPrefab, pontoDeTiro.position, Quaternion.identity);
            Vector2 direcaoTiro = (player.position - pontoDeTiro.position).normalized;

            // Dá o empurrão no tiro (O Projetil precisa ter um Rigidbody2D!)
            Rigidbody2D rbTiro = tiro.GetComponent<Rigidbody2D>();
            if (rbTiro != null) rbTiro.linearVelocity = direcaoTiro * 8f;

            // Vira o tiro pra apontar pro jogador
            float angle = Mathf.Atan2(direcaoTiro.y, direcaoTiro.x) * Mathf.Rad2Deg;
            tiro.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    // --- SISTEMA DE DANO E VIDA ---
    public void TomarDano(int dano)
    {
        vidaAtual -= dano;
        AtualizarBarraDeVida();
        StartCoroutine(PiscarDano());

        if (vidaAtual <= 0) Morrer();
    }

    void AtualizarBarraDeVida()
    {
        // Calcula matematicamente qual sprite usar baseado na % de vida
        if (barraDeVidaRenderer != null && spritesDeVida.Length > 0)
        {
            int index = Mathf.Clamp((vidaAtual * spritesDeVida.Length) / vidaMaxima, 0, spritesDeVida.Length - 1);
            barraDeVidaRenderer.sprite = spritesDeVida[index];
        }
    }

    IEnumerator PiscarDano()
    {
        if (bossRenderer != null)
        {
            bossRenderer.color = corDeDano;
            yield return new WaitForSeconds(0.1f);
            bossRenderer.color = Color.white;
        }
    }

    // --- COLISÃO COM O PLAYER ---
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            // Se o Boss bater no Ailone, empurra e não morre!
            Debug.Log("Boss atropelou o Ailone!");
            // Aqui você pode chamar a função do Ailone tomar dano. Ex:
            // col.gameObject.GetComponent<VidaAilone>().TomarDano(20);
        }
    }

    // --- A GRANDE MORTE ---
    void Morrer()
    {
        // Dropa as 12 moedas em formato de explosão ao redor dele!
        for (int i = 0; i < quantidadeMoedas; i++)
        {
            Vector2 posicaoAleatoria = (Vector2)transform.position + Random.insideUnitCircle * 3f;
            Instantiate(moedaPrefab, posicaoAleatoria, Quaternion.identity);
        }

        // Dá a XP direta (Substitua pela sua chamada real de XP)
        if (GerenciadorDeProgresso.instancia != null)
        {
            // GerenciadorDeProgresso.instancia.AdicionarXP(xpDoBoss);
            Debug.Log("Ganhou " + xpDoBoss + " XP!");
        }

        Destroy(gameObject);
    }
}