using UnityEngine;
using System.Collections; // Precisamos disso para usar o temporizador (IEnumerator)

public class GerenciadorDeFase : MonoBehaviour
{
    public static GerenciadorDeFase instancia;

    [Header("Configuração de Ondas")]
    public int ondaAtual = 1;
    public int ondaMaxima = 5;

    [Header("Interface")]
    public GameObject telaFinalizacao;

    [Header("Efeitos")]
    public float tempoEsperaFimDeFase = 2f; // Tempo em segundos para curtir a morte do urso

    private bool faseConcluida = false;

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

    void Start()
    {
        if (telaFinalizacao != null)
        {
            telaFinalizacao.SetActive(false);
        }
    }

    public void RegistrarOndaConcluida(int ondaQueAcabou)
    {
        if (faseConcluida) return;

        Debug.Log("Onda concluída: " + ondaQueAcabou);

        if (ondaQueAcabou >= ondaMaxima)
        {
            FinalizarFase();
        }
    }

    void FinalizarFase()
    {
        faseConcluida = true;
        Debug.Log("Fase Concluída! Esperando o último urso morrer...");

        // Em vez de pausar direto, a gente chama o temporizador
        StartCoroutine(RotinaFinalizarFase());
    }

    IEnumerator RotinaFinalizarFase()
    {
        // O jogo continua rodando normal por X segundos
        yield return new WaitForSeconds(tempoEsperaFimDeFase);

        Debug.Log("Tempo esgotado. Exibindo tela de vitória e pausando o jogo.");

        // Agora sim, liga a tela e congela o tempo!
        if (telaFinalizacao != null)
        {
            telaFinalizacao.SetActive(true);
        }

        Time.timeScale = 0f;
    }
}