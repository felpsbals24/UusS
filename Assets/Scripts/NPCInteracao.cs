using UnityEngine;

public class NPCInteracao : MonoBehaviour
{
    [Header("Visuais")]
    public GameObject balaoDeFala;

    [Header("Sistema")]
    public GameObject prefabArmaOrbital;

    // --- NOVA VARIÁVEL AQUI ---
    public int nivelNecessario = 2;

    private bool playerPerto = false;
    private bool jaEntregou = false;

    void Start()
    {
        if (balaoDeFala != null)
            balaoDeFala.SetActive(false);
    }

    void Update()
    {
        // Quando o player tá perto e aperta E, ele checa o nível antes de entregar
        if (playerPerto && !jaEntregou && Input.GetKeyDown(KeyCode.E))
        {
            // Puxamos a instância do seu gerenciador para ver o nível atual do Ailone
            if (GerenciadorDeProgresso.instancia != null)
            {
                if (GerenciadorDeProgresso.instancia.nivelAtual >= nivelNecessario)
                {
                    EntregarArma();
                }
                else
                {
                    // Feedback no console pra você saber que a trava funcionou!
                    Debug.Log("Ailone tá no nível " + GerenciadorDeProgresso.instancia.nivelAtual + ". Precisa estar no nível " + nivelNecessario + "!");
                }
            }
        }
    }

    // OLHA ELE AQUI! O famoso método EntregarArma:
    void EntregarArma()
    {
        jaEntregou = true;
        if (balaoDeFala != null) balaoDeFala.SetActive(false);

        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null && prefabArmaOrbital != null)
        {
            // Cria a arma
            GameObject novaArma = Instantiate(prefabArmaOrbital, player.transform.position, Quaternion.identity);

            // Gruda ela no Ailone
            novaArma.transform.SetParent(player.transform);

            // A MARRETADA: Força a arma a ficar no centro (0,0) do Ailone e garante que o Z dela seja 0!
            novaArma.transform.localPosition = Vector3.zero;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !jaEntregou)
        {
            playerPerto = true;
            if (balaoDeFala != null) balaoDeFala.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !jaEntregou)
        {
            playerPerto = false;
            if (balaoDeFala != null) balaoDeFala.SetActive(false);
        }
    }
}