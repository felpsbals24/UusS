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
        // Se o player apertar E perto do NPC, abre a interface da loja
        if (playerPerto && Input.GetKeyDown(KeyCode.E))
        {
            if (GerenciadorLoja.instancia != null)
            {
                GerenciadorLoja.instancia.AbrirLoja();

                // Opcional: esconde o balão de fala de "Aperte E" enquanto a loja tá aberta
                if (balaoDeFala != null) balaoDeFala.SetActive(false);
            }
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