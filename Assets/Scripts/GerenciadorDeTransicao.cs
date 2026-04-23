using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement; // Essencial para mudar de cena
using UnityEngine.UI; // Essencial para mexer na imagem preta

public class GerenciadorDeTransicao : MonoBehaviour
{
    [Header("Configurações")]
    public Image telaPreta;
    public float tempoDeTransicao = 3f; // Seus 3 segundos de atraso/fade

    void Start()
    {
        // Assim que a cena abre, a tela começa preta e vai clareando
        if (telaPreta != null)
        {
            StartCoroutine(FadeIn());
        }
    }

    // Você vai chamar essa função nos seus botões (Jogar, Sair, etc)
    public void MudarDeCena(string nomeDaProximaCena)
    {
        StartCoroutine(FadeOutE_Mudar(nomeDaProximaCena));
    }

    IEnumerator FadeIn()
    {
        telaPreta.gameObject.SetActive(true);
        telaPreta.color = new Color(0, 0, 0, 1); // Alpha 1 (Totalmente preto)

        float tempoDecorrido = 0;
        while (tempoDecorrido < tempoDeTransicao)
        {
            tempoDecorrido += Time.deltaTime;
            // Vai reduzindo o Alpha de 1 para 0
            float alpha = Mathf.Lerp(1, 0, tempoDecorrido / tempoDeTransicao);
            telaPreta.color = new Color(0, 0, 0, alpha);

            yield return null; // Espera o próximo frame
        }

        // Desativa a tela preta no final para não bloquear seus cliques
        telaPreta.gameObject.SetActive(false);
    }

    IEnumerator FadeOutE_Mudar(string nomeDaCena)
    {
        telaPreta.gameObject.SetActive(true);
        telaPreta.color = new Color(0, 0, 0, 0); // Alpha 0 (Transparente)

        float tempoDecorrido = 0;
        while (tempoDecorrido < tempoDeTransicao)
        {
            tempoDecorrido += Time.deltaTime;
            // Vai aumentando o Alpha de 0 para 1
            float alpha = Mathf.Lerp(0, 1, tempoDecorrido / tempoDeTransicao);
            telaPreta.color = new Color(0, 0, 0, alpha);

            yield return null; // Espera o próximo frame
        }

        // Depois que a tela ficou 100% preta e passou os 3 segundos, carrega a cena!
        SceneManager.LoadScene(nomeDaCena);
    }
}