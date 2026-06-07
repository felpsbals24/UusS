using UnityEngine;

public class AtributosAilone : MonoBehaviour
{
    public static AtributosAilone instancia;

    [Header("Multiplicadores de Velocidade")]
    public float multiplicadorVelocidadeAilone = 1f;
    public float multiplicadorVelocidadeOnda = 1f;
    public float multiplicadorVelocidadeCortadora = 1f;

    
    [Header("Multiplicadores de Dano")]
    public float multiplicadorDanoOnda = 1f; 

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

        
        if (multiplicadorVelocidadeAilone > 3.5f)
        {
            multiplicadorVelocidadeAilone = 3.5f;
        }
    }

    public void AumentarVelocidadeOnda(float valor)
    {
        multiplicadorVelocidadeOnda += valor;
    }

    public void AumentarVelocidadeCortadora(float valor)
    {
        multiplicadorVelocidadeCortadora += valor;
    }

    // --- SUA FUNÇÃO PREENCHIDA AQUI ---
    public void AumentarDanoOnda(float aumento)
    {
        
        multiplicadorDanoOnda += aumento;
    }
}