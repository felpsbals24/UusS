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

    public void MudarDeCena(string nomeDaProximaCena)
    {
        StartCoroutine(FadeOutE_Mudar(nomeDaProximaCena));
    }

    // --- A FUNÇÃO DE SAIR (CORRIGIDA) ---
    public void VoltarParaMenu()
    {
        // O jogo CONTINUA pausado aqui, nada se mexe!
        StartCoroutine(FadeOutE_Mudar("Menu"));
    }

    IEnumerator FadeIn()
    {
        telaPreta.gameObject.SetActive(true);
        telaPreta.color = new Color(0, 0, 0, 1);

        float tempoDecorrido = 0;
        while (tempoDecorrido < tempoDeTransicao)
        {
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
            tempoDecorrido += Time.unscaledDeltaTime;
            float alpha = Mathf.Lerp(0, 1, tempoDecorrido / tempoDeTransicao);
            telaPreta.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        // Só devolve o tempo ao normal AGORA, na hora de carregar a próxima cena!
        Time.timeScale = 1f;
        SceneManager.LoadScene(nomeDaCena);
    }
}