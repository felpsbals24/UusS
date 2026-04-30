using UnityEngine;
using UnityEngine.UI;

public class GerenciadorDeProgresso : MonoBehaviour
{
    public static GerenciadorDeProgresso instancia;

    public int xpAtual = 0;
    public int nivelAtual = 0;
    public int[] xpPorNivel;

    public Image imagemBarra;
    public Sprite[] spritesDaBarra;

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

    public void AdicionarXP(int quantidadeXP)
    {
        xpAtual += quantidadeXP;
        ChecarNivel();
        AtualizarBarra();
    }

    void ChecarNivel()
    {
        if (nivelAtual < xpPorNivel.Length)
        {
            int xpNecessario = xpPorNivel[nivelAtual];

            if (xpAtual >= xpNecessario)
            {
                xpAtual -= xpNecessario;
                nivelAtual++;

                if (GerenciadorDeLevelUp.instancia != null)
                {
                    GerenciadorDeLevelUp.instancia.MostrarCartas();
                }

                ChecarNivel();
            }
        }
    }

    void AtualizarBarra()
    {
        if (nivelAtual >= xpPorNivel.Length)
        {
            if (imagemBarra != null && spritesDaBarra.Length > 0)
            {
                imagemBarra.sprite = spritesDaBarra[spritesDaBarra.Length - 1];
            }
            return;
        }

        float porcentagem = (float)xpAtual / xpPorNivel[nivelAtual];
        int pedacos = spritesDaBarra.Length - 1;
        int indiceSprite = Mathf.RoundToInt(porcentagem * pedacos);
        indiceSprite = Mathf.Clamp(indiceSprite, 0, pedacos);

        if (imagemBarra != null && spritesDaBarra.Length > 0)
        {
            imagemBarra.sprite = spritesDaBarra[indiceSprite];
        }
    }
}