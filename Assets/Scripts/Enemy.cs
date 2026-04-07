using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    [SerializeField] private float speed = 2f;

    private Transform player;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sr;

    private Vector2 moveDirection;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
            player = playerObject.transform;
    }

    private void Update()
    {
        if (player == null)
        {
            moveDirection = Vector2.zero;
            animator.SetBool("IsWalking", false);
            return;
        }

        moveDirection = ((Vector2)player.position - rb.position).normalized;

        animator.SetBool("IsWalking", moveDirection != Vector2.zero);

        if (moveDirection.x > 0.05f)
            sr.flipX = false;
        else if (moveDirection.x < -0.05f)
            sr.flipX = true;
    }

    private void FixedUpdate()
    {
        if (player == null) return;

        Vector2 newPosition = rb.position + moveDirection * speed * Time.fixedDeltaTime;
        rb.MovePosition(newPosition);
    }
}