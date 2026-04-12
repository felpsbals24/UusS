using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Configurações")]
    public float speed = 2.5f;

    private Transform playerTransform;
    private Rigidbody2D rb;

    // Novas variáveis para Animação e Imagem
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            playerTransform = playerObj.transform;
        }
    }

    void FixedUpdate()
    {
        if (playerTransform != null)
        {
            // Descobre a direção
            Vector2 direction = (playerTransform.position - transform.position).normalized;

            // Move o urso
            Vector2 newPosition = rb.position + direction * speed * Time.fixedDeltaTime;
            rb.MovePosition(newPosition);

            // --- COMUNICAÇÃO COM O ANIMATOR ---
            if (animator != null)
            {
                animator.SetFloat("MoveX", direction.x);
                animator.SetFloat("MoveY", direction.y);
            }

            // Vira o desenho do urso para a esquerda ou direita
            if (direction.x < 0)
            {
                spriteRenderer.flipX = true; // Olhando pra esquerda
            }
            else if (direction.x > 0)
            {
                spriteRenderer.flipX = false; // Olhando pra direita
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // 1. Aciona o gatilho da Morte no Animator
            if (animator != null)
            {
                animator.SetTrigger("IsDead");
            }

            // 2. Desliga a física e a inteligência do urso pra ele parar de andar
            this.enabled = false;
            rb.simulated = false;

            // 3. Destrói o urso APÓS 0.5 segundos (tempo pra animação tocar)
            // OBS: Se a sua animação for mais longa, mude o 0.5f para 1.0f, etc.
            Destroy(gameObject, 0.5f);
        }
    }
}