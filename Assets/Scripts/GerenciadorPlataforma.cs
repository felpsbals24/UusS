using UnityEngine;

public class GerenciadorPlataforma : MonoBehaviour
{
    [Header("Objeto com todos os botões mobile")]
    public GameObject painelControlesMobile;

    void Awake()
    {
        // Verifica a plataforma do jogo
#if UNITY_ANDROID || UNITY_IOS 
        // Se for celular, ativa os botões na tela
        if (painelControlesMobile != null) painelControlesMobile.SetActive(true);
#else
        // Se for PC (Windows/Mac) ou o próprio Editor da Unity, esconde os botões
        if (painelControlesMobile != null) painelControlesMobile.SetActive(false);
#endif
    }
}