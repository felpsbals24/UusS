using UnityEngine;
using UnityEngine.UI;
using TMPro; // ADICIONADO: Necessário para controlar textos do TextMeshPro

public class GerenciadorDeProgresso : MonoBehaviour
{
    public static GerenciadorDeProgresso instancia;

    public int xpAtual = 0;
    public int nivelAtual = 0;
    public int[] xpPorNivel;

    public Image imagemBarra;
    public Sprite[] spritesDaBarra;

    // --- NOVA VARIÁVEL ENTRA AQUI ---
    [Header("Interface do Nível")]
    public TextMeshProUGUI textoNivel; // Arraste o texto do nível para cá no Unity

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
        // Garante que o texto comece certo assim que o jogo iniciar
        AtualizarTextoNivel();
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

                // --- ATUALIZA O TEXTO QUANDO O NÍVEL SUBIR ---
                AtualizarTextoNivel();

                if (GerenciadorDeLevelUp.instancia != null)
                {
                    GerenciadorDeLevelUp.instancia.MostrarCartas();
                }

                ChecarNivel();
            }
        }
    }

    // --- NOVA FUNÇÃO PARA EXIBIR O NÍVEL NA TELA ---
    void AtributosAilone() { } // Apenas mantendo referência visual

    void AtualizarTextoNivel()
    {
        if (textoNivel != null)
        {
            textoNivel.text = "LV. " + nivelAtual;  
        }
    }

    void Blueprints() { }

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