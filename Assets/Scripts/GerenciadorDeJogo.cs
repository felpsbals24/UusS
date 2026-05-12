using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class GerenciadorDeJogo : MonoBehaviour
{
    public static GerenciadorDeJogo instancia;

    public int inimigosPorOnda = 20;
    private int inimigosRestantes;
    public int ondaAtual = 1;

    public WaveSpawner spawner;

    public TextMeshProUGUI textoContador;
    public TextMeshProUGUI textoOndasVencidas; // NOVO
    public TextMeshProUGUI textoMensagemCentro;
    public GameObject painelGameOver;
    public GameObject hudGeral;

    void Awake()
    {
        instancia = this;
        Time.timeScale = 1f;
    }

    void Start()
    {
        textoMensagemCentro.gameObject.SetActive(false);
        if (painelGameOver != null) painelGameOver.SetActive(false);
        if (hudGeral != null) hudGeral.SetActive(true);

        IniciarOnda(inimigosPorOnda);
    }

    public void IniciarOnda(int quantidade)
    {
        inimigosRestantes = quantidade;
        spawner.IniciarNovaOnda(quantidade, ondaAtual);
        AtualizarTexto();
    }

    public void UrsoMorreu()
    {
        inimigosRestantes--;

        if (inimigosRestantes <= 0)
        {
            inimigosRestantes = 0;
            int ondaQueAcabou = ondaAtual; // Salva o número da onda que acabou de ser limpa
            ondaAtual++; // Prepara para a próxima (se houver)

            AtualizarTexto(); // Atualiza a HUD pra mostrar 0 inimigos antes de congelar

            // --- NOSSO GATILHO NA PORTA DE SAÍDA ---
            if (GerenciadorDeFase.instancia != null)
            {
                // Avisa que a onda acabou
                GerenciadorDeFase.instancia.RegistrarOndaConcluida(ondaQueAcabou);

                // Se a onda que acabamos de vencer é igual ou maior que o limite, acaba o jogo AQUI!
                if (ondaQueAcabou >= GerenciadorDeFase.instancia.ondaMaxima)
                {
                    return; // O "return" faz o código parar e não deixa a contagem da próxima onda iniciar!
                }
            }
            // ----------------------------------------

            // Se não for a última onda, segue o jogo normal
            StartCoroutine(ContagemRegressivaProximaOnda());
            return;
        }

        AtualizarTexto();
    }

    void AtualizarTexto()
    {
        if (textoContador != null)
            textoContador.text = "Inimigos Restantes: " + inimigosRestantes;

        if (textoOndasVencidas != null)
            textoOndasVencidas.text = "Ondas Vencidas: " + (ondaAtual - 1);
    }

    IEnumerator ContagemRegressivaProximaOnda()
    {
        textoMensagemCentro.gameObject.SetActive(true);
        for (int i = 5; i > 0; i--)
        {
            textoMensagemCentro.text = "Nova onda em: " + i;
            yield return new WaitForSeconds(1f);
        }
        textoMensagemCentro.gameObject.SetActive(false);
        inimigosPorOnda += 5;
        IniciarOnda(inimigosPorOnda);
    }

    public void MostrarGameOver()
    {
        if (painelGameOver != null) painelGameOver.SetActive(true);
        if (hudGeral != null) hudGeral.SetActive(false);
        Time.timeScale = 0f;
    }

    public void ReiniciarFase()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void VoltarMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
}