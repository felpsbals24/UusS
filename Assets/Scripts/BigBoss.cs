using UnityEngine;
using UnityEngine.UI; // Necessário para a Barra de Vida (Slider)
using System.Collections;

public class BigBoss : MonoBehaviour
{
    [Header("Movimentação do Boss")]
    public float velocidade = 2.5f;
    private Transform player;
    private Rigidbody2D rb;

    [Header("Animação")]
    public Animator anim;

    [Header("Combate")]
    public float forcaRepulsao = 15f;
    public float danoNoPlayer = 30f; // --- O DANO REAL QUE ELE DÁ ---
    public GameObject vfxRepulsao;

    [Header("Loot / Recompensas Especiais")]
    public GameObject moedaPrefab;
    public int quantidadeMoedas = 12;
    public int xpDoBoss = 100;

    // --- VARIÁVEIS DA BARRA DE VIDA DARK SOULS ---
    private Slider barraDeVida;
    private EnemyHealth scriptVidaBoss;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        scriptVidaBoss = GetComponent<EnemyHealth>();

        if (anim == null) anim = GetComponent<Animator>();

        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null) player = p.transform;

        // Procura a barra escondida na UI pelo nome exato
        GameObject objBarra = GameObject.Find("BarraDeVidaBoss");
        if (objBarra != null)
        {
            barraDeVida = objBarra.GetComponent<Slider>();

            // Liga a barra na tela e configura a vida máxima
            if (scriptVidaBoss != null)
            {
                barraDeVida.maxValue = scriptVidaBoss.vidaMaxima;
                barraDeVida.value = scriptVidaBoss.vidaMaxima;
            }
        }
    }

    void FixedUpdate()
    {
        if (player != null && rb != null)
        {
            Vector2 direcao = (player.position - transform.position).normalized;

            // --- CORREÇÃO TÓPICO 2: Usando física contínua para evitar bugs nas costas ---
            rb.linearVelocity = direcao * velocidade;

            if (anim != null)
            {
                if (Mathf.Abs(direcao.x) > Mathf.Abs(direcao.y))
                {
                    anim.SetFloat("MoveX", direcao.x > 0 ? 1 : -1);
                    anim.SetFloat("MoveY", 0);
                }
                else
                {
                    anim.SetFloat("MoveX", 0);
                    anim.SetFloat("MoveY", direcao.y > 0 ? 1 : -1);
                }
            }
        }
    }

    void Update()
    {
        // Atualiza a barrinha visualmente para acompanhar a vida real do EnemyHealth
        if (barraDeVida != null && scriptVidaBoss != null)
        {
            barraDeVida.value = scriptVidaBoss.vidaAtual;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Vector2 direcaoEmpurrao = (col.transform.position - transform.position).normalized;

            Rigidbody2D rbPlayer = col.gameObject.GetComponent<Rigidbody2D>();
            HDirections scriptPlayer = col.gameObject.GetComponent<HDirections>();

            // --- CORREÇÃO TÓPICO 1: Puxa o seu script de vida ---
            PlayerHealth vidaPlayer = col.gameObject.GetComponent<PlayerHealth>();

            if (rbPlayer != null && scriptPlayer != null)
            {
                scriptPlayer.AplicarKnockback(0.3f);
                rbPlayer.linearVelocity = Vector2.zero;
                rbPlayer.AddForce(direcaoEmpurrao * forcaRepulsao, ForceMode2D.Impulse);
            }

            // --- APLICA O DANO NO AILONE ---
            if (vidaPlayer != null)
            {
                vidaPlayer.ReceberDano(danoNoPlayer);
            }

            if (vfxRepulsao != null)
            {
                GameObject vfx = Instantiate(vfxRepulsao, transform.position, Quaternion.identity);
                vfx.transform.localScale = new Vector3(0.5f, 0.5f, 1f);
                Destroy(vfx, 0.5f);
            }
        }
    }

    void OnDestroy()
    {
        if (!gameObject.scene.isLoaded) return;

        // Esconde a barra épica quando o Boss for de arrasta pra cima
        if (barraDeVida != null) barraDeVida.gameObject.SetActive(false);

        DroparLootDoBoss();
    }

    void DroparLootDoBoss()
    {
        if (moedaPrefab != null)
        {
            for (int i = 0; i < quantidadeMoedas; i++)
            {
                Vector2 posicaoAleatoria = (Vector2)transform.position + Random.insideUnitCircle * 2f;
                Instantiate(moedaPrefab, posicaoAleatoria, Quaternion.identity);
            }
        }

        if (GerenciadorDeProgresso.instancia != null)
        {
            GerenciadorDeProgresso.instancia.AdicionarXP(xpDoBoss);
            Debug.Log("BOSS DERROTADO! Ganhou " + xpDoBoss + " XP!");
        }
    }
}