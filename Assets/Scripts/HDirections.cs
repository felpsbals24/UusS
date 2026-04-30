using UnityEngine;

public class HDirections : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] private float velocidadeMovimento = 5f;

    private Vector2 movimento;
    private Vector2 ultimoMovimento = Vector2.down;

    private void Awake()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody2D>();

        if (animator == null)
            animator = GetComponent<Animator>();
    }

    private void Update()
    {
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
    }

    private void FixedUpdate()
    {
        float velocidadeAtual = velocidadeMovimento;

        if (AtributosAilone.instancia != null)
        {
            velocidadeAtual *= AtributosAilone.instancia.multiplicadorVelocidadeAilone;
        }

        rb.linearVelocity = movimento * velocidadeAtual;
    }
}