using UnityEngine;
using System.Collections;

public class HDirections : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private TrailRenderer trailRenderer; // Referência para o rastro
    [SerializeField] private float velocidadeMovimento = 5f;

    [Header("Configurações do Dash")]
    [SerializeField] private float forcaDoDash = 18f;
    [SerializeField] private float tempoDoDash = 0.2f;
    [SerializeField] private float tempoDeRecarga = 1f;
    [SerializeField] private Color corDoVulto = new Color(1f, 1f, 1f, 0.5f);

    private Vector2 movimento;
    private Vector2 ultimoMovimento = Vector2.down;

    private bool estaDandoDash = false;
    private bool podeDarDash = true;

    // --- NOVA VARIÁVEL DE INVENCIBILIDADE ---
    [HideInInspector] public bool estaInvencivel = false;

    private void Awake()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        if (animator == null) animator = GetComponent<Animator>();
        if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();

        // Pega o TrailRenderer e garante que comece desligado
        if (trailRenderer == null) trailRenderer = GetComponent<TrailRenderer>();
        if (trailRenderer != null) trailRenderer.emitting = false;
    }

    private void Update()
    {
        if (estaDandoDash) return;

        movimento.x = Input.GetAxisRaw("Horizontal");
        movimento.y = Input.GetAxisRaw("Vertical");
        movimento = movimento.normalized;

        animator.SetFloat("MoveX", movimento.x);
        animator.SetFloat("MoveY", movimento.y);
        animator.SetFloat("MoveMagnitude", movimento.sqrMagnitude);

        if (movimento != Vector2.zero)
        {
            ultimoMovimento = movimento;
            animator.SetFloat("LastMoveX", ultimoMovimento.x);
            animator.SetFloat("LastMoveY", ultimoMovimento.y);
        }

        if (Input.GetKeyDown(KeyCode.Space) && podeDarDash && movimento != Vector2.zero)
        {
            StartCoroutine(RotinaDeDash());
        }
    }

    private void FixedUpdate()
    {
        if (estaDandoDash)
        {
            rb.linearVelocity = ultimoMovimento * forcaDoDash;
            return;
        }

        float velocidadeAtual = velocidadeMovimento;
        if (AtributosAilone.instancia != null)
        {
            velocidadeAtual *= AtributosAilone.instancia.multiplicadorVelocidadeAilone;
        }

        rb.linearVelocity = movimento * velocidadeAtual;
    }

    private IEnumerator RotinaDeDash()
    {
        podeDarDash = false;
        estaDandoDash = true;
        estaInvencivel = true; // --- FICA INVENCÍVEL ---

        if (trailRenderer != null) trailRenderer.emitting = true; // --- LIGA O RASTRO ---

        Color corOriginal = spriteRenderer.color;
        spriteRenderer.color = corDoVulto;

        yield return new WaitForSeconds(tempoDoDash);

        estaDandoDash = false;
        estaInvencivel = false; // --- VOLTA A TOMAR DANO ---

        if (trailRenderer != null) trailRenderer.emitting = false; // --- DESLIGA O RASTRO ---

        spriteRenderer.color = corOriginal;

        yield return new WaitForSeconds(tempoDeRecarga);
        podeDarDash = true;
    }
}