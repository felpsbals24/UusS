using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerMoviment : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rigidbody2D;

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private Animator animator;

    



    private void LateUpdate()
    {
        Vector2 velocidade = this.rigidbody2D.linearVelocity;

        if ((velocidade.x != 0) || (velocidade.y != 0))
        {
            this.animator.SetBool("andando", true);
        } else
        {
            this.animator.SetBool("andando", false);
        }

        if (velocidade.x > 0)
        {
            this.spriteRenderer.flipX = false;
        } else if (velocidade.x < 0)
        {
            this.spriteRenderer.flipX = true;
        }
    }
}
