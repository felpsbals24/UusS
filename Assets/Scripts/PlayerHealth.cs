using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float vidaMaxima = 100f;
    private float vidaAtual;

    public Sprite[] spritesSeringa;
    public Image imagemSeringaUI;

    void Start()
    {
        vidaAtual = vidaMaxima;
        AtualizarVisualDaSeringa();
    }

    public void ReceberDano(float quantidadeDano)
    {
        // --- CÓDIGO DO DASH (INVENCIBILIDADE) ENTRA AQUI ---
        HDirections scriptMovimento = GetComponent<HDirections>();

        // Se o script existir e ele estiver dando o dash, a gente ignora o dano!
        if (scriptMovimento != null && scriptMovimento.estaInvencivel)
        {
            Debug.Log("Dash perfeito! Ailone desviou do urso.");
            return; // O return faz o código parar aqui e não continua pra baixo
        }
        // ---------------------------------------------------

        vidaAtual -= quantidadeDano;

        if (vidaAtual < 0)
        {
            vidaAtual = 0;
        }

        AtualizarVisualDaSeringa();

        if (vidaAtual == 0)
        {
            Morrer();
        }
    }

    void AtualizarVisualDaSeringa()
    {
        if (spritesSeringa.Length == 0 || imagemSeringaUI == null) return;

        float porcentagemVida = vidaAtual / vidaMaxima;

        int indiceDoSprite = Mathf.RoundToInt((spritesSeringa.Length - 1) * (1f - porcentagemVida));

        indiceDoSprite = Mathf.Clamp(indiceDoSprite, 0, spritesSeringa.Length - 1);

        imagemSeringaUI.sprite = spritesSeringa[indiceDoSprite];
    }

    void Morrer()
    {
        if (GerenciadorDeJogo.instancia != null)
        {
            GerenciadorDeJogo.instancia.MostrarGameOver();
        }
        gameObject.SetActive(false);
    }
}