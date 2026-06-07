using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

// Essa lista define os TIPOS de cartas que existem no jogo
public enum TipoBuff
{
    VelocidadeAilone,
    VelocidadeOnda,
    VelocidadeCortadora,
    RegenVida,
    CooldownDash,
    DanoOnda
}

public class GerenciadorDeLevelUp : MonoBehaviour
{
    public static GerenciadorDeLevelUp instancia;

    public GameObject painelLevelUp;
    public TextMeshProUGUI textoCarta1;
    public TextMeshProUGUI textoCarta2;
    public TextMeshProUGUI textoCarta3;

    // Guarda quais cartas estão aparecendo agora na tela
    private TipoBuff buffCarta1;
    private TipoBuff buffCarta2;
    private TipoBuff buffCarta3;

    void Awake()
    {
        if (instancia == null) instancia = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        painelLevelUp.SetActive(false);
    }

    public void MostrarCartas()
    {
        painelLevelUp.SetActive(true);
        Time.timeScale = 0f;

        // 1. Cria o baralho com todos os buffs disponíveis e seus textos
        List<TipoBuff> baralho = new List<TipoBuff>
        {
            TipoBuff.VelocidadeAilone, TipoBuff.VelocidadeOnda,
            TipoBuff.VelocidadeCortadora, TipoBuff.RegenVida,
            TipoBuff.CooldownDash, TipoBuff.DanoOnda
        };

        // 2. Sorteia a Carta 1
        int index1 = Random.Range(0, baralho.Count);
        buffCarta1 = baralho[index1];
        textoCarta1.text = PegarTextoDoBuff(buffCarta1);
        baralho.RemoveAt(index1); // Remove pra não vir repetida

        // 3. Sorteia a Carta 2
        int index2 = Random.Range(0, baralho.Count);
        buffCarta2 = baralho[index2];
        textoCarta2.text = PegarTextoDoBuff(buffCarta2);
        baralho.RemoveAt(index2);

        // 4. Sorteia a Carta 3
        int index3 = Random.Range(0, baralho.Count);
        buffCarta3 = baralho[index3];
        textoCarta3.text = PegarTextoDoBuff(buffCarta3);
    }

    // Retorna o texto bonitinho dependendo do que foi sorteado
    string PegarTextoDoBuff(TipoBuff tipo)
    {
        switch (tipo)
        {
            case TipoBuff.VelocidadeAilone: return "Boost de velocidade do Ailone em +15%"; // Mudei aqui
            case TipoBuff.VelocidadeOnda: return "Onda de choque +5% de velocidade";
            case TipoBuff.VelocidadeCortadora: return "Velocidade da cortadora +10%";
            case TipoBuff.RegenVida: return "Regenera 15 HP (Reduz o tempo em 1s)"; // Mudei aqui
            case TipoBuff.CooldownDash: return "Recarga do Dash fica mais rapida";
            case TipoBuff.DanoOnda: return "Aumenta o dano da Onda em +5%";
            default: return "Buff misterioso!";
        }
    }

    // Botões chamam essa mesma função passando qual carta era
    public void EscolherCarta1() { AplicarBuff(buffCarta1); }
    public void EscolherCarta2() { AplicarBuff(buffCarta2); }
    public void EscolherCarta3() { AplicarBuff(buffCarta3); }

    void AplicarBuff(TipoBuff tipoEscolhido)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        switch (tipoEscolhido)
        {
            case TipoBuff.VelocidadeAilone:
                AtributosAilone.instancia.AumentarVelocidadeAilone(0.15f); // Reduzido de 0.50f para 0.15f
                break;
            case TipoBuff.VelocidadeOnda:
                AtributosAilone.instancia.AumentarVelocidadeOnda(0.50f);
                break;
            case TipoBuff.VelocidadeCortadora:
                AtributosAilone.instancia.AumentarVelocidadeCortadora(0.50f);
                break;
            case TipoBuff.DanoOnda:
                AtributosAilone.instancia.AumentarDanoOnda(0.50f);
                break;
            case TipoBuff.RegenVida:
                if (player != null) player.GetComponent<PlayerHealth>().MelhorarRegen(); // Nova função ativada!
                break;
            case TipoBuff.CooldownDash:
                if (player != null) player.GetComponent<HDirections>().DiminuirCooldownDash(0.15f);
                break;
        }

        FecharPainel();
    }

    void FecharPainel()
    {
        painelLevelUp.SetActive(false);
        Time.timeScale = 1f;
    }
}