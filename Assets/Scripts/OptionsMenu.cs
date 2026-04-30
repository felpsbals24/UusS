using UnityEngine;

public class OptionsMenu : MonoBehaviour
{
    [Header("Telas")]
    public GameObject optionsPanel;

    [Header("Áudio")]
    public AudioSource musicaFundo;

    // --- BOTÕES DE ABRIR E FECHAR ---
    public void OpenOptions()
    {
        // O Espião fofoqueiro:
        Debug.Log("1. O clique do botão chegou na função OpenOptions!");

        if (optionsPanel != null)
        {
            Debug.Log("2. A tela de opções foi encontrada e vai ser ativada!");
            optionsPanel.SetActive(true);
        }
        else
        {
            Debug.LogWarning("ALERTA: A função rodou, mas o espaço 'Options Panel' está vazio no Inspector!");
        }
    }

    public void CloseOptions()
    {
        optionsPanel.SetActive(false);
    }

    // --- CONTROLES DE VOLUME ---
    public void MudarVolumeGeral(float volume)
    {
        AudioListener.volume = volume;
    }

    public void MudarVolumeMusica(float volume)
    {
        if (musicaFundo != null)
        {
            musicaFundo.volume = volume;
        }
    }

    public void FecharJogo()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}