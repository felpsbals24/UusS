using UnityEngine;
using TMPro; // Necessário para usar o TextMeshPro

public class DanoFlutuante : MonoBehaviour
{
    [Header("Configurações da Animação")]
    public float velocidadeSubida = 2f;
    public float tempoDeVida = 1f;

    private TextMeshPro textoMesh;
    private Color corDoTexto;
    private float temporizador;

    void Awake()
    {
        textoMesh = GetComponent<TextMeshPro>();
        corDoTexto = textoMesh.color;
        temporizador = tempoDeVida;

        // Dá um pequeno "empurrão" aleatório para os lados.
        // Assim, se o urso tomar vários hits seguidos, os números não ficam perfeitamente um em cima do outro.
        transform.position += new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.2f, 0.5f), 0);
    }

    // Essa função vai ser chamada pelo urso na hora que ele apanhar
    public void ConfigurarDano(int valorDano)
    {
        textoMesh.text = valorDano.ToString();
    }

    void Update()
    {
        // Move o texto para cima
        transform.position += Vector3.up * velocidadeSubida * Time.deltaTime;

        // Calcula a transparência (Fade Out)
        temporizador -= Time.deltaTime;
        float alfa = temporizador / tempoDeVida; // Vai de 1 até 0
        corDoTexto.a = alfa;
        textoMesh.color = corDoTexto;

        // Destrói o objeto quando o tempo acaba
        if (temporizador <= 0)
        {
            Destroy(gameObject);
        }
    }
}