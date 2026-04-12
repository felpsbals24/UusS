using UnityEngine;
using TMPro; 

public class GerenciadorDeJogo : MonoBehaviour
{
    
    public static GerenciadorDeJogo instancia;

    [Header("Configurações da Fase")]
    public int ursosRestantes = 20; 

    [Header("Interface (UI)")]
    public TextMeshProUGUI textoContador; 

    void Awake()
    {
      
        instancia = this;
    }

    void Start()
    {
       
        AtualizarTexto();
    }

   
    public void UrsoMorreu()
    {
        ursosRestantes--; 

        if (ursosRestantes <= 0)
        {
            ursosRestantes = 0;
            Debug.Log("Você sobreviveu à horda de ursos!");
           
        }

        AtualizarTexto(); 
    }

    void AtualizarTexto()
    {
        if (textoContador != null)
        {
            textoContador.text = "Ursos Restantes: " + ursosRestantes;
        }
    }
}