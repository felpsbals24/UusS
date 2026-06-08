using UnityEngine;

public class NPCLoja : MonoBehaviour
{
    [Header("UI do NPC")]
    public GameObject balaoDeFala;
    private bool playerPerto = false;

    void Start()
    {
        // Garante que o balão começa desligado
        if (balaoDeFala != null) balaoDeFala.SetActive(false);
    }

    void Update()
    {
        // Mantém o atalho do teclado para quando estiver testando no PC
        if (playerPerto && Input.GetKeyDown(KeyCode.E))
        {
            InteragirComLoja();
        }
    }

    // --- NOVA FUNÇÃO PÚBLICA PARA O BOTÃO DO CELULAR ---
    public void InteragirComLoja()
    {
        // SÓ ABRE se o jogador estiver de fato perto do NPC!
        if (playerPerto)
        {
            if (GerenciadorLoja.instancia != null)
            {
                GerenciadorLoja.instancia.AbrirLoja();

                // Esconde o balão de fala enquanto a loja tá aberta
                if (balaoDeFala != null) balaoDeFala.SetActive(false);
            }
        }
        else
        {
            Debug.Log("O Player está longe demais para abrir a loja!");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerPerto = true;
            if (balaoDeFala != null) balaoDeFala.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerPerto = false;
            if (balaoDeFala != null) balaoDeFala.SetActive(false);

            // Trava de segurança: se o player se afastar, fecha a loja na força
            if (GerenciadorLoja.instancia != null) GerenciadorLoja.instancia.FecharLoja();
        }
    }
}