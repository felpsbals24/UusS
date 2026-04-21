using UnityEngine;
using UnityEngine.UI;

public class SistemaDeVida : MonoBehaviour
{
    public float acucarMaximo = 100f;
    public float acucarAtual = 0f;

    public Image imagemLiquido;

    void Start()
    {
        acucarAtual = 0f;
        AtualizarSeringa();
    }

    public void ReceberAcucar(float quantidade)
    {
        acucarAtual += quantidade;

        if (acucarAtual >= acucarMaximo)
        {
            acucarAtual = acucarMaximo;
            ChoqueGlicemico();
        }

        AtualizarSeringa();
    }

    void AtualizarSeringa()
    {
        if (imagemLiquido != null)
        {
            imagemLiquido.fillAmount = acucarAtual / acucarMaximo;
        }
    }

    void ChoqueGlicemico()
    {
        if (GerenciadorDeJogo.instancia != null)
        {
            GerenciadorDeJogo.instancia.MostrarGameOver();
        }
        gameObject.SetActive(false);
    }
}