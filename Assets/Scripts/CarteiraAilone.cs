using UnityEngine;
using TMPro; // Caso queira colocar um texto de moedas na HUD depois

public class CarteiraAilone : MonoBehaviour
{
    public static CarteiraAilone instancia;

    [Header("Economia")]
    public int docesDourados = 0;
    public TextMeshProUGUI textoMoedasHUD; // Opcional: Arraste um texto da HUD aqui se quiser mostrar o saldo

    void Awake()
    {
        if (instancia == null) instancia = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        AtualizarHUD();
    }

    public void AdicionarMoedas(int quantidade)
    {
        docesDourados += quantidade;
        AtualizarHUD();
        Debug.Log("Ganhou doce dourado! Saldo atual: " + docesDourados);
    }

    public bool GastarMoedas(int quantidade)
    {
        if (docesDourados >= quantidade)
        {
            docesDourados -= quantidade;
            AtualizarHUD();
            return true; // Compra autorizada
        }
        return false; // Dinheiro insuficiente
    }

    void AtualizarHUD()
    {
        if (textoMoedasHUD != null)
        {
            textoMoedasHUD.text = "x " + docesDourados;
        }
    }
}