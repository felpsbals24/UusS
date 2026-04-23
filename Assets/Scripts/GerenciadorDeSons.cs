using UnityEngine;

public class GerenciadorDeSons : MonoBehaviour
{
    public AudioSource tocadorDeEfeitos;  

    [Header("Seus Efeitos")]
    public AudioClip somBotao1;
    public AudioClip somBotao2;
    public AudioClip somBotao3;

     
    public void TocarSom1()
    {
        tocadorDeEfeitos.PlayOneShot(somBotao1);
    }

    public void TocarSom2()
    {
        tocadorDeEfeitos.PlayOneShot(somBotao2);
    }

    public void TocarSom3()
    {
        tocadorDeEfeitos.PlayOneShot(somBotao3);
    }
}