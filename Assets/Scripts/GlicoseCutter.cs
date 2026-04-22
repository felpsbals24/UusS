using UnityEngine;

public class GlicoseCutter: MonoBehaviour
{
    public float velocidadeGiro = 150f; // Velocidade da rotação
    public int danoDaArma = 15; // Dano que a espada dá nos ursos

    void Update()
    {
        // Gira o objeto infinitamente no eixo Z
        transform.Rotate(0, 0, velocidadeGiro * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Se a espada bater em alguém que tem vida (os ursos)
        EnemyHealth inimigo = collision.GetComponent<EnemyHealth>();
        if (inimigo != null)
        {
            inimigo.TomarDano(danoDaArma);
        }
    }
}