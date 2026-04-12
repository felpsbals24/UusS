using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Configurações")]
    public float speed = 2.5f;

    private Transform playerTransform;
    private Rigidbody2D rb;

   
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
            
            Vector2 direction = (playerTransform.position - transform.position).normalized;

            
            Vector2 newPosition = rb.position + direction * speed * Time.fixedDeltaTime;
            rb.MovePosition(newPosition);

            
            if (animator != null)
            {
                animator.SetFloat("MoveX", direction.x);
                animator.SetFloat("MoveY", direction.y);
            }

            
            if (direction.x < 0)
            {
                spriteRenderer.flipX = true; 
            }
            else if (direction.x > 0)
            {
                spriteRenderer.flipX = false; 
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
           
            if (animator != null)
            {
                animator.SetTrigger("IsDead");
            }

            this.enabled = false;
            rb.simulated = false;

            
            if (GerenciadorDeJogo.instancia != null)
            {
                GerenciadorDeJogo.instancia.UrsoMorreu();
            }
            

            Destroy(gameObject, 0.5f);
        }
    }
}