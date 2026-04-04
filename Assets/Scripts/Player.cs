using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;

    private SpriteRenderer sr;
    private Animator anim;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");

        
        transform.position += new Vector3(moveX, 0f, 0f) * speed * Time.deltaTime;

        
        anim.SetFloat("Speed", Mathf.Abs(moveX));

       
        if (moveX > 0)
            sr.flipX = false;
        else if (moveX < 0)
            sr.flipX = true;
    }
}