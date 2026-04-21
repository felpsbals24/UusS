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
            yield return new WaitForSeconds(intervalo);
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

        
        Collider2D[] inimigosAtingidos = Physics2D.OverlapCircleAll(transform.position, raioDoPulso);

        foreach (Collider2D col in inimigosAtingidos)
        {
            EnemyHealth hp = col.GetComponent<EnemyHealth>();
            if (hp != null)
            {
                hp.TomarDano(danoDoPulso);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, raioDoPulso);
    }
}