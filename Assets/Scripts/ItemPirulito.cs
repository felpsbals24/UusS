using UnityEngine;
using System.Collections;

public class ItemPirulito : MonoBehaviour
{
    public int valorXP = 5;

    public CircleCollider2D gatilhoRangeDetecao;
    public CircleCollider2D colisorFisicoPequeno;

    public float velocidadePerseguicao = 25f;
    public float distanciaColetaFinal = 0.5f;

    private Transform targetPlayer;
    private Rigidbody2D rb;
    private bool estaAtraindo = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
        }
        rb.gravityScale = 0;
        rb.linearDamping = 5f;

        estaAtraindo = false;
        if (gatilhoRangeDetecao != null) gatilhoRangeDetecao.enabled = true;
        if (colisorFisicoPequeno != null) colisorFisicoPequeno.enabled = true;
    }

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            targetPlayer = playerObj.transform;
        }

        Vector2 direcaoPulo = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        rb.AddForce(direcaoPulo * 7f, ForceMode2D.Impulse);
    }

    void Update()
    {
        if (estaAtraindo && targetPlayer != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPlayer.position, velocidadePerseguicao * Time.deltaTime);

            float distancia = Vector2.Distance(transform.position, targetPlayer.position);
            if (distancia < distanciaColetaFinal)
            {
                if (GerenciadorDeProgresso.instancia != null)
                {
                    GerenciadorDeProgresso.instancia.AdicionarXP(valorXP);
                }
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D outro)
    {
        if (!estaAtraindo && outro.CompareTag("Player"))
        {
            estaAtraindo = true;
            rb.linearVelocity = Vector2.zero;
            rb.simulated = false;

            if (gatilhoRangeDetecao != null) gatilhoRangeDetecao.enabled = false;
            if (colisorFisicoPequeno != null) colisorFisicoPequeno.enabled = false;
        }
    }
}