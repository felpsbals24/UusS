using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int vidaMaxima = 10;
    private int vidaAtual;

    public GameObject pirulitoPrefab;

    private Animator animator;
    private bool isDying = false;

    void Start()
    {
        vidaAtual = vidaMaxima;
        animator = GetComponent<Animator>();
    }

    public void TomarDano(int dano)
    {
        if (isDying) return;

        vidaAtual -= dano;

        if (vidaAtual <= 0)
        {
            Morrer();
        }
    }

    void Morrer()
    {
        isDying = true;

        if (pirulitoPrefab != null)
        {
            Instantiate(pirulitoPrefab, transform.position, Quaternion.identity);
        }

        if (animator != null)
        {
            animator.SetTrigger("Morte");
        }

        if (GerenciadorDeJogo.instancia != null)
        {
            GerenciadorDeJogo.instancia.UrsoMorreu();
        }

        Collider2D col = GetComponent<Collider2D>();
        if (col != null)
        {
            col.enabled = false;
        }

        EnemyAI ia = GetComponent<EnemyAI>();
        if (ia != null)
        {
            ia.enabled = false;
        }

        Destroy(gameObject, 1.5f);
    }
}