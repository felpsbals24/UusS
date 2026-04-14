using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float velocidade = 2f;
    public float quantidadeAcucar = 10f;
    public bool spriteOriginalViradoParaEsquerda = false; // MARQUE ISSO NO BOMBER!

    private Transform player;
    private EnemyHealth saude;
    private bool atacou = false;
    private Animator animator;
    private Rigidbody2D rb;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        saude = GetComponent<EnemyHealth>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (player != null && !atacou && rb != null)
        {
            Vector2 direcao = (player.position - transform.position).normalized;

            if (animator != null)
            {
                animator.SetFloat("Horizontal", direcao.x);
                animator.SetFloat("Vertical", direcao.y);
            }

            float escalaX = direcao.x < 0 ? -1f : 1f;

          
            if (spriteOriginalViradoParaEsquerda)
            {
                escalaX *= -1f;
            }

            transform.localScale = new Vector3(escalaX, 1, 1);

            Vector2 novaPosicao = Vector2.MoveTowards(rb.position, player.position, velocidade * Time.fixedDeltaTime);
            rb.MovePosition(novaPosicao);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !atacou)
        {
            atacou = true;
            SistemaDeVida vidaPlayer = collision.gameObject.GetComponent<SistemaDeVida>();
            if (vidaPlayer != null) vidaPlayer.ReceberAcucar(quantidadeAcucar);
            if (saude != null) saude.TomarDano(9999);
        }
    }
}