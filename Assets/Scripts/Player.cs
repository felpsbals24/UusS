using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator animator;

    private Vector2 movement;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement = movement.normalized;

        int animState = 0;

        if (movement.y > 0.1f && Mathf.Abs(movement.x) > 0.1f)
            animState = 4; // RunUpDiagonal
        else if (movement.y < -0.1f && Mathf.Abs(movement.x) > 0.1f)
            animState = 5; // RunDownDiagonal
        else if (movement.y > 0.1f)
            animState = 2; // RunUp
        else if (movement.y < -0.1f)
            animState = 3; // RunDown
        else if (Mathf.Abs(movement.x) > 0.1f)
            animState = 1; // RunSide
        else
            animState = 0; // Idle

        // flip normal
        if (movement.x > 0)
            sr.flipX = false;
        else if (movement.x < 0)
            sr.flipX = true;

        // exceção: diagonal para baixo está invertida
        if (animState == 5)
        {
            if (movement.x > 0)
                sr.flipX = true;
            else if (movement.x < 0)
                sr.flipX = false;
        }

        animator.SetInteger("AnimState", animState);
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = movement * speed;
    }
}