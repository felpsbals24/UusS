using UnityEngine;
using System.Collections;

public class AnimacaoVFXPulso : MonoBehaviour
{
     
    public float tamanhoDoRaio = 5f;
    public float duracaoDaAnimacao = 0.5f;  
    public Color corInicial = new Color(0.2f, 0.8f, 1f, 0.7f);  

    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.color = corInicial;

       
        transform.localScale = Vector3.zero;

        StartCoroutine(AnimarCrescimento());
    }

    IEnumerator AnimarCrescimento()
    {
        float tempo = 0f;

        
        Vector3 escalaFinal = new Vector3(tamanhoDoRaio * 2, tamanhoDoRaio * 2, 1f);

        while (tempo < duracaoDaAnimacao)
        {
            tempo += Time.deltaTime;
            float progresso = tempo / duracaoDaAnimacao;

           
            transform.localScale = Vector3.Lerp(Vector3.zero, escalaFinal, progresso);
 
            Color corAtual = sr.color;
            corAtual.a = Mathf.Lerp(corInicial.a, 0f, progresso);
            sr.color = corAtual;

            yield return null;
        }
    }
}