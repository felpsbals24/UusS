using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GerenciadorDeTransicao : MonoBehaviour
{
    [Header("Configurações")]
    public Image telaPreta;
    public float tempoDeTransicao = 3f;

    void Start()
    {
        if (telaPreta != null)
        {
            StartCoroutine(FadeIn());
        }
    }

    // Função antiga que você já usava (para Iniciar o jogo, etc)
    public void MudarDeCena(string nomeDaProximaCena)
    {
        Time.timeScale = 1f; // Despausa o jogo por garantia
        StartCoroutine(FadeOutE_Mudar(nomeDaProximaCena));
    }

    // --- NOVA FUNÇÃO --- 
    // Feita só pro seu botão de Sair do Pause!
    public void VoltarParaMenu()
    {
        Time.timeScale = 1f; // Tira o pause do jogo
        StartCoroutine(FadeOutE_Mudar("Menu")); // Já carrega o Menu direto
    }

    IEnumerator FadeIn()
    {
        telaPreta.gameObject.SetActive(true);
        telaPreta.color = new Color(0, 0, 0, 1);

        float tempoDecorrido = 0;
        while (tempoDecorrido < tempoDeTransicao)
        {
            // unscaledDeltaTime faz a animação rodar MESMO com o jogo pausado!
            tempoDecorrido += Time.unscaledDeltaTime;

            float alpha = Mathf.Lerp(1, 0, tempoDecorrido / tempoDeTransicao);
            telaPreta.color = new Color(0, 0, 0, alpha);

            yield return null;
        }

        telaPreta.gameObject.SetActive(false);
    }

    IEnumerator FadeOutE_Mudar(string nomeDaCena)
    {
        telaPreta.gameObject.SetActive(true);
        telaPreta.color = new Color(0, 0, 0, 0);

        float tempoDecorrido = 0;
        while (tempoDecorrido < tempoDeTransicao)
        {
            // unscaledDeltaTime aqui de novo para garantir a saída suave
            tempoDecorrido += Time.unscaledDeltaTime;

            float alpha = Mathf.Lerp(0, 1, tempoDecorrido / tempoDeTransicao);
            telaPreta.color = new Color(0, 0, 0, alpha);

            yield return null;
        }

        // Garante que o Menu Principal vai começar despausado
        Time.timeScale = 1f;
        SceneManager.LoadScene(nomeDaCena);
    }
}