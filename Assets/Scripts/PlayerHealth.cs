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