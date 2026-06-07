using UnityEngine;
using System.Collections;

public class AtaqueBaldeGelatina : MonoBehaviour
{
    [Header("Configurações do Ataque")]
    public GameObject prefabBaldeProjetil; // ARRASTE O PREFAB DO BALDE ANIMADO AQUI
    public float raioDeLancamento = 4f;
    public float intervaloAtaque = 3.5f;

    void Start()
    {
        StartCoroutine(RotinaLancamento());
    }

    IEnumerator RotinaLancamento()
    {
        while (true)
        {
            yield return new WaitForSeconds(intervaloAtaque);
            LancarBalde();
        }
    }

    void LancarBalde()
    {
        if (prefabBaldeProjetil == null) return;

        Vector2 pontoAleatorio = Random.insideUnitCircle * raioDeLancamento;
        Vector3 posicaoAlvo = transform.position + new Vector3(pontoAleatorio.x, pontoAleatorio.y, 0);

        // Cria o balde na posição do Ailone
        GameObject balde = Instantiate(prefabBaldeProjetil, transform.position, Quaternion.identity);

        // Passa o alvo para o script do balde levá-lo até lá
        BaldeProjetil scriptProjetil = balde.GetComponent<BaldeProjetil>();
        if (scriptProjetil != null)
        {
            scriptProjetil.ConfigurarDestino(posicaoAlvo);
        }
    }
}