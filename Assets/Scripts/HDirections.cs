using UnityEngine;
using System.Collections;
// Removemos a linha do OnScreenControls para evitar o erro de namespace!

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
    [HideInInspector] public bool tomandoKnockback = false;
    [HideInInspector] public bool estaInvencivel = false;

    // Usamos 'GameObject' em vez do componente específico para evitar o erro de compilação
    private GameObject virtualJoystick;
    private RectTransform joystickAlavancaTransform;
    private Vector2 joystickInput = Vector2.zero;

    private void Awake()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        if (animator == null) animator = GetComponent<Animator>();
        if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();

        if (trailRenderer == null) trailRenderer = GetComponent<TrailRenderer>();
        if (trailRenderer != null) trailRenderer.emitting = false;

        // Procura pelo objeto da Alavanca do analógico na cena
        virtualJoystick = GameObject.Find("Joystick_Alavanca");
        if (virtualJoystick != null)
        {
            joystickAlavancaTransform = virtualJoystick.GetComponent<RectTransform>();
        }
    }

    private void Update()
    {
        if (estaDandoDash) return;

        // Calcula a direção baseada na distância da alavanca em relação ao centro (Fundo)
        if (joystickAlavancaTransform != null && joystickAlavancaTransform.anchoredPosition != Vector2.zero)
        {
            // Pega a posição local da alavanca e limita entre -1 and 1
            joystickInput = joystickAlavancaTransform.anchoredPosition;

            // Supondo que seu Movement Range seja por volta de 50 a 70 pixels, normalizamos o vetor
            movimento = Vector2.ClampMagnitude(joystickInput / 50f, 1f);
        }
        else
        {
            // Se não mover o analógico, usa o teclado do PC
            movimento.x = Input.GetAxisRaw("Horizontal");
            movimento.y = Input.GetAxisRaw("Vertical");
        }

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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            TentarExecutarDash();
        }
    }

    public void TentarExecutarDash()
    {
        if (podeDarDash && movimento != Vector2.zero && !estaDandoDash)
        {
            StartCoroutine(RotinaDeDash());
        }
    }

    private void FixedUpdate()
    {
        // Se tomou uma porrada do Boss, ignora o joystick/teclado e deixa a física agir!
        if (tomandoKnockback) return;

        if (estaDandoDash)
        {
            rb.linearVelocity = ultimoMovimento * forcaDoDash;
            return;
        }

        float velocidadeAtual = velocidadeMovimento;

        if (DevModeManager.superVelocidade)
        {
            velocidadeAtual = 15f;
        }
        else if (AtributosAilone.instancia != null)
        {
            velocidadeAtual *= AtributosAilone.instancia.multiplicadorVelocidadeAilone;
        }

        rb.linearVelocity = movimento * velocidadeAtual; // Corrigido para 'movimento' em português caso use variável local
        rb.linearVelocity = movimento * velocidadeAtual;
    }

    private IEnumerator RotinaDeDash()
    {
        podeDarDash = false;
        estaDandoDash = true;
        estaInvencivel = true;

        if (trailRenderer != null) trailRenderer.emitting = true;

        Color corOriginal = spriteRenderer.color;
        spriteRenderer.color = corDoVulto;

        yield return new WaitForSeconds(tempoDoDash);

        estaDandoDash = false;
        estaInvencivel = false;

        if (trailRenderer != null) trailRenderer.emitting = false;

        spriteRenderer.color = corOriginal;

        yield return new WaitForSeconds(tempoDeRecarga);
        podeDarDash = true;
    }
    public void AplicarKnockback(float tempo)
    {
        StartCoroutine(RotinaKnockback(tempo));
    }

    private IEnumerator RotinaKnockback(float tempo)
    {
        tomandoKnockback = true; // Solta o controle
        yield return new WaitForSeconds(tempo);
        tomandoKnockback = false; // Pega o controle de volta
    }
    public void DiminuirCooldownDash(float reducao)
    {
        tempoDeRecarga -= reducao;
        if (tempoDeRecarga < 0.2f) tempoDeRecarga = 0.2f;
    }
}