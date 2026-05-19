using UnityEngine;
using System.Collections;

public class PlayerPulse : MonoBehaviour
{
    public float raioDoPulso = 5f;
    public int danoDoPulso = 5;
    public float intervalo = 3f;

    public GameObject vfxDoPulso;

    void Start()
    {
        StartCoroutine(RotinaDeAtaque());
    }

    IEnumerator RotinaDeAtaque()
    {
        while (true)
        {
            float intervaloAtual = intervalo;

            if (AtributosAilone.instancia != null)
            {
                intervaloAtual /= AtributosAilone.instancia.multiplicadorVelocidadeOnda;
            }

            yield return new WaitForSeconds(intervaloAtual);
            ExecutarPulso();
        }
    }

    void ExecutarPulso()
    {
        if (vfxDoPulso != null)
        {
            GameObject vfx = Instantiate(vfxDoPulso, transform.position, Quaternion.identity);
            Destroy(vfx, 1.0f);
        }

        // --- NOVO: CÁLCULO DO DANO COM BUFF ---
        int danoFinal = danoDoPulso;

        if (AtributosAilone.instancia != null)
        {
            // Multiplica o dano base pelo multiplicador e arredonda para virar inteiro
            danoFinal = Mathf.RoundToInt(danoDoPulso * AtributosAilone.instancia.multiplicadorDanoOnda);
        }
        // --------------------------------------

        Collider2D[] inimigosAtingidos = Physics2D.OverlapCircleAll(transform.position, raioDoPulso);

        foreach (Collider2D col in inimigosAtingidos)
        {
            EnemyHealth hp = col.GetComponent<EnemyHealth>();
            if (hp != null)
            {
                hp.TomarDano(danoFinal); // Usa a variável nova que tem o buff aplicado!
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, raioDoPulso);
    }
}