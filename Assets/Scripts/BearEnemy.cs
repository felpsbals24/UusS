using System.Collections;
using UnityEngine;

public class BearEnemy : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float deathDuration = 0.5f;

    private Transform player;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator animator;
    private Collider2D enemyCollider;

    private bool isDead;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        enemyCollider = GetComponent<Collider2D>();
    }

    private void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("Nenhum objeto com tag Player foi encontrado.");
        }
    }

    private void FixedUpdate()
    {
        if (isDead || player == null) return;

        Vector2 direction = ((Vector2)player.position - rb.position).normalized;
        Vector2 newPosition = rb.position + direction * speed * Time.fixedDeltaTime;

        UpdateAnimation(direction);
        rb.MovePosition(newPosition);
    }

    private void UpdateAnimation(Vector2 direction)
    {
        if (animator == null) return;

        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            animator.SetInteger("MoveDir", 0);

            if (direction.x > 0.05f)
                sr.flipX = false;
            else if (direction.x < -0.05f)
                sr.flipX = true;
        }
        else if (direction.y > 0f)
        {
            animator.SetInteger("MoveDir", 1);
        }
        else
        {
            animator.SetInteger("MoveDir", 2);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isDead) return;

        if (other.CompareTag("Player"))
        {
            StartCoroutine(Die());
        }
    }

    private IEnumerator Die()
    {
        isDead = true;

        if (enemyCollider != null)
            enemyCollider.enabled = false;

        if (animator != null)
            animator.SetTrigger("Die");

        yield return new WaitForSeconds(deathDuration);
        Destroy(gameObject);
    }
}