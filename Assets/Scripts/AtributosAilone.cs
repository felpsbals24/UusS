using UnityEngine;

public class AtributosAilone : MonoBehaviour
{
    public static AtributosAilone instancia;

    public float multiplicadorVelocidadeAilone = 1f;
    public float multiplicadorVelocidadeOnda = 1f;
    public float multiplicadorVelocidadeCortadora = 1f;

    void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AumentarVelocidadeAilone(float valor)
    {
        multiplicadorVelocidadeAilone += valor;
    }

    public void AumentarVelocidadeOnda(float valor)
    {
        multiplicadorVelocidadeOnda += valor;
    }

    public void AumentarVelocidadeCortadora(float valor)
    {
        multiplicadorVelocidadeCortadora += valor;
    }
}