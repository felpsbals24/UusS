using UnityEngine;

public class GlicoseCutter : MonoBehaviour
{
    public float velocidadeGiro = 150f;
    public int danoDaArma = 15;

    void Update()
    {
        float velocidadeAtual = velocidadeGiro;

        if (AtributosAilone.instancia != null)
        {
            velocidadeAtual *= AtributosAilone.instancia.multiplicadorVelocidadeCortadora;
        }

        transform.Rotate(0, 0, velocidadeAtual * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyHealth inimigo = collision.GetComponent<EnemyHealth>();
        if (inimigo != null)
        {
            inimigo.TomarDano(danoDaArma);
        }
    }
}