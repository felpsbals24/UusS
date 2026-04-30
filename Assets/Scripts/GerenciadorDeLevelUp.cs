using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GerenciadorDeLevelUp : MonoBehaviour
{
    public static GerenciadorDeLevelUp instancia;

    public GameObject painelLevelUp;

    public TextMeshProUGUI textoCarta1;
    public TextMeshProUGUI textoCarta2;
    public TextMeshProUGUI textoCarta3;

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
        painelLevelUp.SetActive(false);

        textoCarta1.text = "Boost de velocidade do Ailone em +5%";
        textoCarta2.text = "Onda de choque glicemico +5% de velocidade";
        textoCarta3.text = "Velocidade da cortadora de glicose +10%";
    }

    public void MostrarCartas()
    {
        painelLevelUp.SetActive(true);
        Time.timeScale = 0f;
    }

    public void EscolherCarta1()
    {
        if (AtributosAilone.instancia != null)
        {
            AtributosAilone.instancia.AumentarVelocidadeAilone(0.05f);
        }
        FecharPainel();
    }

    public void EscolherCarta2()
    {
        if (AtributosAilone.instancia != null)
        {
            AtributosAilone.instancia.AumentarVelocidadeOnda(0.05f);
        }
        FecharPainel();
    }

    public void EscolherCarta3()
    {
        if (AtributosAilone.instancia != null)
        {
            AtributosAilone.instancia.AumentarVelocidadeCortadora(0.10f);
        }
        FecharPainel();
    }

    void FecharPainel()
    {
        painelLevelUp.SetActive(false);
        Time.timeScale = 1f;
    }
}