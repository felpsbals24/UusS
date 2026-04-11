using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class EnemyBearController : MonoBehaviour
{
    [Header("Alvo")]
    [SerializeField] private Transform alvo;

    [Header("Movimento")]
    [SerializeField] private float velocidade = 2.2f;
    [SerializeField] private float distanciaParada = 0.15f;

    [Header("Separacao")]
    [SerializeField] private float raioSeparacao = 0.75f;
    [SerializeField] private float forcaSeparacao = 1.35f;
    [SerializeField] private LayerMask camadaUrsos;

    private Rigidbody2D rb;
    private Collider2D meuCollider;
    private readonly Collider2D[] vizinhos = new Collider2D[16];
    private ContactFilter2D filtroSeparacao;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        meuCollider = GetComponent<Collider2D>();

        rb.gravityScale = 0f;
        rb.freezeRotation = true;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;

        filtroSeparacao = new ContactFilter2D();
        filtroSeparacao.useTriggers = false;
        filtroSeparacao.SetLayerMask(camadaUrsos);
    }

    private void FixedUpdate()
    {
        if (alvo == null)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        Vector2 direcaoAlvo = (Vector2)alvo.position - rb.position;

        if (direcaoAlvo.sqrMagnitude > distanciaParada * distanciaParada)
            direcaoAlvo.Normalize();
        else
            direcaoAlvo = Vector2.zero;

        Vector2 separacao = CalcularSeparacao();

        Vector2 direcaoFinal = direcaoAlvo + separacao * forcaSeparacao;

        if (direcaoFinal.sqrMagnitude > 1f)
            direcaoFinal.Normalize();

        rb.linearVelocity = direcaoFinal * velocidade;
    }

    private Vector2 CalcularSeparacao()
    {
        int quantidade = Physics2D.OverlapCircle(
            (Vector2)transform.position,
            raioSeparacao,
            filtroSeparacao,
            vizinhos
        );

        Vector2 resultado = Vector2.zero;

        for (int i = 0; i < quantidade; i++)
        {
            Collider2D outro = vizinhos[i];

            if (outro == null || outro == meuCollider)
                continue;

            Vector2 afastamento = (Vector2)(transform.position - outro.transform.position);
            float distancia = afastamento.magnitude;

            if (distancia <= 0.001f)
                continue;

            float peso = 1f - Mathf.Clamp01(distancia / raioSeparacao);
            resultado += afastamento.normalized * peso;
        }

        if (resultado == Vector2.zero)
            return Vector2.zero;

        return resultado.normalized;
    }

    public void DefinirAlvo(Transform novoAlvo)
    {
        alvo = novoAlvo;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, raioSeparacao);
    }
}