using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

[System.Serializable]
public class ItemShop
{
    public string nome;
    public int preco;
    public GameObject prefabDoItem;
    public Sprite iconeDoItem;
    public bool jaComprado = false;// A IMAGEM que você queria
}

public class GerenciadorLoja : MonoBehaviour
{
    public static GerenciadorLoja instancia;

    [Header("Configuração da UI")]
    public GameObject painelLoja;
    public TextMeshProUGUI textoSaldo;
    public Image displayImagemItem; // Um campo de Image na UI para mostrar o que foi comprado
    public TextMeshProUGUI textoNomeItem; // Um texto para mostrar o nome do que comprou

    [Header("Banco de Dados de Itens")]
    public List<ItemShop> listaDeItens; // Adicione quantos itens quiser aqui no Unity!

    void Awake()
    {
        if (instancia == null) instancia = this;
        else Destroy(gameObject);
    }

    public void AbrirLoja()
    {
        // Procura o NPC na cena e verifica se o player está perto antes de abrir!
        NPCLoja npc = Object.FindFirstObjectByType<NPCLoja>();

        // Se achou o NPC e o código dele diz que o player está longe, cancela!
        if (npc != null)
        {
            // Como 'playerPerto' é privado, vamos descobrir se o balão de fala está ativo na cena. 
            // Se o balão está ativo, significa que o player está perto!
            if (npc.balaoDeFala != null && !npc.balaoDeFala.activeSelf)
            {
                Debug.Log("Bloqueado: O Player não está perto do NPC!");
                return; // Sai da função e não abre a loja
            }
        }

        // Se passou no teste (ou se não achar o NPC para testar), abre a loja normalmente
        painelLoja.SetActive(true);
        Time.timeScale = 0f;
        AtualizarSaldoVisual();
    }

    public void FecharLoja()
    {
        painelLoja.SetActive(false);
        Time.timeScale = 1f;
    }

    // ESSA É A FUNÇÃO QUE VOCÊ VAI USAR NO ONCLICK
    // Você vai passar o NÚMERO (ID) do item na lista
    public void ComprarItemPeloID(int id)
    {
        if (id < 0 || id >= listaDeItens.Count) return;

        ItemShop item = listaDeItens[id];

        // --- NOVA TRAVA: SE JÁ COMPROU, CANCELA! ---
        if (item.jaComprado)
        {
            Debug.Log("Item já foi comprado!");
            return;
        }

        if (CarteiraAilone.instancia.GastarMoedas(item.preco))
        {
            item.jaComprado = true; // Marca como comprado
            EquiparItem(item);
        }
        else
        {
            Debug.Log("Dinheiro insuficiente para " + item.nome);
        }
    }

    void EquiparItem(ItemShop item)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null && item.prefabDoItem != null)
        {
            GameObject novo = Instantiate(item.prefabDoItem, player.transform.position, Quaternion.identity);
            novo.transform.SetParent(player.transform);
            novo.transform.localPosition = Vector3.zero;

            // Atualiza a imagem e nome no painel da loja para feedback
            if (displayImagemItem != null) displayImagemItem.sprite = item.iconeDoItem;
            if (textoNomeItem != null) textoNomeItem.text = "Comprou: " + item.nome;

            AtualizarSaldoVisual();
        }
    }

    void AtualizarSaldoVisual()
    {
        if (CarteiraAilone.instancia != null)
            textoSaldo.text = "Saldo: " + CarteiraAilone.instancia.docesDourados;
    }
}