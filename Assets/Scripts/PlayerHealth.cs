using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public float vidaMaxima = 200f;
    public float vidaAtual;

    public Sprite[] spritesSeringa;
    public Image imagemSeringaUI;

    [Header("Buff de Regeneração")]
    public float quantidadeCura = 15f; // Valor fixo de cura
    public float intervaloRegen = 35f; // Tempo inicial
    public bool possuiRegen = false;  // Começa desligado

    void Start()
    {
        vidaAtual = vidaMaxima;
        AtualizarVisualDaSeringa();
        StartCoroutine(RotinaDeRegeneracao());
    }

    public void ReceberDano(float quantidadeDano)
    {
        // 1. Checa IMEDIATAMENTE se o cheat tá ligado. Se tiver, cancela tudo!
        if (DevModeManager.imortal) return;

        HDirections scriptMovimento = GetComponent<HDirections>();

        if (scriptMovimento != null && scriptMovimento.estaInvencivel)
        {
            return;
        }

        // 2. Só toma dano se for mortal e não estiver no meio do dash
        vidaAtual -= quantidadeDano;
        if (vidaAtual < 0) vidaAtual = 0;

        AtualizarVisualDaSeringa();

        if (vidaAtual == 0) Morrer();
    }

    public void Curar(float quantidade)
    {
        vidaAtual += quantidade;
        if (vidaAtual > vidaMaxima) vidaAtual = vidaMaxima;
        AtualizarVisualDaSeringa();
    }

    // --- NOVA FUNÇÃO DE MELHORAR O BUFF ---
    public void MelhorarRegen()
    {
        if (!possuiRegen)
        {
            possuiRegen = true;
            intervaloRegen = 35f; // Na primeira vez, ativa com 35s
        }
        else
        {
            intervaloRegen -= 1f; // Tira 1 segundo

            if (intervaloRegen < 15f)
            {
                intervaloRegen = 15f; // Trava no limite de 15 segundos
            }
        }
    }

    // --- MOTOR DE REGENERAÇÃO ATUALIZADO ---
    IEnumerator RotinaDeRegeneracao()
    {
        while (true)
        {
            if (possuiRegen)
            {
                // Espera o tempo atual (35s, 34s... até 15s)
                yield return new WaitForSeconds(intervaloRegen);

                if (vidaAtual < vidaMaxima)
                {
                    Curar(quantidadeCura);
                }
            }
            else
            {
                // Se ainda não pegou a carta, checa a cada 1 segundo sem fazer nada
                yield return new WaitForSeconds(1f);
            }
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
        if (GerenciadorDeJogo.instancia != null) GerenciadorDeJogo.instancia.MostrarGameOver();
        gameObject.SetActive(false);
    }
}