using UnityEngine;

public class PirulitoDourado : MonoBehaviour
{
    [Header("Configurações do Ímã")]
    public float raioAbsorcao = 3f; // Distância que o Ailone precisa chegar para puxar
    public float velocidadeVoo = 10f; // Quão rápido a moeda voa até ele

    private Transform playerTransform;
    private bool sendoPuxado = false;

    void Start()
    {
        // Encontra o Ailone na cena pela Tag
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }

        // --- SOLUÇÃO DO BUG DE CAIR DO MAPA ---
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.gravityScale = 0f; // Força a gravidade a ser ZERO para não cair na tela
        }
    }

    void Update()
    {
        if (playerTransform == null) return;

        // Calcula a distância entre a moeda e o Ailone
        float distancia = Vector2.Distance(transform.position, playerTransform.position);

        // Se o player entrou no raio, ativa o ímã
        if (distancia <= raioAbsorcao)
        {
            sendoPuxado = true;
        }

        // Se o ímã estiver ativo, voa na direção do Ailone
        if (sendoPuxado)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, velocidadeVoo * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (CarteiraAilone.instancia != null)
            {
                CarteiraAilone.instancia.AdicionarMoedas(1);
            }
            Destroy(gameObject);
        }
    }
}