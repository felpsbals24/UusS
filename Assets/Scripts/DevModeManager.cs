using UnityEngine;

public class DevModeManager : MonoBehaviour
{
    // Variáveis estáticas para qualquer script do jogo ler facilmente
    public static bool imortal = false;
    public static bool superVelocidade = false;

    [Header("UI")]
    public GameObject painelDev;

    // Abre/Fecha o painel ao clicar no botão de configurações
    public void AlternarPainelDev()
    {
        if (painelDev != null)
            painelDev.SetActive(!painelDev.activeSelf);
    }

    public void CheatImortalidade()
    {
        imortal = !imortal;
        Debug.Log("DEV: Imortalidade = " + imortal);
    }

    public void CheatVelocidade()
    {
        superVelocidade = !superVelocidade;
        Debug.Log("DEV: Super Velocidade = " + superVelocidade);
    }

    public void CheatMoedas()
    {
        if (CarteiraAilone.instancia != null)
        {
            CarteiraAilone.instancia.AdicionarMoedas(100); // Dá 100 moedas
        }
    }

    public void CheatXP()
    {
        // Puxa o seu script de progresso que criamos na primeira aula
        if (GerenciadorDeProgresso.instancia != null)
        {
            // Se o seu método de ganhar XP tiver outro nome (ex: GanharXP), mude aqui!
            // GerenciadorDeProgresso.instancia.AdicionarXP(50); 
            Debug.Log("DEV: XP Adicionado!");
        }
    }

    public void CheatSpawnTodosUrsos()
    {
        // Puxa o seu WaveSpawner em qualquer lugar da cena
        WaveSpawner spawner = Object.FindFirstObjectByType<WaveSpawner>();

        if (spawner != null)
        {
            // Hackeia o tempo original e crava em ZERO
            spawner.tempoEntreSpawns = 0f;
            Debug.Log("DEV: Tempo de spawn zerado! Prepara que vai chover urso!");
        }
        else
        {
            Debug.Log("DEV: Erro - Não achei o WaveSpawner na cena.");
        }
    }

    public void CheatPularOnda()
    {
        GameObject spawner = GameObject.Find("SpawnerManager");
        if (spawner != null)
        {
            // Insira aqui o método que avança a onda no seu Spawner
            Debug.Log("DEV: Onda Pulada!");
        }
    }
}