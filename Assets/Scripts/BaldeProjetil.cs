using UnityEngine;

public class BaldeProjetil : MonoBehaviour
{
    public float velocidadeVoo = 12f;
    public GameObject prefabPocaAcido; // Prefab da poça verde escuro

    private Vector3 destino;
    private bool destinoDefinido = false;

    public void ConfigurarDestino(Vector3 posicaoDestino)
    {
        destino = posicaoDestino;
        destinoDefinido = true;
    }

    void Update()
    {
        if (!destinoDefinido) return;

        // Move o balde pelo ar até o ponto sorteado no chão
        transform.position = Vector3.MoveTowards(transform.position, destino, velocidadeVoo * Time.deltaTime);

        // Quando o balde chega no chão
        if (Vector3.Distance(transform.position, destino) < 0.1f)
        {
            if (prefabPocaAcido != null)
            {
                // Força o Z a ser 0 para não nascer atrás do background
                Vector3 posicaoCorrecao = new Vector3(destino.x, destino.y, 0f);
                Instantiate(prefabPocaAcido, posicaoCorrecao, Quaternion.identity);
            }
            Destroy(gameObject); // Destrói o balde voador
        }
    }
}